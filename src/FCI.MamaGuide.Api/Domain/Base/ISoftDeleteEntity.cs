namespace FCI.MamaGuide.Api.Domain.Base;

public interface ISoftDeleteEntity
{
    bool IsDeleted { get; }
    DateTime? DeletedOnUtc { get; }
}