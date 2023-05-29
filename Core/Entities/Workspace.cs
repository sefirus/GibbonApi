namespace Core.Entities;

public class Workspace
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
    public bool IsAiEnabled { get; set; }

    // Navigation properties
    public User Owner { get; set; }
}