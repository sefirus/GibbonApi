using Core.Entities;

namespace Core.Interfaces.Services;

public interface IWorkspaceService
{
    Task<Workspace> CreateWorkspaceAsync(string name, bool isAiEnabled);
}