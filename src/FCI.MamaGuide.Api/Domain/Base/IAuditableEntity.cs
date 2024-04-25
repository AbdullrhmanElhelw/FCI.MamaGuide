namespace FCI.MamaGuide.Api.Domain.Base;

public interface IAuditableEntity
{
    DateTime CreatedOnUtc { get; }

    DateTime? ModifiedOnUtc { get; }
}