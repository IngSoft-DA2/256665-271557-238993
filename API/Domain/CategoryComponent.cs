namespace Domain;

public abstract class CategoryComponent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public Guid? CategoryFatherId { get; set; } = null;
    public void CategoryValidator()
    {
        if (string.IsNullOrEmpty(Name))
        {
            throw new InvalidCategoryException("Category name is required");
        }
    }
    
    public abstract void AddChild(CategoryComponent childToAdd);
}