using System.Xml.Serialization;

namespace UltraPlay.Application.Models
{
    [XmlRoot(ElementName = "Event")]
    public class EventModel
    {
        [XmlAttribute(AttributeName = "ID")]
        public string Id { get; set; } = null!;

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; } = null!;

        [XmlElement(ElementName = "Match")]
        public List<MatchModel> Matches { get; set; } = new List<MatchModel>();

        [XmlAttribute(AttributeName = "IsLive")]
        public bool IsLive { get; set; }

        [XmlAttribute(AttributeName = "CategoryID")]
        public string CategoryId { get; set; }
    }

}