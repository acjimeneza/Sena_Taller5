using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ms_sena.Infrastructure
{
    public interface IDataBase
    {
        Task<TimeDto> AddTimeAsync(TimeDto time);
        Task<List<TimeDto>> GetAllAsync();
    }
}
