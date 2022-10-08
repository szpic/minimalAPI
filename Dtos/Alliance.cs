using System.Xml.Serialization;

namespace minimalAPI.Dtos;

[XmlRoot("alliance")]
public class Alliance
{
    public string name { get; set; } = string.Empty;
    public string tag { get; set; } = string.Empty;
}

