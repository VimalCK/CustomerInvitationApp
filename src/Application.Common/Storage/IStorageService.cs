using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common
{
    public interface IStorageService{

        Task<IEnumerable<T>> GetDataAsync<T>();
        Task<bool> SaveDataAsync<T>(IEnumerable<T> data);
    }
}