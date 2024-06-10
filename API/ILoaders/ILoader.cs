using Domain;
using WebModel.Requests.BuildingRequests;
using WebModel.Responses.LoaderReponses;

namespace ILoaders;

public interface ILoader
{
    public string LoaderName();
    public  List<Building> LoadAllBuildings(string filePath);
}