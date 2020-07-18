using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = repository.GetCommandById(id);
            if(commandItem!=null)
                return Ok(mapper.Map<CommandReadDto>(commandItem));

            return NotFound();
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = mapper.Map<Command>(commandCreateDto);
            repository.CreateCommand(commandModel);
            repository.SaveChanges();

            var commandReadDto = mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id },commandReadDto);
            //return Ok(commandModel);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = repository.GetCommandById(id);
            if (commandModelFromRepo == null)
                return NotFound();

            mapper.Map(commandUpdateDto, commandModelFromRepo);

            repository.UpdateCommand(commandModelFromRepo);
            repository.SaveChanges();
            return NoContent();

        }
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = repository.GetCommandById(id);
            if (commandModelFromRepo == null)
                return NotFound();

            var commandToPatch = mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch,ModelState);
            if (!TryValidateModel(commandToPatch))
                return ValidationProblem(ModelState);

            mapper.Map(commandToPatch, commandModelFromRepo);

            repository.UpdateCommand(commandModelFromRepo);
            repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = repository.GetCommandById(id);
            if (commandModelFromRepo == null)
                return NotFound();

            repository.DeleteCommand(commandModelFromRepo);
            repository.SaveChanges();

            return NoContent();
        }
    }
}
