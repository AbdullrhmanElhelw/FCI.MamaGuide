using FCI.MamaGuide.Api.Data;
using FCI.MamaGuide.Api.Domain.Entities.Core;
using Vertical_Slice_Architecture.Shared.Repositories.MainRepository;

namespace FCI.MamaGuide.Api.Features.ReviewArticle.Repository;

public class VerifiedArticleRepository : GenericRepository<VerifiedArticle>, IVerifiedArticleRepository
{
    public VerifiedArticleRepository(MamaGuideDbContext context) : base(context)
    {
    }
}