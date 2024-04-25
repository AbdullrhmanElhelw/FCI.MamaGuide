using Microsoft.AspNetCore.Identity;

namespace FCI.MamaGuide.Api.Domain.Base;

public abstract class BaseIdentityEntity : IdentityUser<Guid>, ISoftDeleteEntity, IAuditableEntity
{
    protected BaseIdentityEntity()
    {
        Id = Guid.NewGuid();
        CreatedOnUtc = DateTime.UtcNow;
    }

    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }

    public string City { get; protected set; }
    public string Governorate { get; protected set; }

    public bool Gender { get; protected set; }

    public bool IsDeleted { get; protected set; }

    public DateTime? DeletedOnUtc { get; protected set; }

    public DateTime CreatedOnUtc { get; protected set; }

    public DateTime? ModifiedOnUtc { get; protected set; }
}