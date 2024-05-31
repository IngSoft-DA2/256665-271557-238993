namespace Domain;

public class CategoryComposite : CategoryComponent
{

    public List<CategoryComponent> SubCategories { get; set; } = new List<CategoryComponent>();
    
    public override void AddChild(CategoryComponent childToAdd)
    {
        SubCategories.Add(childToAdd);
    }

    public override List<CategoryComponent> GetChilds()
    {
        return SubCategories;
    }

    public override bool Equals(object? obj)
    {
        CategoryComponent objectToCompareWith = obj as CategoryComposite;
        
        return base.Equals(obj) && SubCategories.SequenceEqual(objectToCompareWith.GetChilds());
    }
}
