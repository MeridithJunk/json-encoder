using Json_Encoder;
using Newtonsoft.Json;
using Xunit;

namespace Json_Encoder_Tests;
public class SerializerTests
{
    private readonly CustomEncoder _sut;
    public SerializerTests()
    {
        _sut = new CustomEncoder();
    }

    [Fact]
    public void GivenAnObjectWithOneStringPropertyReturnEncodedJsonString()
    {
        var objectToBeEncoded = new {something = "something"};
        var actual = _sut.Create(objectToBeEncoded);
        Assert.Equal(JsonConvert.SerializeObject(objectToBeEncoded), actual);
    }
    
    [Fact]
    public void GivenAnObjectWithMultipleStringPropertiesReturnEncodedJsonString()
    {
        var objectWithMultipleStringProps = new {anotherThing = "somethingElse", moo = "cow"};
        var actual = _sut.Create(objectWithMultipleStringProps);
        Assert.Equal(JsonConvert.SerializeObject(objectWithMultipleStringProps), actual);
    }
    
    [Fact]
    public void GivenAnObjectWithAStringAndIntValueReturnEncodedJsonString()
    {
        var objectWithMultipleStringProps = new {anotherThing = "somethingElse", moo = 1};
        var actual = _sut.Create(objectWithMultipleStringProps);
        Assert.Equal(JsonConvert.SerializeObject(objectWithMultipleStringProps), actual);
    }
    
    [Fact]
    public void GivenAnObjectWithByteValueReturnEncodedJsonString()
    {
        byte exampleByte = 64;
        var objectWithMultipleStringProps = new {anotherThing = exampleByte};
        var actual = _sut.Create(objectWithMultipleStringProps);
        Assert.Equal(JsonConvert.SerializeObject(objectWithMultipleStringProps), actual);
    }
    
    [Fact]
    public void GivenAnObjectWithDoubleValueReturnEncodedJsonString()
    {
        var exampleDouble = 64.2;
        var objectWithMultipleStringProps = new {anotherThing = exampleDouble};
        var actual = _sut.Create(objectWithMultipleStringProps);
        Assert.Equal(JsonConvert.SerializeObject(objectWithMultipleStringProps), actual);
    }
}