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
            if (type == typeof(object))
            {
                var subProperties = value.GetType().GetProperties();
                foreach (var subProperty in subProperties)
                {
                    var subName = subProperty.Name;
                    var subPropertyInfo = value.GetType().GetProperty(subName);
                    var subValue = subPropertyInfo.GetValue(value, null);
                    var subType = subValue.GetType();
                    BreakdownStringArrayOrNumber(subType, jsonBuilder, subName, subValue);
                }

            }
            else
            {
                BreakdownStringArrayOrNumber(type, jsonBuilder, name, value);
            }
        }
        
        return "{" + jsonBuilder + "}";
    }

    private void BreakdownStringArrayOrNumber(Type type, StringBuilder jsonBuilder, string name, object value)
    {
        if (type == typeof(string))
        {
            jsonBuilder.Append(TextToAppend(jsonBuilder, $"\"{name}\":\"{value}\""));
        }
        else if (type.IsArray)
        {
            BuildArrayBlob(value, jsonBuilder, name);
        }
        else
        {
            jsonBuilder.Append(TextToAppend(jsonBuilder, $"\"{name}\":{value}"));
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