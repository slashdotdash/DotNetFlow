namespace DotNetFlow.Utils
{
    public class Program
    {
        static void Main(string[] args)
        {           
            new SerializeCommands().Execute();
            new ImportCommands().Execute();
        }
    }
}