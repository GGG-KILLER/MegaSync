namespace MegaSync.Model;

public class LogMessage
{
    public int Id { get; set; }
    public required DateTime Timestamp { get; set; }
    public required string Message { get; set; }
}
