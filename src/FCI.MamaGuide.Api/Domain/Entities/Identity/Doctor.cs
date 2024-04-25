using FCI.MamaGuide.Api.Domain.Base;
using FCI.MamaGuide.Api.Domain.Entities.Core;

namespace FCI.MamaGuide.Api.Domain.Entities.Identity;

public sealed class Doctor : BaseIdentityEntity
{
    private readonly List<Article> _articles;

    private Doctor()
    { }

    private Doctor(
        string firstName,
        string lastName,
        string city,
        string governorate,
        bool gender,
        string specialization,
        string phoneNumber,
        Hospital hospital)
    {
        FirstName = firstName;
        LastName = lastName;
        City = city;
        Governorate = governorate;
        Gender = gender;
        Specialization = specialization;
        PhoneNumber = phoneNumber;
        Hospital = hospital;
        _articles = [];
        UserName = firstName.Substring(0, 1) + lastName + phoneNumber.Substring(0, 3);
    }

    private Doctor(string firstName) => FirstName = firstName;

    public string Specialization { get; private set; }

    public Guid HospitalId { get; private set; }
    public Hospital Hospital { get; private set; }

    public IReadOnlyCollection<Article> Articles => _articles;

    public static Doctor Create(string firstName,
                                string lastName,
                                string city,
                                string governorate,
                                bool gender,
                                string specialization,
                                string phoneNumber,
                                Hospital hospital)
    {
        return new(firstName, lastName, city, governorate, gender, specialization, phoneNumber, hospital);
    }

    public static Doctor Select(string firstName)
    {
        return new(firstName);
    }

    public static Doctor Update(Doctor doctor,
                                string firstName,
                                string lastName,
                                string city,
                                string governorate,
                                string specialization)
    {
        doctor.FirstName = firstName;
        doctor.LastName = lastName;
        doctor.City = city;
        doctor.Governorate = governorate;
        doctor.Specialization = specialization;
        return doctor;
    }

    public static Doctor Delete(Doctor doctor)
    {
        doctor.IsDeleted = true;
        return doctor;
    }
}