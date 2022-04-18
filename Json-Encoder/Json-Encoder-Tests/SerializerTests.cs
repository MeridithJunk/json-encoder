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
    public void GivenAnObjectWithOneStringPropertyReturnEncodedJsonString()
    {
        var sut = new CustomEncoder();
        var actual = sut.Create(example);
        Assert.Equal(JsonConvert.SerializeObject(example), actual);
    }
    
    [Fact]
    public void GivenAnObjectWithMultipleStringPropertiesReturnEncodedJsonString()
    {
        var sut = new CustomEncoder();
        var objectWithMultipleStringProps = new {anotherThing = "somethingElse", moo = "cow"};
        var actual = sut.Create(objectWithMultipleStringProps);
        Assert.Equal(JsonConvert.SerializeObject(objectWithMultipleStringProps), actual);
    }
}