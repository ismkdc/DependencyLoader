using System.Diagnostics;
using DependencyLoader;

const int count = 2;

var dependencies = FakeDataGenerator.Generate(count);
var dependencyLoader = new DependencyLoader.DependencyLoader(dependencies);

Console.WriteLine(">>PRINTING DEPENDENCIES BY LEVELS<<");
dependencyLoader.Print();

var sw = Stopwatch.StartNew();
Console.WriteLine(">>LOADING DEPENDENCIES<<");
await dependencyLoader.Load();
Console.WriteLine($"Loaded in {sw.ElapsedMilliseconds}ms");