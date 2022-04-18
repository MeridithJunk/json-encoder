using System.Reflection;
using System.Text;

namespace Json_Encoder;

public class CustomEncoder
{
    public string Create(object objectToBeEncoded)
    {
        var properties = objectToBeEncoded.GetType().GetProperties();
        var stringBuilder = new StringBuilder();
        foreach (var property in properties)
        {
            var name = property.Name;
            var temp = objectToBeEncoded.GetType().GetProperty(name);
            var value = (string) temp.GetValue(objectToBeEncoded, null);
            stringBuilder.Append($"\"{name}\":\"{value}\"");
        }
        
        return "{" + stringBuilder + "}";
    }
}