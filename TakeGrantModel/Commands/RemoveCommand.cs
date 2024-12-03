using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeGrantModel.Monitors;

namespace TakeGrantModel.Commands{
    public class RemoveCommand : ICommand{
        private ClassicMonitor monitor = ClassicMonitor.GetMonitor();
        public void Execute(string[] args){
            var from = monitor.GetComponentByName(args[0]);
            var rule = args[2].GetOperationByString()[0];
            var to = monitor.GetComponentByName(args[4]);

            monitor.Remove(from, to, rule);
        }
    }
}
