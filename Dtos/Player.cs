using System.Xml.Serialization;

namespace minimalAPI.Dtos;

[XmlRoot(ElementName = "player")]
public class Player
{

	[XmlAttribute(AttributeName = "id")]
	public string Id { get; set; }

	[XmlAttribute(AttributeName = "name")]
	public string Name { get; set; }

	[XmlAttribute(AttributeName = "status")]
	public string Status { get; set; }

	[XmlAttribute(AttributeName = "alliance")]
	public int Alliance { get; set; }
}

