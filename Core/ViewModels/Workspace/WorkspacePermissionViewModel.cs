namespace Core.ViewModels.Workspace;

public class WorkspacePermissionViewModel : AssignPermissionViewModel
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
}