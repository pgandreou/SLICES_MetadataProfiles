using Moq;

namespace Slices.V1.StandardConverters.Tests;

public class StandardConverterCollectionTest
{
    [Fact]
    public void Test1()
    {
        var converterMock = new Mock<ISlicesStandardConverter>();
        var standardId = "mock_standard";

        converterMock.Setup(c => c.ExternalStandard).Returns(standardId);

        StandardConverterCollection converterCollection = new(new[] { converterMock.Object });

        Assert.Equal(1, converterCollection.CovertersByStandard.Count);
        Assert.Equal(converterMock.Object, converterCollection.CovertersByStandard[standardId]);
    }
}