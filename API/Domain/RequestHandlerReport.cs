namespace Domain;

public class RequestHandlerReport : Report
{
    public int AvgTimeToCloseRequest { get; set; }
    public Guid RequestHandlerId { get; set; }
}