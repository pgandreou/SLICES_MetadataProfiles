using Slices.TestsSupport;
using Slices.V1.StandardConverters.DataCite.Model;

namespace Slices.V1.StandardConverters.DataCite.Tests;

public class DataCiteSerializerTest
{
    [Fact]
    public void Test1()
    {
        using StreamReader reader = SlicesTestHelpers.GetCopiedFileReader(GetType(), "ReferenceFiles\\datacite-example-full-v4.xml");
        DataCiteSerializer serializer = new();

        DataCiteResource resource = serializer.FromXml(reader);

        Assert.NotNull(resource);
    }

    [Fact]
    public void Experiment1()
    {
        DataCiteResource resource = new();

        resource.contributors = new[]
        {
            new DataCiteResourceContributor
            {
                contributorType = DataCiteContributorType.ProjectLeader,
                contributorName = new DataCiteResourceContributorName { Value = "Starr, Joan" },
                givenName = "Joan",
                familyName = "Starr",
                nameIdentifier = new [] { new DataCiteNameIdentifier
                {
                    schemeURI = "http://orcid.org/",
                    nameIdentifierScheme = "ORCID",
                    Value = "0000-0002-7285-027X",
                } },
            }
        };

        DataCiteSerializer serializer = new();
        StringWriter writer = new();

        serializer.ToXml(resource, writer);
        string result = writer.ToString();

        Assert.NotNull(result);
    }
}