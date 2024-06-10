using System.Text.Json.Serialization;

namespace LoaderJSON;

public class BuildingWrapperJson
{
    [JsonPropertyName("edificios")]
    public List<BuildingJSON> Edificios { get; set; }
    
}

public class BuildingJSON
{
    [JsonPropertyName("nombre")]
    public string Nombre { get; set; }

    [JsonPropertyName("direccion")]
    public AddressJson Direccion { get; set; }

    [JsonPropertyName("encargado")]
    public string Encargado { get; set; }

    [JsonPropertyName("gps")]
    public GpsJson Gps { get; set; }

    [JsonPropertyName("gastos_comunes")]
    public double GastosComunes { get; set; }

    [JsonPropertyName("departamentos")]
    public List<FlatJson> Departamentos { get; set; }
}

public class AddressJson
{
    [JsonPropertyName("calle_principal")]
    public string CallePrincipal { get; set; }

    [JsonPropertyName("numero_puerta")]
    public int NumeroPuerta { get; set; }

    [JsonPropertyName("calle_secundaria")]
    public string CalleSecundaria { get; set; }
}

public class GpsJson
{
    [JsonPropertyName("latitud")]
    public double Latitud { get; set; }

    [JsonPropertyName("longitud")]
    public double Longitud { get; set; }
}

public class FlatJson
{
    [JsonPropertyName("piso")]
    public int Piso { get; set; }

    [JsonPropertyName("numero_puerta")]
    public int NumeroPuerta { get; set; }

    [JsonPropertyName("habitaciones")]
    public int Habitaciones { get; set; }

    [JsonPropertyName("conTerraza")]
    public bool ConTerraza { get; set; }

    [JsonPropertyName("baños")]
    public int Banos { get; set; }

    [JsonPropertyName("propietarioEmail")]
    public string PropietarioEmail { get; set; }
}