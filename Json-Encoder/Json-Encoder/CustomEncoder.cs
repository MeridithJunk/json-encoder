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
            var propertyInfo = objectToBeEncoded.GetType().GetProperty(name);
            var value = propertyInfo.GetValue(objectToBeEncoded, null);
            if (value.GetType() == typeof(int))
            {
                stringBuilder.Append(stringBuilder.Length == 0
                    ?  $"\"{name}\":{value}"
                    : $",\"{name}\":{value}");  
            }
            else
            {
                stringBuilder.Append(stringBuilder.Length == 0
                    ? $"\"{name}\":\"{value}\""
                    : $",\"{name}\":\"{value}\"");
            }
        }
        
        return "{" + stringBuilder + "}";
    }
}