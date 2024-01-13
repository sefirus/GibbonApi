namespace Core.Enums;

public static class RolesEnum
{
    /// <summary>
    /// God-alike role for dev team
    /// </summary>
    public const string SuperUser = "SuperUser";
    /// <summary>
    /// Regular clients
    /// </summary>
    public const string RegularUser = "RegularUser";
    /// <summary>
    /// Permissions to delete workspace 
    /// </summary>
    public const string Owner = "Owner";
    /// <summary>
    /// Permissions to schema modifying and generating data on the Workspace
    /// </summary>
    public const string Admin = "Admin";
    /// <summary>
    /// Permissions to read-write operations on the Workspace
    /// </summary>
    public const string General = "General";
    /// <summary>
    /// To remove the role from the user
    /// </summary>
    public const string None = "None";

    public static List<string> WorkspaceRoles = new (){ Owner, Admin, General };
}