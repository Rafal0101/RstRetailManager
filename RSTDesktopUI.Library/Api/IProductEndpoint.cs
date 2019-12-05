using System.Collections.Generic;
using System.Threading.Tasks;
using RSTDesktopUI.Library.Models;

namespace RSTDesktopUI.Library.Api
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
    }
}