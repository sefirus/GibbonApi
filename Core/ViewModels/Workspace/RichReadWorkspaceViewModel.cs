using Core.ViewModels.Schema;

namespace Core.ViewModels.Workspace;

public class RichReadWorkspaceViewModel : ReadWorkspaceViewModel
{
    public IEnumerable<SchemaObjectViewModel> SchemaObjects { get; set; }
    public IEnumerable<WorkspacePermissionViewModel> Permissions { get; set; }
}