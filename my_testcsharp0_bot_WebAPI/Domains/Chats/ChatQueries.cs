using Dapper;
using System.Data.SqlClient;

namespace my_testcsharp0_bot_WebAPI.Domains.Chats
{
	public class ChatQueries : IChatQueries
	{
		private IConfiguration _configuration;

		public ChatQueries(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<IEnumerable<Chat>> GetAllChats()
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				return await connection.QueryAsync<Chat>(ChatQueriesStrings.GetAllChats);
			}
		}

		public async Task<Chat> GetChatByIdAndName(Chat chat)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				return await connection.QueryFirstOrDefaultAsync<Chat>(ChatQueriesStrings.GetChatByIdAndName, chat);
			}
		}

		public async Task CreateChat(Chat chat)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				await connection.ExecuteAsync(ChatQueriesStrings.CreateChat, chat);
			}
		}

		public async Task UpdateChat(Chat chat)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				await connection.ExecuteAsync(ChatQueriesStrings.UpdateChat, chat);
			}
		}

		public async Task DeleteChat(Chat chat)
		{
			using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
			{
				await connection.ExecuteAsync("DELETE FROM dbo.Chats WHERE Id=@Id", chat);
			}
		}
	}
}
