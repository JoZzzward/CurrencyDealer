using System.Xml.Serialization;

namespace Crawler.Core.Services.AppHttpClient.Models;

[XmlRoot("ValCurs")]
public class ExchangeRates
{
    [XmlAttribute("Date")]
    public string Date { get; set; }
    [XmlAttribute("name")]
    public string Name { get; set; }
    [XmlElement("Valute")]
    public List<ExchangeValue> Values { get; set; }
}
