namespace Domain;

public class CategoryComposite : CategoryComponent
{
    public List<CategoryComponent> SubCategories { get; set; }

    public override void AddChild(CategoryComponent childToAdd)
    {
        SubCategories.Add(childToAdd);
    }
}
