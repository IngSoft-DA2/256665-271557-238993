namespace Domain;

public class Category : CategoryComponent
{
    public override void AddChild(CategoryComponent childToAdd)
    {
    }

    public override List<CategoryComponent>? GetChilds()
    {
        return null;
    }

    public override bool Equals(object? obj)
    {
        CategoryComponent? objectToCompareWith = obj as Category;
        
        return Id == objectToCompareWith.Id && Name == objectToCompareWith.Name;
    }

}