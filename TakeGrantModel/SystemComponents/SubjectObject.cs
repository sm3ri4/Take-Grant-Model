using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeGrantModel.Monitors;
using static TakeGrantModel.OperationType;

namespace TakeGrantModel.SystemComponents{
    public class SubjectObject : ISystemComponent{
        public string name;

        public SubjectObject(string name){
            this.name = name;
        }

    }
}
