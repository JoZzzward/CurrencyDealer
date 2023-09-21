using System.Xml.Serialization;

namespace Crawler.Core.Services.AppHttpClient.Models;

[XmlRoot("Valuta")]
public class CurrencyHandbookArray
{
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlElement("Item")]
    public List<CurrencyHandbookArrayItem> Items { get; set; }
}
