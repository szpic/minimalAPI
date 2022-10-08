using System.Xml.Serialization;

namespace minimalAPI.Dtos;

[XmlRoot("planet")]
public class Planet
{
    public string id { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public string coords { get;set; } = string.Empty;
    [XmlElement("moon")]
    public Moon moon { get; set; }
}

