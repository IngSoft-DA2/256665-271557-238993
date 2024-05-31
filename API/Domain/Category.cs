namespace Domain;

public class Category : CategoryComponent
{
    public override void AddChild(CategoryComponent childToAdd)
    {
        throw new NotImplementedException();
    }
    
    public override bool Equals(object? obj)
    {
        CategoryComponent? objectToCompareWith = obj as Category;
        
        return Id == objectToCompareWith.Id && Name == objectToCompareWith.Name;
    }

}