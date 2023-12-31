using RandomNameGenerator.Application.DTO;

namespace RandomNameGenerator.Application.Services
{
    public interface IName
    {
        public Task<List<NamesDTO>> GetNames(bool id);
    }
}
