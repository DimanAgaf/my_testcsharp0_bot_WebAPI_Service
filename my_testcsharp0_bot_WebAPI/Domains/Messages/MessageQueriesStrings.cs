namespace my_testcsharp0_bot_WebAPI.Domains.Messages
{
	public class MessageQueriesStrings
	{
		/// <summary>
		/// Возвращает список всех зарегистрированных сообщений
		/// </summary>
		public static readonly string GetAllMessages =
			"SELECT * FROM dbo.Messages";
		/// <summary>
		/// Возвращает список всех зарегистрированных сообщений принадлежащих к конкретному чату
		/// </summary>
		public static readonly string GetAllMessagesInChat =
			"SELECT * FROM dbo.Messages WHERE ChatId=@ChatId";
		/// <summary>
		/// Добавляет запись о сообщении
		/// </summary>
		public static readonly string CreateMessage =
			"INSERT INTO dbo.Messages(Id,ChatId,Text,Date)VALUES(@Id,@ChatId,@Text,@Date)";
		/// <summary>
		/// Обновляет запись о сообщении
		/// </summary>
		public static readonly string UpdateMessage =
			"UPDATE dbo.Messages SET Text=@Text,Date=@Date WHERE Id=@Id AND ChatId=@ChatId";
		/// <summary>
		/// Удаляет запись о сообщении
		/// </summary>
		public static readonly string DeleteMessage =
			"DELETE FROM dbo.Messages WHERE Id=@Id AND ChatId=@ChatId";
	}
}
