namespace my_testcsharp0_bot_WebAPI.Domains.Chats
{
	public interface IChatQueries
	{
		/// <summary>
		/// Возвращает список зарегистрированных чатов
		/// </summary>
		/// <returns>Список <typeparamref name="Chat"/></returns>
		Task<IEnumerable<Chat>> GetAllChats();
		/// <summary>
		/// Возвращает записи о чате
		/// </summary>
		/// <param name="chat">Данные о чате</param>
		/// <returns><typeparamref name="Chat"/></returns>
		Task<Chat> GetChatByIdAndName(Chat chat);
		/// <summary>
		/// Добавляет запись о новом чате 
		/// </summary>
		/// <param name="chat">Данные о чате</param>
		Task CreateChat(Chat chat);
		/// <summary>
		/// Обновляет запись о чате
		/// </summary>
		/// <param name="chat">Данные о чате</param>
		Task UpdateChat(Chat chat);
		/// <summary>
		/// Удаляет запись о чате
		/// </summary>
		/// <param name="chat">Данные о чате</param>
		Task DeleteChat(Chat chat);
	}
}
