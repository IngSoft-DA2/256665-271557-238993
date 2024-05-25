namespace Domain;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }


    public void CategoryValidator()
    {
        if (string.IsNullOrEmpty(Name))
        {
            throw new InvalidCategoryException("Category name is required");
        }
    }


    public override bool Equals(object? obj)
    {
        Category? objectToCompareWith = obj as Category;
        
        return Id == objectToCompareWith.Id && Name == objectToCompareWith.Name;
    }
}