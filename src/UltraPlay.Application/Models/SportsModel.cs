using System.Xml.Serialization;

namespace UltraPlay.Application.Models
{

    [XmlRoot(ElementName = "XmlSports")]
    public class SportsModel
    {
        [XmlElement(ElementName = "Sport")]
        public SportModel Sport { get; set; }

        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string? Xsd { get; set; }

        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string? Xsi { get; set; }

        [XmlAttribute(AttributeName = "CreateDate")]
        public DateTime CreateDate { get; set; }
    }

}