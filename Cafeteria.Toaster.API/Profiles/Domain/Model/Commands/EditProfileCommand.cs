namespace Cafeteria.Toaster.API.Profiles.Domain.Model.Commands;

public record EditProfileCommand(int Id, string ProfileUrl, string Description, string BackgroundUrl);