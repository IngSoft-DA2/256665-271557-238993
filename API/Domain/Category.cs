namespace Domain;

public class Category
{
    public Guid Id = Guid.NewGuid();
    public string Name { get; set; }

    public override bool Equals(object? obj)
    {
        Category? objectToCompareWith = obj as Category;
        if (objectToCompareWith is null) return false;

        return Id == objectToCompareWith.Id && Name == objectToCompareWith.Name;
    }
}