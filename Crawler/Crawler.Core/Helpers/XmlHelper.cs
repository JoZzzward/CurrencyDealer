using System.Text;
using System.Xml.Serialization;

namespace Crawler.Core.Helpers;

public static class XmlHelper
{
    public static T DeserializeFromString<T>(string content)
    {
        var serializer = new XmlSerializer(typeof(T));

        using var reader = new StreamReader(
            new MemoryStream(
                Encoding.UTF8.GetBytes(content)
                ));
        
        var result = (T)Convert.ChangeType(serializer.Deserialize(reader)!, typeof(T));

        return result;
    }
}