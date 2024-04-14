namespace Core.ViewModels.Workspace;

public class ReadWorkspaceViewModel : ReadWorkspaceShortViewModel
{
    public string AccessLevel { get; set; }
    public int NumOfSchemaObjects { get; set; }
    public int NumOfDocuments { get; set; }
}