using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Newtonsoft.Json;

namespace my_testcsharp0_bot_WebAPI
{
	public class TelegramBotWorker : BackgroundService
	{
		private readonly ILogger<TelegramBotWorker> _logger;
		private readonly IConfiguration _configuration;

		public TelegramBotWorker(ILogger<TelegramBotWorker> logger, IConfiguration configuration)
		{
			_logger = logger;
			_configuration = configuration;	
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			if (_configuration.GetSection("Token").Value is not { } Token)
				return;

			TelegramBotClient botClient = new TelegramBotClient(Token);

			using var cts = new CancellationTokenSource();

			var receiverOptions = new ReceiverOptions
			{
				AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
			};
			botClient.StartReceiving(
				updateHandler: HandleUpdateAsync,
				pollingErrorHandler: HandlePollingErrorAsync,
				receiverOptions: receiverOptions,
				cancellationToken: cts.Token
			);

			var me = await botClient.GetMeAsync();

			_logger.LogInformation($"Start listening for @{me.Username}");
		}
		async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
		{
			if (update.Message is not { } message)
				return;

			if (message.Text is not { } messageText)
				return;

			var chatId = message.Chat.Id;

			_logger.LogInformation($"Received a '{messageText}' message in chat {chatId}.");

			if (_configuration.GetSection("Url").Value is not { } Url)
				return;

			using (HttpClient httpClient = new HttpClient())
			{
				switch (message.Text)
				{
					case "/start":
						{
							try
							{
								var json =	new my_testcsharp0_bot_WebAPI.Domains.Chats.Chat
									{
										Id = message.Chat.Id,
										Username = message.Chat.Username,
										FirstName = message.Chat.FirstName,
										LastName = message.Chat.LastName
									};
								HttpResponseMessage response = await httpClient.PostAsJsonAsync(Url + "/api/Redirect/CreateChat", json);
								if (response.IsSuccessStatusCode)
								{
									await botClient.SendTextMessageAsync(chatId, "I registered you!");
								}
								else
								{
									var responseString = response.Content.ReadAsStringAsync().Result;
									await botClient.SendTextMessageAsync(chatId, responseString);
								}
							}
							catch (Exception ex)
							{
								await botClient.SendTextMessageAsync(chatId, ex.Message);
							}
							break;
						}
					case "/getAllMessages":
						{
							try
							{
								HttpResponseMessage response = await httpClient.GetAsync(Url + "/api/Redirect/" + message.Chat.Id);
								if (response.IsSuccessStatusCode)
								{
									var responseString = response.Content.ReadAsStringAsync().Result;
									var json = JsonConvert.DeserializeObject<IEnumerable<my_testcsharp0_bot_WebAPI.Domains.Messages.Message>>(responseString);

									foreach (var n in json)
									{
										await botClient.SendTextMessageAsync(chatId, n.Date + " : " + n.Text);
									}
								}
								else
								{
									var responseString = response.Content.ReadAsStringAsync().Result;
									await botClient.SendTextMessageAsync(chatId, responseString);
								}
							}
							catch (Exception ex)
							{
								await botClient.SendTextMessageAsync(chatId, ex.Message);
							}
							break;
						}
					default:
						{
							try
							{
								var json = new my_testcsharp0_bot_WebAPI.Domains.Messages.Message
								{
									Id = message.MessageId,
									ChatId = message.Chat.Id,
									Text = message.Text,
									Date = message.Date.ToLocalTime(),
									Chat = new Domains.Chats.Chat
									{
										Id = message.Chat.Id,
										Username = message.Chat.Username,
										FirstName = message.Chat.FirstName,
										LastName = message.Chat.LastName
									}
								};
								HttpResponseMessage response = await httpClient.PostAsJsonAsync(Url + "/api/Redirect/CreateMessage", json);
								if (response.IsSuccessStatusCode)
								{
									await botClient.SendTextMessageAsync(chatId, "I remembered!");
								}
								else
								{
									var responseString = response.Content.ReadAsStringAsync().Result;
									await botClient.SendTextMessageAsync(chatId, responseString);
								}
							}
							catch (Exception ex)
							{
								await botClient.SendTextMessageAsync(chatId, ex.Message);
							}
							break;
						}
				}
			}
		}

		Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
		{
			var ErrorMessage = exception switch
			{
				ApiRequestException apiRequestException
					=> $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
				_ => exception.ToString()
			};

			_logger.LogInformation(ErrorMessage);
			return Task.CompletedTask;
		}
	}
}