using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TakeGrantModel.SystemComponents;
using static TakeGrantModel.OperationType;

namespace TakeGrantModel.Monitors{
    public class ExtendedMonitor{
        private Dictionary<(ISystemComponent, ISystemComponent), Operation> imaginaryMatrix;
        private readonly Dictionary<(ISystemComponent, ISystemComponent), List<Operation>> matrix 
            = ClassicMonitor.GetMonitor().GetMatrixDict;
        private static ExtendedMonitor monitor = new ExtendedMonitor();

        private ExtendedMonitor(){
            this.imaginaryMatrix = new Dictionary<(ISystemComponent, ISystemComponent), Operation>();
        }

        public static ExtendedMonitor GetMonitor(){
            if (monitor == null)
                return new ExtendedMonitor();
            return monitor;
        }

        public void Command1(ISystemComponent from, ISystemComponent to){
            if(matrix.ContainsKey((from,to)) && matrix[(from, to)].Contains(Operation.Read))
                imaginaryMatrix.Add((to, from), Operation.Write);
        }

        public void Command2(ISystemComponent from, ISystemComponent to){
            if (matrix.ContainsKey((from, to)) && matrix[(from, to)].Contains(Operation.Write))
                imaginaryMatrix.Add((to, from), Operation.Read);
        }

        public void Post(ISystemComponent readSubject, ISystemComponent sysObject, ISystemComponent writeSubject){
            if(matrix.ContainsKey((readSubject,sysObject)) && matrix[(readSubject,sysObject)].Contains(Operation.Read)
              && matrix.ContainsKey((writeSubject,sysObject)) && matrix[(writeSubject, sysObject)].Contains(Operation.Write))
            {
                imaginaryMatrix.Add((writeSubject, readSubject), Operation.Write);
                imaginaryMatrix.Add((readSubject, writeSubject), Operation.Read);
            }
        }

        public void Spy(ISystemComponent xRead, ISystemComponent yRead, ISystemComponent sysObject){
            if(matrix.ContainsKey((xRead,yRead)) && matrix[(xRead,yRead)].Contains(Operation.Read)
              && matrix.ContainsKey((yRead,sysObject)) && matrix[(yRead, sysObject)].Contains(Operation.Read))
            {
                imaginaryMatrix.Add((sysObject, xRead), Operation.Write);
                imaginaryMatrix.Add((xRead, sysObject), Operation.Read);
            }
        }

        public void Find(ISystemComponent xWrite, ISystemComponent yWrite, ISystemComponent sysObject){
            if (matrix.ContainsKey((xWrite, yWrite)) && matrix[(xWrite, yWrite)].Contains(Operation.Write)
              && matrix.ContainsKey((yWrite, sysObject)) && matrix[(yWrite, sysObject)].Contains(Operation.Write))
            {
                imaginaryMatrix.Add((sysObject, xWrite), Operation.Read);
                imaginaryMatrix.Add((xWrite, sysObject), Operation.Write);
            }
        }

        public void Pass(ISystemComponent xObject, ISystemComponent ySubject, ISystemComponent zObject){
            if(matrix.ContainsKey((ySubject,xObject)) && matrix[(ySubject, xObject)].Contains(Operation.Write)
              && matrix.ContainsKey((ySubject,zObject)) && matrix[(ySubject, zObject)].Contains(Operation.Read))
            {
                imaginaryMatrix.Add((zObject, xObject), Operation.Write);
                imaginaryMatrix.Add((xObject, zObject), Operation.Read);
            }
        }
    }
}
