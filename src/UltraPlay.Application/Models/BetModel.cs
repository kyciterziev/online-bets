using System.Xml.Serialization;

namespace UltraPlay.Application.Models
{
    [XmlRoot(ElementName = "Bet")]
    public class BetModel
    {
        [XmlAttribute(AttributeName = "ID")]
        public string Id { get; set; } = null!;

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; } = null!;

        [XmlElement(ElementName = "Odd")]
        public List<OddModel> Odds { get; set; } = new List<OddModel>();

        [XmlAttribute(AttributeName = "IsLive")]
        public bool IsLive { get; set; }
    }

}