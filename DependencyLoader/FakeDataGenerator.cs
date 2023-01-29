using System.Text;

namespace DependencyLoader;

public static class FakeDataGenerator
{
    public static IList<Dependency> Generate(int count)
    {
        var dependencies = new List<Dependency>();

        for (var i = 0; i < count; i++)
        {
            var dependency = new Dependency(RandomNameGenerator());
            AddDependencies(dependency, count);
            dependencies.Add(dependency);
        }

        return dependencies;
    }

    private static void AddDependencies(Dependency dependency, int count)
    {
        if (count == 0)
        {
            return;
        }

        for (var i = 0; i < count; i++)
        {
            var childDependency = new Dependency(RandomNameGenerator());
            dependency.AddDependency(childDependency);
            AddDependencies(childDependency, count - 1);
        }
    }

    private static string RandomNameGenerator(int length = 10)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        var sb = new StringBuilder();
        for (var i = 0; i < length; i++)
        {
            sb.Append(chars[Random.Shared.Next(chars.Length)]);
        }

        return sb.ToString();
    }
}