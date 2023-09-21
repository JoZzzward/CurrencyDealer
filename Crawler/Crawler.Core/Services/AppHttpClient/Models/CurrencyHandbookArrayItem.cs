using System.Xml.Serialization;

namespace Crawler.Core.Services.AppHttpClient.Models;

public class CurrencyHandbookArrayItem
{
    [XmlAttribute("ID")]
    public string Id { get; set; }
    public string Name { get; set; }
    public string EngName { get; set; }
    public string Nominal { get; set; }
    public string ParentCode { get; set; }
    public string ISO_Num_Code { get; set; }
    public string ISO_Char_Code { get; set; }
}