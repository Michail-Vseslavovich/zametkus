using Auth_service.application.services;
using Auth_service.domain.entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User_service.infrastructure.Dbcontext;

namespace Auth_service.api.controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService service;
        public UserController(UserDbContext db)
        {
            service = new UserService(db);
        }

        [HttpGet]
        public ActionResult GetByid(int id, string password)
        {
            User u = service.getById(id, password);
            if (u == null)
            {
                return NotFound();
            }
            return Ok(u);
        }


        [HttpGet]
        public ActionResult GetId(string password, string email)
        {
            int id = service.getId(password, email);
            if (id == 0)
            {
                return NotFound();
            }
            return Ok(id);
        }


        [HttpPost]
        public ActionResult Post(User user)
        {
            if (service.Add(user))
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpDelete]
        public ActionResult Delete(int id, string password)
        {
            if ( service.DeleteById(id, password) )
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpPut]
        public ActionResult Put(User user, string password)
        {
            if(service.Update(user, password))
            {
                return Ok();
            }
            return BadRequest();
        }

        
    }
}
