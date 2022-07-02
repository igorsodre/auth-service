namespace Server.Models.Domain;

public class User
{
    public string Id { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public uint TokenVersion { get; init; }

    public string UserName { get; init; } = string.Empty;
}
