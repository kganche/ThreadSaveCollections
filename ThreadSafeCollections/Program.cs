using ThreadSafeCollections;

var counter = new LogCounter();
var collector = new LogCollector();
var processor = new LogProcessor();

var threads = new Thread[5];

for (int i = 0; i < 5; i++)
{
    int threadId = i;

    threads[i] = new Thread(() =>
    {
        var random = new Random(threadId * Environment.TickCount);

        for (int j = 0; j < 200; j++)
        {
            var level = LogLevels.Levels[random.Next(3)];
            var log = new LogEntry($"Log from T{threadId}", level, DateTime.Now);

            counter.RecordLog(level);
            collector.Add(log);
            processor.Enqueue(log);

            Thread.Sleep(random.Next(1, 10));
        }
    });

    threads[i].Start();
}

var consumer = new Thread(() =>
{
    Thread.Sleep(200);
    processor.ProcessAll();
});

consumer.Start();

foreach (var thread in threads)
{
    thread.Join();
}

consumer.Join();

counter.PrintStats();

Console.WriteLine($"\nTotal stored logs: {collector.Count()}");

Console.WriteLine("\nLast 10 logs:");
foreach (var log in collector.Last(10))
{
    Console.WriteLine($"{log.Time:HH:mm:ss.fff} {log.Level} {log.Message}");
}