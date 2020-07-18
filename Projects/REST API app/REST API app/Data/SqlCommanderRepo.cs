using REST_API_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace REST_API_app.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly MyAppContext context;

        public SqlCommanderRepo(MyAppContext context)
        {
            this.context = context;
        }

        public void CreateCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            context.Commands.Add(command);
        }

        public void DeleteCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            context.Commands.Remove(command);
        }

        public Command GetCommandById(int id)
        {
            return context.Commands.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Command> GetCommands()
        {
            return context.Commands.ToList();
        }

        public bool SaveChanges()
        {
            return (context.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command command)
        {
            
        }
    }
}
