namespace ThreadSafeCollections;

public class BrokenLogCounter
{
    public Dictionary<string, int> Counts = [];

    public void RecordLog(string level)
    {
        if (!Counts.TryGetValue(level, out int value))
        {
            value = 0;
            Counts[level] = value;
        }

        Counts[level] = ++value;
    }

    public void PrintStats()
    {
        Console.WriteLine("\nLog counts:");
        foreach (var level in LogLevels.Levels)
        {
            Counts.TryGetValue(level, out var count);
            Console.WriteLine($"{level}: {count}");
        }

        Console.WriteLine($"TOTAL: {Counts.Values.Sum()}");
    }
}
