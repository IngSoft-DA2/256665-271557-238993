using System.Text.Json;
using Domain;
using ILoaders;

namespace LoaderJSON;

public class LoaderJson : ILoader
{
    public string LoaderName()
    {
        return "BuildingLoaderJSON";
    }

    public List<Building> LoadAllBuildings(string filePath)
    {
        List<Building> buildings = new List<Building>();

        try
        {
            string json = File.ReadAllText(filePath);
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true
            };

            BuildingWrapperJson buildingWrapper =
                JsonSerializer.Deserialize<BuildingWrapperJson>(json, serializerOptions);

            foreach (BuildingJSON building in buildingWrapper.Edificios)
            {
                Building newBuilding = new Building()
                {
                    Name = building.Nombre,
                    Address =
                        $"{building.Direccion.CallePrincipal}, {building.Direccion.NumeroPuerta}, {building.Direccion.CalleSecundaria}",
                    Manager = new Manager()
                    {
                        Email = building.Encargado
                    },
                    Location = new Location()
                    {
                        Latitude = building.Gps.Latitud,
                        Longitude = building.Gps.Longitud
                    },
                    CommonExpenses = building.GastosComunes
                };

                foreach (FlatJson flat in building.Departamentos)
                {
                    newBuilding.Flats.Append(new Flat()
                    {
                        Floor = flat.Piso,
                        RoomNumber = flat.NumeroPuerta.ToString(),
                        TotalRooms = flat.Habitaciones,
                        TotalBaths = flat.Banos,
                        HasTerrace = flat.ConTerraza,
                        OwnerAssigned = new Owner()
                        {
                            Email = flat.PropietarioEmail,
                        }
                    });

                }

                buildings.Add(newBuilding);
            }
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception("Error loading buildings from JSON");
        }

        return buildings;
    }
}