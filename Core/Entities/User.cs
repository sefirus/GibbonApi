using Core.Interfaces;

namespace Core.Entities;

public class User : ICreatableEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public Guid ApplicationRoleId { get; set; }
    public Role ApplicationRole { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset ModifiedDate { get; set; }
}