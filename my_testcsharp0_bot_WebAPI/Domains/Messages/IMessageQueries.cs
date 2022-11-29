namespace my_testcsharp0_bot_WebAPI.Domains.Messages
{
	public interface IMessageQueries
	{
		/// <summary>
		/// Возвращает список всех зарегистрированных сообщений
		/// </summary>
		/// <returns>Список <typeparamref name="Message"/></returns>
		Task<IEnumerable<Message>> GetAllMessages();
		/// <summary>
		/// Возвращает список всех зарегистрированных сообщений принадлежащих к конкретному чату
		/// </summary>
		/// <param name="chatId">Идентификатор чата</param>
		/// <returns>Список <typeparamref name="Message"/></returns>
		Task<IEnumerable<Message>> GetAllMessagesInChat(long chatId);
		/// <summary>
		/// Добавляет запись о сообщении
		/// </summary>
		/// <param name="message">Данные о сообщении</param>
		Task CreateMessage(Message message);
		/// <summary>
		/// Обновляет запись о сообщении
		/// </summary>
		/// <param name="message">Данные о сообщении</param>
		Task UpdateMessage(Message message);
		/// <summary>
		/// Удаляет запись о сообщении
		/// </summary>
		/// <param name="message">Данные о сообщении</param>
		Task DeleteMessage(Message message);
	}
}
