using my_testcsharp0_bot_WebAPI.Domains.Chats;
using System.ComponentModel.DataAnnotations;

namespace my_testcsharp0_bot_WebAPI.Domains.Messages
{
	public class Message
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// Идентификатор чата
		/// </summary>
		public long ChatId { get; set; }
		/// <summary>
		/// Текст сообщения
		/// </summary>
		public string? Text { get; set; }
		/// <summary>
		/// Дата сообщения
		/// </summary>
		public DateTime Date { get; set; }
		public virtual Chat Chat { get; set; } = null!;
	}
}
