namespace ThreadSafeCollections;

public class BrokenLogProcessor
{
    private readonly Queue<LogEntry> queue = [];

    public void Enqueue(LogEntry log) => queue.Enqueue(log);

    public void ProcessAll()
    {
        while (queue.TryDequeue(out var log))
        {
            Console.ForegroundColor = log.Level switch
            {
                "ERROR" => ConsoleColor.Red,
                "WARNING" => ConsoleColor.Yellow,
                _ => ConsoleColor.Gray
            };

            Console.WriteLine($"[{log.Time:HH:mm:ss.fff}] {log.Level} - {log.Message}");
            Console.ResetColor();
        }
    }
}
