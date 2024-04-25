namespace FCI.MamaGuide.Api.Domain.Base;

public abstract class BaseEntity : IEquatable<BaseEntity>, ISoftDeleteEntity, IAuditableEntity
{
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedOnUtc = DateTime.UtcNow;
    }

    public Guid Id { get; protected set; }

    public bool IsDeleted { get; protected set; }

    public DateTime? DeletedOnUtc { get; protected set; }

    public DateTime CreatedOnUtc { get; protected set; }

    public DateTime? ModifiedOnUtc { get; protected set; }

    public bool Equals(BaseEntity? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((BaseEntity)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() ^ 31;
    }
}