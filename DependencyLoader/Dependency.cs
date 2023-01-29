namespace DependencyLoader;

public class Dependency
{
    private readonly IList<Dependency> _dependencies = new List<Dependency>();
    public IEnumerable<Dependency> Dependencies => _dependencies;

    public Dependency(string name)
    {
        Name = name;
    }

    public string Name { get; }
    public bool IsLoaded { get; set; }
    public int Level { get; private set; }

    public void AddDependency(Dependency dependency)
    {
        dependency.Level = Level + 1;
        _dependencies.Add(dependency);
    }
    
    public async Task Load()
    {
        await Task.Delay(50);
        IsLoaded = true;

        Console.WriteLine($"Loaded {Name}");
    }
}