namespace LoaderXML;

using System.Xml.Serialization;

public class BuildingWrapperXML
{
    [XmlElement("edificios")]
    public List<BuildingXML> Edificios { get; set; }
    
}

public class BuildingXML
{
    [XmlElement("nombre")]
    public string Nombre { get; set; }

    [XmlElement("direccion")]
    public AddressXML Direccion { get; set; }

    [XmlElement("encargado")]
    public string Encargado { get; set; }

    [XmlElement("gps")]
    public GpsXML Gps { get; set; }

    [XmlElement("gastos_comunes")]
    public double GastosComunes { get; set; }

    [XmlElement("departamentos")]
    public List<FlatXML> Departamentos { get; set; }
}

public class AddressXML
{
    [XmlElement("calle_principal")]
    public string CallePrincipal { get; set; }

    [XmlElement("numero_puerta")]
    public int NumeroPuerta { get; set; }

    [XmlElement("calle_secundaria")]
    public string CalleSecundaria { get; set; }
}

public class GpsXML
{
    [XmlElement("latitud")]
    public double Latitud { get; set; }

    [XmlElement("longitud")]
    public double Longitud { get; set; }
}

public class FlatXML
{
    [XmlElement("piso")]
    public int Piso { get; set; }

    [XmlElement("numero_puerta")]
    public int NumeroPuerta { get; set; }
    
    [XmlElement("habitaciones")]
    public int Habitaciones { get; set; }
    
    
    [XmlElement("con_terraza")]
    public bool ConTerraza { get; set; }
    
    [XmlElement("banos")]
    public int Banos { get; set; }
    
    [XmlElement("propietario_email")]
    public string PropietarioEmail { get; set; }
    
}