namespace ThreadSafeCollections;

public class BrokenLogCollector
{
    private readonly List<LogEntry> logs = [];

    public void Add(LogEntry log) => logs.Add(log);

    public int Count() => logs.Count;

    public LogEntry[] Last(int n) => logs.OrderByDescending(l => l.Time).Take(n).ToArray();
}
