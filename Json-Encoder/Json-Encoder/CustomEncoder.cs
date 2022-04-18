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
            BreakdownJsonObject(jsonBuilder, name, value);
        }
        
        return "{" + jsonBuilder + "}";
    }

    private void BreakdownJsonObject(StringBuilder jsonBuilder, string name, object value)
    {
        var type = value.GetType();
        if (type == typeof(string))
        {
            jsonBuilder.Append(TextToAppend(jsonBuilder, $"\"{name}\":\"{value}\""));
        }
        else if (type.IsArray)
        {
            BuildArrayBlob(value, jsonBuilder, name);
        }
        else if (type == typeof(int) || type == typeof(byte) || type == typeof(double))
        {
            jsonBuilder.Append(TextToAppend(jsonBuilder, $"\"{name}\":{value}"));
        }
        else
        {
            var subProperties = value.GetType().GetProperties();
                var objectBuilder = new StringBuilder();
                jsonBuilder.Append($"\"{name}\":" + "{");
                foreach (var subProperty in subProperties)
                {
                    var subName = subProperty.Name;
                    var subPropertyInfo = type.GetProperty(subName);
                    var subValue = subPropertyInfo.GetValue(value, null);
                    BreakdownJsonObject(objectBuilder, subName, subValue);
                }
                jsonBuilder.Append($"{objectBuilder}" + '}');
        }
    }

    private void BuildArrayBlob(object value, StringBuilder jsonBuilder, string name)
    {
        var arrayBuilder = new StringBuilder();
        foreach (var item in (Array) value)
        {
            arrayBuilder.Append(item is string
                ? TextToAppend(arrayBuilder, $"\"{item}\"")
                : TextToAppend(arrayBuilder, $"{item}"));
        }

        jsonBuilder.Append(TextToAppend(jsonBuilder, $"\"{name}\":[{arrayBuilder}]"));
    }

    private string TextToAppend(StringBuilder jsonBlob, string text)
    {
        return jsonBlob.Length == 0 ? text : $",{text}";
    }
}