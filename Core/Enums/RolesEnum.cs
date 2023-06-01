namespace Core.Enums;

public static class RolesEnum
{
    /// <summary>
    /// God-alike role for dev team
    /// </summary>
    public static string SuperUser = "SuperUser";
    /// <summary>
    /// Regular clients
    /// </summary>
    public static string RegularUser = "RegularUser";
    /// <summary>
    /// Permissions to delete workspace 
    /// </summary>
    public static string Owner = "Owner";
    /// <summary>
    /// Permissions to schema modifying and generating data on the Workspace
    /// </summary>
    public static string Admin = "Admin";
    /// <summary>
    /// Permissions to read-write operations on the Workspace
    /// </summary>
    public static string Regular = "Regular";
}