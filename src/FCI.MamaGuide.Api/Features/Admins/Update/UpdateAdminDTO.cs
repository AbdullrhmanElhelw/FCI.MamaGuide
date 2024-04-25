namespace FCI.MamaGuide.Api.Features.Admins.Update;

public sealed record class UpdateAdminDTO
     (string FirstName,
      string LastName,
      string City,
      string Governorate);