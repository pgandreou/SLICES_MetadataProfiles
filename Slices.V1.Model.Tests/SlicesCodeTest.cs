using Slices.V1.Model.Annotations;

namespace Slices.V1.Model.Tests;

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

    [Theory]
    [MemberData(nameof(CategoryToCodeString_AllHandled_Values))]
    public void CategoryToCodeString_AllHandled(SlicesFieldCategory category)
    {
        Assert.False(string.IsNullOrEmpty(category.ToCodeString()));
    }

    public static IEnumerable<object[]> CategoryToCodeString_AllHandled_Values()
    {
        foreach (SlicesFieldCategory category in Enum.GetValues<SlicesFieldCategory>())
        {
            yield return new object[] { category };
        }
    }
}