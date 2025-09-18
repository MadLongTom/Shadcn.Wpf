namespace Shadcn.Wpf.Models;

/// <summary>
/// Person model for forms demonstration
/// </summary>
public class Person
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    
    public override bool Equals(object? obj)
    {
        return obj is Person person && Id == person.Id;
    }
    
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
    
    public override string ToString()
    {
        return Name;
    }
}
