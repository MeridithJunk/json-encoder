using System.Text;

namespace Json_Encoder;

public class CustomEncoder
{
    public string Create(object objectToBeEncoded)
    {
        var properties = objectToBeEncoded.GetType().GetProperties();
        var jsonBuilder = new StringBuilder();
        foreach (var property in properties)
        {
            var name = property.Name;
            var propertyInfo = objectToBeEncoded.GetType().GetProperty(name);
            var value = propertyInfo.GetValue(objectToBeEncoded, null);
            var type = value.GetType();
            if (_numericTypes.Contains(type))
            {
                jsonBuilder.Append(TextToAppend(jsonBuilder, $"\"{name}\":{value}"));
            }
            else if (type.IsArray)
            {
                var arrayBuilder = new StringBuilder();
                foreach (var item in (Array) value)
                {
                    arrayBuilder.Append(item is string
                        ? TextToAppend(arrayBuilder, $"\"{item}\"")
                        : TextToAppend(arrayBuilder, $"{item}"));
                }
                jsonBuilder.Append($"\"{name}\":[{arrayBuilder}]");
            }
            else
            {
                jsonBuilder.Append(TextToAppend(jsonBuilder, $"\"{name}\":\"{value}\""));
            }
        }
        
        return "{" + jsonBuilder + "}";
    }

    private string TextToAppend(StringBuilder jsonBlob, string text)
    {
        return jsonBlob.Length == 0 ? text : $",{text}";
    }

    readonly HashSet<Type> _numericTypes = new()
    {
        typeof(int), typeof(byte), typeof(double)
    };
}