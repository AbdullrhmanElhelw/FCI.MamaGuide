using FCI.MamaGuide.Api.Domain.Base;
using FCI.MamaGuide.Api.Domain.Entities.Core;

namespace FCI.MamaGuide.Api.Domain.Entities.Identity;

public sealed class Admin : BaseIdentityEntity
{
    private readonly List<VerifiedArticle> _verifiedArticles;

    private Admin
      (
      string firstName,
      string lastName,
      string city,
      string governorate,
      bool gender,
      string phoneNumber
      )
    {
        FirstName = firstName;
        LastName = lastName;
        City = city;
        Governorate = governorate;
        Gender = gender;
        PhoneNumber = phoneNumber;
        UserName = firstName + lastName;
        _verifiedArticles = [];
    }

    public IReadOnlyCollection<VerifiedArticle> VerifiedArticles => _verifiedArticles;

    public static Admin Create
      (
        string firstName,
        string lastName,
        string city,
        string governorate,
        bool gender,
        string phoneNumber)
    {
        return new(firstName, lastName, city, governorate, gender, phoneNumber);
    }

    public static Admin Update(Admin admin, string firstName, string lastName, string city, string governorate)
    {
        admin.FirstName = firstName;
        admin.LastName = lastName;
        admin.City = city;
        admin.Governorate = governorate;
        return admin;
    }

    public static Admin Delete(Admin admin)
    {
        admin.IsDeleted = true;
        return admin;
    }
}