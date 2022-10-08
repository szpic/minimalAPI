using System.Xml.Serialization;

namespace minimalAPI.Dtos;

[XmlRoot(ElementName = "highscore")]
public class HighScore
{
    [XmlElement(ElementName = "player")]
    public List<PlayerHighscore> Player { get; set; }
	[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
	public string Xsi { get; set; }
	[XmlAttribute(AttributeName = "category")]
	public string Category { get; set; }
	[XmlAttribute(AttributeName = "type")]
	public string Type { get; set; }
	[XmlAttribute(AttributeName = "noNamespaceSchemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
	public string NoNamespaceSchemaLocation { get; set; }
	[XmlAttribute(AttributeName = "timestamp")]
	public string Timestamp { get; set; }
	[XmlAttribute(AttributeName = "serverId")]
	public string ServerId { get; set; }
}

