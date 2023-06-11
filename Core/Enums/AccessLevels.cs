using static Core.Enums.RolesEnum;

namespace Core.Enums;

public static class AccessLevels
{
    public const string GeneralAccess = $"{SuperUser}, {Owner}, {Admin}, {General}";
    public const string AdminAccess = $"{SuperUser}, {Owner}, {Admin}";
    public const string OwnerAccess = $"{SuperUser}, {Owner}";
}