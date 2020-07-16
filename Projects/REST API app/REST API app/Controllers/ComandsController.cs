using Microsoft.AspNetCore.Mvc;
using REST_API_app.Data;
using REST_API_app.Models;
using System.Collections.Generic;

namespace REST_API_app.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class ComandsController : Controller
    {
        private readonly ICommanderRepo repository;

        public ComandsController(ICommanderRepo repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commandItems = repository.GetCommands();

            return Ok(commandItems);
        }

        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandById(int id)
        {
            var commandItem = repository.GetCommandById(id);

            return Ok(commandItem);
        }
    }
}
