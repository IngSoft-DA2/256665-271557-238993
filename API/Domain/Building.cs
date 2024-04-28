namespace Domain;

public class Building
{
    public Guid Id { get; set; }
    public Guid ManagerId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public Location Location { get; set; }
    public ConstructionCompany ConstructionCompany { get; set; }
    public double CommonExpenses { get; set; }
    public IEnumerable<Flat> Flats { get; set; } = new List<Flat>();

    public void BuildingValidator()
    {
        NameValidation();
        AddressValidation();
        LocationValidation();
        ConstructionCompanyValidation();
    }
    private void NameValidation()
    {
        if (string.IsNullOrEmpty(Name))
        {
            throw new InvalidBuildingException("Building name cannot be empty");
        }
    }
    
    private void AddressValidation()
    {
        if (string.IsNullOrEmpty(Address))
        {
            throw new InvalidBuildingException("Building address cannot be empty");
        }
    }
    
    private void LocationValidation()
    {
        if (Location == null)
        {
            throw new InvalidBuildingException("Building location cannot be empty");
        }
    }
    
    private void ConstructionCompanyValidation()
    {
        if (ConstructionCompany == null)
        {
            throw new InvalidBuildingException("Construction company cannot be empty");
        }
    }
    
    public void CommonExpensesValidation()
    {
        if (CommonExpenses < 0)
        {
            throw new InvalidBuildingException("Common expenses cannot be negative");
        }
    }
}