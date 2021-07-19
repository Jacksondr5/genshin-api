using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBaseRepository
    {
        Task<int?> GetMaxId();
    }
}