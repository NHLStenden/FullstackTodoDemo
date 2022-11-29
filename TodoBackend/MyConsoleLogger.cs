namespace TodoBackend;

public interface IMyLogger
{
    void Log(string message);
}

public class MyConsoleLogger : IMyLogger
{
    public void Log(string message)
    {
        Console.WriteLine("MyConsoleLogger - " + message);
    }
}

public class MyDbLogger : IMyLogger
{
    public void Log(string message)
    {
        Console.WriteLine("DbLogger - " + message);
    }
}

public class CombineLogger : IMyLogger
{
    private readonly IMyLogger _logger1;
    private readonly IMyLogger _logger2;

    public CombineLogger(IMyLogger logger1, IMyLogger logger2)
    {
        _logger1 = logger1;
        _logger2 = logger2;
    }
    
    public void Log(string message)
    {
        _logger1.Log(message);
        _logger2.Log(message);
    }
} 


