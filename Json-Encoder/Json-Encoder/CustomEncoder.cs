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
                stringBuilder.Append(TextToAppend(stringBuilder, $"\"{name}\":{value}"));
            }
            else if (type == typeof(string[]))
            {
                foreach (var item in (string[]) value)
                {
                    stringBuilder.Append(TextToAppend(stringBuilder, $"\"{name}\":[\"{item}\"]"));
                }
            }
            else
            {
                stringBuilder.Append(TextToAppend(stringBuilder, $"\"{name}\":\"{value}\""));
            }
        }
        
        return "{" + stringBuilder + "}";
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