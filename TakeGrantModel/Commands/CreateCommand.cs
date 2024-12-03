using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TakeGrantModel.Monitors;
using TakeGrantModel.SystemComponents;

namespace TakeGrantModel.Commands{
    public class CreateCommand : ICommand{
        private ClassicMonitor monitor = ClassicMonitor.GetMonitor();
        public void Execute(string[] args){
            var from = monitor.GetComponentByName(args[0]);
            var to = new SubjectObject(args[4]);
            var rules = args[2].GetOperationByString();

            monitor.Create(from, to, rules);
        }
    }
}
