using System.Threading.Tasks;
using RSTDesktopUI.Models;

namespace RSTDesktopUI.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}