using System.Collections.Generic;
using System.Threading.Tasks;
using VerasWeb.Models;

namespace VerasWeb.Services
{
    public interface ICosmosDBService
    {
        Task AddItemAsync(Covid19Item item);
        Task DeleteItemAsync(string id);
        Task<Covid19Item> GetItemAsync(string id);
        Task<IEnumerable<Covid19Item>> GetItemsAsync(string queryString);
        Task UpdateItemAsync(string id, Covid19Item item);
    }
}