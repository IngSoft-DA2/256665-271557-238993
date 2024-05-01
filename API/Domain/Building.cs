namespace Domain;

public class Building
{
    #region Properties

    public Guid Id { get; set; }
    public Guid ManagerId { get; set; }
    public Manager Manager { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public Location Location { get; set; }
    public Guid ConstructionCompanyId { get; set; }
    public ConstructionCompany ConstructionCompany { get; set; }
    public double CommonExpenses { get; set; }
    public IEnumerable<Flat> Flats { get; set; } = new List<Flat>();

    #endregion

    #region Validations

    public void BuildingValidator()
    {
        NameValidation();
        AddressValidation();
        LocationValidation();
        ConstructionCompanyValidation();
        CommonExpensesValidation();
        ManagerIdValidation();
        IdValidation();
        FlatValidation();
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

    private void CommonExpensesValidation()
    {
        if (CommonExpenses < 0)
        {
            throw new InvalidBuildingException("Common expenses cannot be negative");
        }
    }

    private void ManagerIdValidation()
    {
        if (ManagerId == Guid.Empty)
        {
            throw new InvalidBuildingException("Manager id cannot be empty");
        }
    }

    private void IdValidation()
    {
        if (Id == Guid.Empty)
        {
            throw new InvalidBuildingException("Id cannot be empty");
        }
    }

    private void FlatValidation()
    {
        if (Flats == null)
        {
            throw new InvalidBuildingException("Flats list can't be null, you must provide a list of flats");
        }
    }

    #endregion

    public override bool Equals(object? obj)
    {
        Building objectToCompare = obj as Building;
        return objectToCompare.Id == Id && 
               objectToCompare.ManagerId == ManagerId && 
               objectToCompare.Name == Name && 
               objectToCompare.Address == Address &&
               objectToCompare.Location.Equals(Location) && 
               objectToCompare.ConstructionCompany.Equals(ConstructionCompany) && objectToCompare.ConstructionCompanyId == ConstructionCompanyId &&
               objectToCompare.CommonExpenses.Equals(CommonExpenses) &&
               objectToCompare.Flats.SequenceEqual(Flats);
    }
}