using REST_API_app.Models;
using System;
using System.Collections.Generic;

namespace REST_API_app.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, HowTo = "Some instruction", Line = "Some line", Platform = "Some platfrom" };
        }

        public IEnumerable<Command> GetCommands()
        {
            var commands = new List<Command>()
            {
                new Command { Id = 0, HowTo = "Some instruction1", Line = "Some line1", Platform = "Some platfrom1" },
                new Command { Id = 1, HowTo = "Some instruction2", Line = "Some line2", Platform = "Some platfrom2" },
                new Command { Id = 2, HowTo = "Some instruction3", Line = "Some line3", Platform = "Some platfrom3" }
            };

            return commands;
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command command)
        {
            throw new NotImplementedException();
        }
    }
}
