using my_testcsharp0_bot_WebAPI.Domains.Chats;
using my_testcsharp0_bot_WebAPI.Domains.Messages;

namespace my_testcsharp0_bot_WebAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddHostedService<TelegramBotWorker>();

			builder.Services.AddControllers();
			builder.Services.AddTransient<IChatQueries, ChatQueries>();
			builder.Services.AddTransient<IMessageQueries, MessageQueries>();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}