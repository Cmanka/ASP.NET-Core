using System.Collections.Generic;
using REST_API_app.Models;

namespace REST_API_app.Data
{
    public interface ICommanderRepo
    {
        IEnumerable<Command> GetCommands();
        Command GetCommandById(int id);
    }
}
