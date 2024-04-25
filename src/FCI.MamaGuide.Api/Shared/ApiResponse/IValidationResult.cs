namespace FCI.MamaGuide.Api.Shared.ApiResponse;

public interface IValidationResult
{
    public static readonly Error ValidationError = new(
       nameof(ValidationError));

    Error[] Errors { get; }
}