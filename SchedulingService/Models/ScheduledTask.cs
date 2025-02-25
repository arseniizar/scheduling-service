namespace SchedulingService.Models;

public class ScheduledTask
{
    public int Id { get; set; }
    public required string TaskName { get; set; }
    public DateTime ScheduledTime { get; set; }
    public bool IsExecuted { get; set; }
}