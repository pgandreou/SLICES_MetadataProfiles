namespace Slices.V1.Model.Tests;

public class SfdoOptionalTest
{
    [Fact]
    public void AbsentByDefault()
    {
        SfdoOptional<int> optional = new();
        
        Assert.False(optional.IsSet);
        Assert.Null(optional.AbsenceReason);

        Assert.Throws<InvalidOperationException>(() => optional.Value);
    }
    
    [Fact]
    public void SetAbsentLocal()
    {
        SfdoOptional<int> optional = new();
        optional.SetAbsent("test");
        
        Assert.False(optional.IsSet);
        Assert.Equal("test", optional.AbsenceReason);

        Assert.Throws<InvalidOperationException>(() => optional.Value);
    }
}