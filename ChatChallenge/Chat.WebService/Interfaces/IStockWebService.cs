using Chat.Models.Stock;
using System.Threading.Tasks;

namespace Chat.WebService.Interfaces
{
    public interface IStockWebService
    {
        Task<Stock> GetStock(StockRequest request);
    }
}
