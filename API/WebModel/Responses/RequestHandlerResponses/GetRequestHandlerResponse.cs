namespace WebModel.Responses.RequestHandlerResponses;

public class GetRequestHandlerResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public override bool Equals(object objectToCompare)
    {
        GetRequestHandlerResponse? toCompare = objectToCompare as GetRequestHandlerResponse;

        bool areEqual =  Id == toCompare.Id &&
               Name == toCompare.Name &&
               LastName == toCompare.LastName &&
               Email == toCompare.Email;

        return areEqual;
    }
}