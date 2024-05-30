namespace Domain;

public class FlatRequestReport : Report
{
    public string FlatNumber { get; set; }
    public string OwnerName { get; set; }
    public Guid BuildingId { get; set; }
    
    public override bool Equals(object obj)
    {
        FlatRequestReport? objectToCompare = obj as FlatRequestReport;
        
        return FlatNumber == objectToCompare.FlatNumber &&
               OwnerName == objectToCompare.OwnerName && base.Equals(obj);
    }
    
}