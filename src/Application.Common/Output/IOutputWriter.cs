using System.Threading.Tasks;

namespace Application.Common
{
    public interface IOutputWriter
    {
      Task<bool> CreateOutputAsync<T>(T[] data);  
    } 
}