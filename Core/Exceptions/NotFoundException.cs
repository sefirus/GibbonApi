using System.Reflection;

namespace Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entityType) : base($"Requested {entityType} not found") { }
}