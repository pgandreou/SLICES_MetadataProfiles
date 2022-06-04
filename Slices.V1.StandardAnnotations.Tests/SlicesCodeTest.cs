using Slices.V1.StandardAnnotations;

namespace Slices.V1.StandardAnnotations.Tests;

public class SlicesCodeTest
{
    [Theory]
    [InlineData(SlicesFieldCategory.PrimaryInformation, 1, "SLICES.PI.01")]
    [InlineData(SlicesFieldCategory.ManagementInformation, 12, "SLICES.MA.12")]
    public void ToCodeStringDefaultDomain(SlicesFieldCategory category, int order, string expected)
    {
        SlicesCodeAttribute slicesCode = new(category, order);

        Assert.Equal(expected, slicesCode.ToCodeString());
    }
}