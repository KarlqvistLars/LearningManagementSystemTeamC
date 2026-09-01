namespace LearningManagementSystemTeamC.Domain.Roles;

public class Role
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    public void Disable()
    {
        IsActive = false;
    }

    public void Enable()
    {
        IsActive = true;
    }

    private Role() { }

    public Role(string name, string code)
    {
        Id = Guid.NewGuid();
        Name = name;
        Code = code;
        IsActive = true;
    }
}
