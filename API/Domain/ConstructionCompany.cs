namespace Domain;

public class ConstructionCompany
{
    public Guid Id { get; set; }
    public string Name { get; set; }




    public void ConstructionCompanyValidator()
    {
        if (string.IsNullOrEmpty(Name))
        {
            throw new InvalidConstructionCompanyException("Name cannot be empty.");
        }
    }
    public override bool Equals(object? objectToCompareWith)
    {
        ConstructionCompany? categoryToCompare = objectToCompareWith as ConstructionCompany;
        if (categoryToCompare is null) return false;

        return Id == categoryToCompare.Id && Name == categoryToCompare.Name;
    }
}