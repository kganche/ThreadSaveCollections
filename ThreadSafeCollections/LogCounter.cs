using System.Collections.Concurrent;

namespace ThreadSafeCollections;

public class LogCounter
{
    private readonly ConcurrentDictionary<string, int> counts = [];

    public void RecordLog(string severity)
    {
        counts.AddOrUpdate(severity, 1, (_, old) => old + 1);
    }

    public void PrintStats()
    {
        Console.WriteLine("\nLog counts:");
        foreach (var level in LogLevels.Levels)
        {
            counts.TryGetValue(level, out var count);
            Console.WriteLine($"{level}: {count}");
        }

        Console.WriteLine($"TOTAL: {counts.Values.Sum()}");
    }
}
