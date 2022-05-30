using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IPeopleService
    {
        Task<bool> InsertAsync(People person);
        Task<bool> InsertListAsync();
    }
}
