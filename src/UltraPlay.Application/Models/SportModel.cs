using System.Xml.Serialization;

namespace UltraPlay.Application.Models
{
    [XmlRoot(ElementName = "Sport")]
    public class SportModel
    {
        [XmlAttribute(AttributeName = "ID")]
        public string Id { get; set; } = null!;

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; } = null!;

        [XmlElement(ElementName = "Event")]
        public List<EventModel> Events { get; set; } = new();
    }
}