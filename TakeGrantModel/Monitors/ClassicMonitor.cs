using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeGrantModel.SystemComponents;
using static TakeGrantModel.OperationType;

namespace TakeGrantModel.Monitors{
    public class ClassicMonitor{

        private Dictionary<(ISystemComponent, ISystemComponent), List<Operation>> matrix;
        private List<ISystemComponent> systemComponents = new List<ISystemComponent>();
        private static ISystemComponent rootSubject = new SubjectObject("X");
        private static ClassicMonitor monitor = new ClassicMonitor();

        private ClassicMonitor(){
            this.matrix = new Dictionary<(ISystemComponent, ISystemComponent), List<Operation>>();
            systemComponents.Add(rootSubject);
        }

        public static ClassicMonitor GetMonitor(){
            if (monitor == null)
                monitor = new ClassicMonitor();
            return monitor;
        }

        public void Take(ISystemComponent from, ISystemComponent to, ISystemComponent target, Operation operation){
            List<Operation> canTakes = matrix[(from, to)];
            List<Operation> accessedRights = matrix[(to, target)];

            if (canTakes.Contains(Operation.Take) && accessedRights.Contains(operation)){
                if (matrix.ContainsKey((from, target)))
                    matrix[(from, target)].Add(operation);
                else
                    matrix.Add((from, target), new List<Operation>() { operation });
            }
            else return;
        }

        public void Grant(ISystemComponent from, ISystemComponent to, ISystemComponent target, Operation operation){
            List<Operation> canGrants = matrix[(from, to)];
            List<Operation> accessedRights = matrix[(from, target)];
            
            if (canGrants.Contains(Operation.Grant) && accessedRights.Contains(operation)){
                if (matrix.ContainsKey((to, target)))
                    matrix[(to, target)].Add(operation);
                else
                    matrix.Add((to, target), new List<Operation>() { operation });
            }
            else return;
        }

        public void Create(ISystemComponent from, ISystemComponent to, List<Operation> operation){
            if (IsExist(from) && !IsExist(to)){
                systemComponents.Add(to);
                matrix[(from, to)] = new List<Operation>(operation);
            }
        }

        public void Remove(ISystemComponent from, ISystemComponent to, Operation operation){
            if (IsExist(from, to) && matrix[(from, to)].Contains(operation)){
                matrix[(from, to)].Remove(operation);

                if (matrix[(from, to)].Count() == 0)
                    matrix.Remove((from, to));
            }
        }

        public bool IsExist(ISystemComponent component){
            return systemComponents.Contains(component);
        }

        public bool IsExist(ISystemComponent from, ISystemComponent to){
            return matrix.ContainsKey((from, to));
        }

        public List<string> GetComponentNames(){
            return systemComponents.Cast<SubjectObject>().Select(x => x.name).ToList();
        }

        public void GetMatrix(){
            Console.WriteLine(string.Join("\n", matrix.Select(x => $"{(x.Key.Item1 as SubjectObject).name} --> {(x.Key.Item2 as SubjectObject).name} : {string.Join(", ", x.Value)}")));
        }

        public ISystemComponent? GetComponentByName(string name){
            return systemComponents.Cast<SubjectObject>().FirstOrDefault(x => x.name == name);
        }

        public Dictionary<(ISystemComponent,ISystemComponent),List<Operation>> GetMatrixDict{
            get{
                return this.matrix;
            }
        }
    }
}
