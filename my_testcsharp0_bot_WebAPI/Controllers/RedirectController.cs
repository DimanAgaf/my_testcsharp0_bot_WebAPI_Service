using Microsoft.AspNetCore.Mvc;
using my_testcsharp0_bot_WebAPI.Domains.Chats;
using my_testcsharp0_bot_WebAPI.Domains.Messages;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace my_testcsharp0_bot_WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RedirectController : ControllerBase
	{
		private readonly ILogger<RedirectController> _logger;
		private IChatQueries _chatQueries;
		private IMessageQueries _messageQueries;

		public RedirectController(ILogger<RedirectController> logger, IChatQueries chatQueries, IMessageQueries messageQueries)
		{
			_logger = logger;
			_chatQueries = chatQueries;
			_messageQueries = messageQueries;
		}

		// GET api/<RedirectController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<IEnumerable<Message>>> GetMessageInChat(long id)
		{
			try
			{
				IEnumerable<Message> allMessages = await _messageQueries.GetAllMessagesInChat(id);
				return Ok(allMessages);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		// POST api/<RedirectController>/CreateChat
		[HttpPost("CreateChat")]
		public async Task<ActionResult> CreateChat([FromBody] Chat chat)
		{
			try
			{
				var filter = await _chatQueries.GetChatByIdAndName(chat);
				if (filter?.Id == chat.Id || filter?.Username == chat.Username)
				{
					return BadRequest("You are already registered!");
				}
				else
				{
					await _chatQueries.CreateChat(chat);
					return Ok();
				}				
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);	
			}
		}

		// POST api/<RedirectController>/CreateMessage
		[HttpPost("CreateMessage")]
		public async Task<ActionResult> CreateMessage([FromBody] Message message)
		{
			try
			{
				await _messageQueries.CreateMessage(message);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
