using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Zametki_service.application.services;
using Zametki_service.domain.entity;
using Zametki_service.infrastructure.DBcontext;

namespace Zametki_service.api.controllers
{


    // ДОБАВЬ ПРОВЕРКУ НА ТО ЧТО ЭТО ТОТ САМЫЙ ЮЗЕР
    [Route("api/[controller]")]
    [ApiController]
    public class ZametkiController : ControllerBase
    {
        private NoteService noteService;
        public ZametkiController(NoteBDcontext db)
        {
            noteService = new NoteService(db);
        }

        [Authorize]
        [HttpGet("NoteId")]
        public ActionResult Get(int id)
        {
            int userId = JwtDecodeService.GetJwtId(Request.Headers["Authorization"]);
            Note note = noteService.getById(id,userId);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [Authorize]
        [HttpGet("UserId")]
        public ActionResult GetById()
        {
            int userId = JwtDecodeService.GetJwtId(Request.Headers["Authorization"]);
            List<Note> Notes = noteService.GetAll(userId);
            if (!Notes.Any())
            {
                return NotFound();
            }
            return Ok(Notes);
        }

        [Authorize]
        [HttpPost("NewNote")]
        public ActionResult Post(Note note)
        {
            int userId = JwtDecodeService.GetJwtId(Request.Headers["Authorization"]);
            if (noteService.Add(note,userId))
            {
                return Ok();
            }
            return BadRequest();
        }


        [Authorize]
        [HttpDelete("Delete")]
        public ActionResult Delete(int id)
        {
            int userId = JwtDecodeService.GetJwtId(Request.Headers["Authorization"]);
            
            if (noteService.DeleteById(id, userId))
            {
                return Ok();
            }
            return BadRequest();

        }
    }
}
