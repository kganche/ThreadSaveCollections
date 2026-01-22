using System.Collections.Concurrent;

namespace ThreadSafeCollections;

public class LogCollector
{
    private readonly ConcurrentBag<LogEntry> logs = [];

    public void Add(LogEntry log) => logs.Add(log);

    public int Count() => logs.Count;

    public LogEntry[] Last(int n) => logs.OrderByDescending(l => l.Time).Take(n).ToArray();
}
