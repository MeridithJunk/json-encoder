using Json_Encoder;
using Newtonsoft.Json;

namespace Json_Encoder_Tests;
using Xunit;


public class UnitTest1
{
    TestItem example = new()
    {
        testString = "something"
    };
    
    [Fact]
    public void GivenAnObjectReturnEncodedJsonString()
    {
        var sut = new CustomEncoder();
        var actual = sut.Create(example);
        Assert.Equal(JsonConvert.SerializeObject(example), actual);
    }
}