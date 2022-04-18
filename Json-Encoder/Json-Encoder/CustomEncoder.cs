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
            var type = value.GetType();
            if (_numericTypes.Contains(type))
            {
                stringBuilder.Append(stringBuilder.Length == 0
                    ?  $"\"{name}\":{value}"
                    : $",\"{name}\":{value}");  
            }
            else if (type == typeof(string[]))
            {
                foreach (var item in (string[]) value)
                {
                    stringBuilder.Append(stringBuilder.Length == 0
                        ? $"\"{name}\":[\"{item}\"]"
                        : $",\"{name}\":[\"{item}\"]");
                }
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

    readonly HashSet<Type> _numericTypes = new()
    {
        typeof(int), typeof(byte), typeof(double)
    };
}