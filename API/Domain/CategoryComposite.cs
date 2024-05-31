namespace Domain;

public class CategoryComposite : CategoryComponent
{
    public List<CategoryComponent> SubCategories { get; set; } = new List<CategoryComponent>();

    public override void AddChild(CategoryComponent childToAdd)
    {
        SubCategories.Add(childToAdd);
    }
}
