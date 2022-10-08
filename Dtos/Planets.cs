using System.Xml.Serialization;

namespace minimalAPI.Dtos;

[XmlRoot("planets")]
public class Planets
{

    List<Planet> planets { get; set; }
}

