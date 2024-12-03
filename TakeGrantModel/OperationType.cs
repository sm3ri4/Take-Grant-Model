using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeGrantModel{
    public class OperationType{
        public enum Operation{
            Read,
            Write,
            Take,
            Grant
        }
    }
}
