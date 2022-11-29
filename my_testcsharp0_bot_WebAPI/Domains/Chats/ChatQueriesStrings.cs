namespace my_testcsharp0_bot_WebAPI.Domains.Chats
{
	public class ChatQueriesStrings
	{
		/// <summary>
		/// Возвращает список зарегистрированных чатов
		/// </summary>
		public static readonly string GetAllChats = 
			"SELECT * FROM dbo.Chats";
		/// <summary>
		/// Возвращает записи о чате
		/// </summary>
		public static readonly string GetChatByIdAndName = 
			"SELECT * FROM dbo.Chats WHERE Id = @Id OR Username = @Username";
		/// <summary>
		/// Добавляет запись о новом чате
		/// </summary>
		public static readonly string CreateChat = 
			"INSERT INTO dbo.Chats(Id,Username,FirstName,LastName)VALUES(@Id,@Username,@FirstName,@LastName)";
		/// <summary>
		/// Обновляет заапись о чате
		/// </summary>
		public static readonly string UpdateChat =
			"UPDATE dbo.Chats SET FirstName=@FirstName,LastName=@LastName WHERE Id=@Id AND Username=@Username";
		/// <summary>
		/// Удаляет заапись о чате
		/// </summary>
		public static readonly string DeleteChat =
			"DELETE FROM dbo.Chats WHERE Id=@Id";
	}
}
