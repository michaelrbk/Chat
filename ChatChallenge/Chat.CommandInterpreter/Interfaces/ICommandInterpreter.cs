using System.Threading.Tasks;

namespace Chat.QueueManager.Interfaces
{
    public interface ICommandInterpreter
    {
        Task InterpretCommand(string message);
    }
}
