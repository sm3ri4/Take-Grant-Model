using ICommand = TakeGrantModel.Commands.ICommand;
using TakeGrantModel.Factory;
using TakeGrantModel.Monitors;
using TakeGrantModel.SystemComponents;
using static TakeGrantModel.OperationType;

namespace TakeGrantModel{
    public class Program{
        private static ClassicMonitor monitor = ClassicMonitor.GetMonitor();

        static void Main(string[] args){
            while (true){
                Console.WriteLine();    
                string prompt = "1.Show matrix\n2.Execute command\n3.Show all objects/subjects\n4.Clear console\n5.Show commands";
                Console.Write(prompt + "\nEnter: ");
                string? result = Console.ReadLine();

                switch (result){
                    case "1":{
                            Console.WriteLine();
                            monitor.GetMatrix();
                            break;
                        }
                    case "2":{
                            Console.WriteLine();
                            monitor.GetMatrix();
                            Console.Write("\nCommand: ");

                            string? command = Console.ReadLine();
                            string[] parts = command.Split(' ');

                            CommandFactory factory = new CommandFactory();
                            ICommand currentCommand = factory.GetCommand(parts[1]);
                            currentCommand.Execute(parts);
                            break;
                        }
                    case "3":{
                            Console.WriteLine("\n" + string.Join("\n", monitor.GetComponentNames()));
                            break;
                        }
                    case "4":{
                            Console.Clear();
                            break;
                        }
                    case "5":{
                            var lines = File.ReadAllLines("C:\\Users\\Eugene\\source\\repos\\TakeGrantModel\\TakeGrantModel\\commands.cfg");
                            Console.WriteLine("\n" + string.Join("\n", lines));
                            break;
                        }
                    default: break;
                }
            }
        }
    }

    public static class StringExtension{
        public static List<Operation> GetOperationByString(this string str){
            List<Operation> result = new List<Operation>();
            string hashString = string.Join("", str.ToHashSet());

            for(int i = 0; i < hashString.Length; i++){
                char letter = hashString[i];
                switch (letter){
                    case 'r':{
                            result.Add(Operation.Read);
                            break;
                        }
                    case 't':{
                            result.Add(Operation.Take);
                            break;
                        }
                    case 'w':{
                        result.Add(Operation.Write);
                        break;
                    }
                    case 'g':{
                            result.Add(Operation.Grant);
                            break;
                        }
                    default: break;
                }
            }

            return result;
        }
    }
}