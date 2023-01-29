namespace DependencyLoader;

record GroupedDependency(int Level, Dependency[] Dependencies);

public class DependencyLoader
{
    private readonly IList<Dependency> _dependencies;

    public DependencyLoader(IList<Dependency> dependencies)
    {
        _dependencies = dependencies;
    }
    
    private IEnumerable<Dependency> GetAllDependencies(Dependency dependency)
    {
        yield return dependency;
        foreach (var child in dependency.Dependencies.SelectMany(GetAllDependencies))
        {
            yield return child;
        }
    }
    
    private IEnumerable<GroupedDependency> GetGroupedDependencies()
    {
        return _dependencies
            .SelectMany(GetAllDependencies)
            .Where(d => !d.IsLoaded)
            .GroupBy(d => d.Level)
            .Select(g => new GroupedDependency(g.Key, g.ToArray()))
            .OrderByDescending(g => g.Level);
    }

    public void Print()
    {
        var count = 0;
        foreach (var group in GetGroupedDependencies())
        {
            Console.WriteLine($"Level {group.Level}:");
            foreach (var dependency in group.Dependencies)
            {
                Console.WriteLine(dependency.Name);
                count++;
            }

            Console.WriteLine();
        }
        
        Console.WriteLine($"Total dependency: {count}");
    }
    
    public async Task Load()
    {
        foreach (var group in GetGroupedDependencies())
        {
            var tasks = group
                .Dependencies
                .Select(dependency => dependency.Load())
                .ToList();
            
            await Task.WhenAll(tasks);
            
            // Adding multi-threading support is made worse results when testing with fake data.
            
            // await Parallel.ForEachAsync(group.Dependencies, async (dependency, _) =>
            // {
            //     await dependency.Load();
            // });
        }
    }
}
