using System.Threading.Tasks;
using RSTDesktopUI.Library.Models;

namespace RSTDesktopUI.Library.Api
{
    public interface ISaleEndpoint
    {
        Task PostSale(SaleModel sale);
    }
}