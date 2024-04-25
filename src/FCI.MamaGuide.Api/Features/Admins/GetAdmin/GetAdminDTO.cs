namespace FCI.MamaGuide.Api.Features.Admins.GetAdmin;

public sealed record GetAdminDTO
    (Guid Id,
     string FirstName,
     string LastName,
     string PhoneNumber,
     DateTime CreatedOn);