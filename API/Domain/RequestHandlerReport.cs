namespace Domain;

public class RequestHandlerReport : Report
{
    public TimeSpan AvgTimeToCloseRequest { get; set; }
    public TimeSpan TotalTime { get; set; }
    
    public override bool Equals(object obj)
    {
        RequestHandlerReport? objectToCompare = obj as RequestHandlerReport;
        
        return AvgTimeToCloseRequest == objectToCompare.AvgTimeToCloseRequest &&
               TotalTime == objectToCompare.TotalTime && base.Equals(obj);
    }
}