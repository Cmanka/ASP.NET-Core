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
        public Command GetCommandById(int id)
        {
            return context.Commands.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Command> GetCommands()
        {
            return context.Commands.ToList();
        }
    }
}
