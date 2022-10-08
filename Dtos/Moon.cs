using System.Xml.Serialization;

namespace minimalAPI.Dtos;

[XmlRoot("moon")]
public class Moon
{
    public string id { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public string coords { get;set; } = string.Empty;
}

