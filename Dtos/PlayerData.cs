using System.Xml.Serialization;

namespace minimalAPI.Dtos;

[XmlRoot("playerData")]
public class PlayerData
{
    [XmlElement("positions")]
    public Positions Positions { get; set; }
    [XmlElement("planets")]
    public Planets planets{get;set; }
    [XmlElement("alliance")]
    public Alliance Alliance { get; set; }
}
