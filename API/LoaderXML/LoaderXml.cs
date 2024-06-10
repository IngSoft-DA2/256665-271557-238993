using System.Xml.Serialization;
using Domain;
using ILoaders;

namespace LoaderXML;

public class LoaderXML : ILoader
{
    public string LoaderName()
    {
        return "BuildingLoaderXML";
    }

    public List<Building> LoadAllBuildings(string filePath)
    {
        List<Building> buildings = new List<Building>();

        try
        {

            XmlSerializer serializer = new XmlSerializer(typeof(BuildingWrapperXML));

            BuildingWrapperXML buildingWrapper =
                serializer.Deserialize(new FileStream(filePath, FileMode.Open)) as BuildingWrapperXML;

            foreach (BuildingXML building in buildingWrapper.Edificios)
            {
                Building newBuilding = new Building()
                {
                    Id = Guid.NewGuid(),
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
                    CommonExpenses = building.GastosComunes,
                    Flats = new List<Flat>()
                };

                foreach (FlatXML flat in building.Departamentos)
                {
                    newBuilding.Flats.Append(new Flat()
                    {
                        Id = Guid.NewGuid(),
                        Floor = flat.Piso,
                        RoomNumber = flat.NumeroPuerta.ToString(),
                        TotalRooms = flat.Habitaciones,
                        TotalBaths = flat.Banos,
                        HasTerrace = flat.ConTerraza,
                        OwnerAssigned = new Owner()
                        {
                            Id = Guid.NewGuid(),
                            Email = flat.PropietarioEmail
                        }
                    });
                }

                buildings.Append(newBuilding);

            }
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }

        return buildings;
        }
}