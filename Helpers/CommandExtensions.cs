using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopList.Helpers
{
    public static class CommandExtensions
    {
        public static Task ExecuteAsync(this ICommand command, object parameter)
        {
            var tcs = new System.Threading.Tasks.TaskCompletionSource<bool>();
            command.Execute(parameter);
            tcs.SetResult(true);
            return tcs.Task;
        }
    }
}
