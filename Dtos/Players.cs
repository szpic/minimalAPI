using System.Xml.Serialization;

namespace minimalAPI.Dtos;

[XmlRoot(ElementName = "players")]
public class Players
{

	[XmlElement(ElementName = "player")]
	public List<Player> Player { get; set; }

	[XmlAttribute(AttributeName = "xsi")]
	public string Xsi { get; set; }

	[XmlAttribute(AttributeName = "noNamespaceSchemaLocation")]
	public string NoNamespaceSchemaLocation { get; set; }

	[XmlAttribute(AttributeName = "timestamp")]
	public int Timestamp { get; set; }

	[XmlAttribute(AttributeName = "serverId")]
	public string ServerId { get; set; }
}

