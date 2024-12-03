using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TakeGrantModel.Monitors;

namespace TakeGrantModel.Commands{
    public class GrantCommand : ICommand{
        private ClassicMonitor monitor = ClassicMonitor.GetMonitor();

        public void Execute(string[] args){
            // < name > grant < rule > for < name > to<name>;
            var from = monitor.GetComponentByName(args[0]);
            var rule = args[2].GetOperationByString()[0];
            var to = monitor.GetComponentByName(args[4]);
            var target = monitor.GetComponentByName(args[6]);

            monitor.Grant(from, to, target, rule);
        }
    }
}
