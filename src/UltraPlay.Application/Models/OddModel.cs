using System.Xml;
using System.Xml.Serialization;

namespace UltraPlay.Application.Models
{
    [XmlRoot(ElementName = "Odd")]
    public class OddModel
    {
        [XmlAttribute(AttributeName = "ID")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; } = null!;

        [XmlAttribute(AttributeName = "Value")]
        public decimal Value { get; set; }

        [XmlAttribute(AttributeName = "SpecialBetValue")]
        public string SpecialBetValue { get; set; }
    }

}