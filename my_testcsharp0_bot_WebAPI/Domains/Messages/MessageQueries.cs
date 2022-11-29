using Dapper;
using my_testcsharp0_bot_WebAPI.Domains.Chats;
using System.Data.SqlClient;

namespace my_testcsharp0_bot_WebAPI.Domains.Messages
{
	public class MessageQueries : IMessageQueries
	{
		private IConfiguration _configuration;

		public MessageQueries(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<IEnumerable<Message>> GetAllMessages()
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				return await connection.QueryAsync<Message>(MessageQueriesStrings.GetAllMessages);
			}
		}

		public async Task<IEnumerable<Message>> GetAllMessagesInChat(long chatId)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				return await connection.QueryAsync<Message>(MessageQueriesStrings.GetAllMessagesInChat, new { ChatId = chatId });
			}
		}

		public async Task CreateMessage(Message message)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				await connection.ExecuteAsync(MessageQueriesStrings.CreateMessage, message);
			}
		}

		public async Task UpdateMessage(Message message)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				await connection.ExecuteAsync(MessageQueriesStrings.UpdateMessage, message);
			}
		}

		public async Task DeleteMessage(Message message)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				await connection.ExecuteAsync(MessageQueriesStrings.DeleteMessage, message);
			}
		}
	}
}
