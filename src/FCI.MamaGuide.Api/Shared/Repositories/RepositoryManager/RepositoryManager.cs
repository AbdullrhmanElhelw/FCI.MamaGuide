using FCI.MamaGuide.Api.Data;
using FCI.MamaGuide.Api.Features.Articles.Repository;
using FCI.MamaGuide.Api.Features.Hospitals.Repository;
using FCI.MamaGuide.Api.Features.ReviewArticle.Repository;

namespace FCI.MamaGuide.Api.Shared.Repositories.RepositoryManager;

public class RepositoryManager : IRepositoryManager
{
    private readonly MamaGuideDbContext _context;
    private readonly Lazy<IArticleRepository> _articleRepository;
    private readonly Lazy<IHospitalRepository> _hospitalRepository;
    private readonly Lazy<IVerifiedArticleRepository> _verifiedArticleRepository;

    public RepositoryManager(MamaGuideDbContext context)
    {
        _context = context;
        _articleRepository = new(() => new ArticleRepository(_context));
        _hospitalRepository = new(() => new HospitalRepository(_context));
        _verifiedArticleRepository = new(() => new VerifiedArticleRepository(_context));
    }

    public IArticleRepository Articles => _articleRepository.Value;
    public IHospitalRepository Hospitals => _hospitalRepository.Value;
    public IVerifiedArticleRepository VerifiedArticles => _verifiedArticleRepository.Value;

    public void Dispose() => _context.Dispose();

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}