using System.ComponentModel.DataAnnotations;

namespace my_testcsharp0_bot_WebAPI.Domains.Chats
{
	public class Chat
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public long Id { get; set; }
		/// <summary>
		/// Имя пользователя
		/// </summary>
		public string Username { get; set; } = null!;
		/// <summary>
		/// Имя
		/// </summary>
		public string? FirstName { get; set; }
		/// <summary>
		/// Фамилия
		/// </summary>
		public string? LastName { get; set; }
	}
}
