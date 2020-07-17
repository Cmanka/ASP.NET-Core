using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using REST_API_app.Data;
using REST_API_app.Dtos;
using REST_API_app.Models;
using System.Collections.Generic;

namespace REST_API_app.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class ComandsController : Controller
    {
        private readonly ICommanderRepo repository;
        private readonly IMapper mapper;

        public ComandsController(ICommanderRepo repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = repository.GetCommands();

            return Ok(mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        [HttpGet("{id}")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = repository.GetCommandById(id);
            if(commandItem!=null)
                return Ok(mapper.Map<CommandReadDto>(commandItem));

            return NotFound();
        }
    }
}
