using System.Xml.Serialization;

namespace minimalAPI.Dtos;

public class Positions
{
    [XmlElement("positions")]
    List<Positions> positions { get; set; }
}

