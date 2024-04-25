using FCI.MamaGuide.Api.Features.Articles.Repository;
using FCI.MamaGuide.Api.Features.Hospitals.Repository;
using FCI.MamaGuide.Api.Features.ReviewArticle.Repository;

namespace FCI.MamaGuide.Api.Shared.Repositories.RepositoryManager;

public interface IRepositoryManager : IDisposable
{
    IArticleRepository Articles { get; }

    IHospitalRepository Hospitals { get; }

    IVerifiedArticleRepository VerifiedArticles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}