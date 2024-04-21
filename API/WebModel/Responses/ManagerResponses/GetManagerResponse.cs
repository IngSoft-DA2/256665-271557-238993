namespace WebModel.Responses.ManagerResponses;

public class GetManagerResponse
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public override bool Equals(object? obj)
    {
        GetManagerResponse? objectToCompare = obj as GetManagerResponse;

        if (objectToCompare is null) return false;

        return Name == objectToCompare.Name && Email == objectToCompare.Email && Password == objectToCompare.Password;
    }
}