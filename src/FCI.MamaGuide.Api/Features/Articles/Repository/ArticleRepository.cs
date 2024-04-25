using FCI.MamaGuide.Api.Data;
using FCI.MamaGuide.Api.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Vertical_Slice_Architecture.Shared.Repositories.MainRepository;

namespace FCI.MamaGuide.Api.Features.Articles.Repository;

public class ArticleRepository : GenericRepository<Article>, IArticleRepository
{
    private readonly MamaGuideDbContext _context;

    public ArticleRepository(MamaGuideDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Article>> GetAllNotVerifiedAsync(Guid doctorId, int pageNumber, int pageSize)
    {
        return await _context.Articles
            .Where(x => x.DoctorId == doctorId && !x.IsVerified)
            .OrderByDescending(o => o.CreatedOnUtc)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(x => x.Doctor)
            .Select(x => Article.GetVerifiedArticle(x.Id, x.Title, x.Content, x.IsVerified, x.CreatedOnUtc, x.Doctor.FirstName))
            .ToListAsync();
    }

    public async Task<IEnumerable<Article>> GetRejectedAsync(Guid doctorId, int pageNumber, int pageSize)
    {
        return await _context.Articles
            .Where(x => x.DoctorId == doctorId && x.IsRejected)
            .OrderByDescending(o => o.CreatedOnUtc)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(x => x.Doctor)
            .Select(x => Article.GetRejectedArticle(x.Id, x.Title, x.Content, x.IsRejected, x.CreatedOnUtc, x.Doctor.FirstName))
            .ToListAsync();
    }

    public Task<int> NotVerifiedCountAsync()
        => _context.Articles.CountAsync(x => !x.IsVerified);

    public Task<int> RejectedCountAsync()
        => _context.Articles.CountAsync(x => x.IsRejected);
}