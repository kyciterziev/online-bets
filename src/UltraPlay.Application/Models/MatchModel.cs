using System.Xml.Serialization;

namespace UltraPlay.Application.Models
{
    [XmlRoot(ElementName = "Match")]
    public class MatchModel
    {
        [XmlAttribute(AttributeName = "ID")]
        public string Id { get; set; } = null!;

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; } = null!;

        [XmlElement(ElementName = "Bet")]
        public List<BetModel> Bets { get; set; } = new List<BetModel>();

        [XmlAttribute(AttributeName = "StartDate")]
        public DateTime StartDate { get; set; }

        [XmlAttribute(AttributeName = "MatchType")]
        public string MatchType { get; set; } = null!;
    }

}