using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCI.MamaGuide.Api.Data.Migrations;

/// <inheritdoc />
public partial class SeedingHospitals : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Hospitals",
            columns: ["Id", "Name", "Governorate", "City", "Street", "PhoneNumber", "IsDeleted", "DeletedOnUtc", "CreatedOnUtc", "ModifiedOnUtc"],
            values: new object[,]
            {
     { Guid.NewGuid(), "El Salam Hospital", "Cairo", "Nasr City", "El Nasr Street", "01000000000", false, null, DateTime.UtcNow, null },
     { Guid.NewGuid(), "El Salam Hospital", "Cairo", "Nasr City", "El Nasr Street", "01000000000", false, null, DateTime.UtcNow, null },
     { Guid.NewGuid(), "Hospital 3", "Cairo", "Dokki", "Dokki Street", "01000000001", false, null, DateTime.UtcNow, null },
     { Guid.NewGuid(), "Hospital 4", "Alexandria", "Alexandria", "Alexandria Street", "01000000002", false, null, DateTime.UtcNow, null },
     { Guid.NewGuid(), "Hospital 5", "Giza", "Giza", "Giza Street", "01000000003", false, null, DateTime.UtcNow, null },
     { Guid.NewGuid(), "Hospital 6", "Luxor", "Luxor", "Luxor Street", "01000000004", false, null, DateTime.UtcNow, null },
     { Guid.NewGuid(), "Hospital 7", "Aswan", "Aswan", "Aswan Street", "01000000005", false, null, DateTime.UtcNow, null },
     { Guid.NewGuid(), "Hospital 8", "Assiut", "Assiut", "Assiut Street", "01000000006", false, null, DateTime.UtcNow, null },
     { Guid.NewGuid(), "Hospital 9", "Sohag", "Sohag", "Sohag Street", "01000000007", false, null, DateTime.UtcNow, null },
     { Guid.NewGuid(), "Hospital 10", "Qena", "Qena", "Qena Street", "01000000008", false, null, DateTime.UtcNow, null }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DELETE FROM Hospitals");
    }
}