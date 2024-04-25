using FCI.MamaGuide.Api.Domain.Entities.Core;
using FCI.MamaGuide.Api.Shared.Repositories.BaseRepository;

namespace FCI.MamaGuide.Api.Features.Articles.Repository;

public interface IArticleRepository : IGenericRepository<Article>
{
    Task<IEnumerable<Article>> GetAllNotVerifiedAsync(Guid doctorId, int pageNumber, int pageSize);

    Task<IEnumerable<Article>> GetRejectedAsync(Guid doctorId, int pageNumber, int pageSize);

    Task<int> NotVerifiedCountAsync();

    Task<int> RejectedCountAsync();
}