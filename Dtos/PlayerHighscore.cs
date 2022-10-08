using System.Xml.Serialization;

namespace minimalAPI.Dtos;

[XmlRoot("player")]
public class PlayerHighscore
{
	[XmlAttribute(AttributeName = "position")]
	public string Position { get; set; }
	[XmlAttribute(AttributeName = "id")]
	public string Id { get; set; }
	[XmlAttribute(AttributeName = "score")]
	public string Score { get; set; }
}

