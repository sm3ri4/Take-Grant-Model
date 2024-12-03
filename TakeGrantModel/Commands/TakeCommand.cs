using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeGrantModel.Monitors;

namespace TakeGrantModel.Commands{
    public class TakeCommand : ICommand{
        private ClassicMonitor monitor = ClassicMonitor.GetMonitor();
        public void Execute(string[] args){
            var from = monitor.GetComponentByName(args[0]);
            var rule = args[2].GetOperationByString()[0];
            var target = monitor.GetComponentByName(args[4]);
            var to = monitor.GetComponentByName(args[6]);

            monitor.Take(from, to, target, rule);
        }
    }
}
