using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TakeGrantModel.Commands;
using ICommand = TakeGrantModel.Commands.ICommand;

namespace TakeGrantModel.Factory{
    public class CommandFactory{
        public ICommand GetCommand(string command){
            switch (command){
                case "take": return new TakeCommand();
                case "grant": return new GrantCommand();
                case "create": return new CreateCommand();
                case "remove": return new RemoveCommand();
                default: return null;
            }
        }
    }
}
