using Slices.Common;
using Slices.TestsSupport;
using Slices.V1.Converters.DataCite.Model;

namespace Slices.V1.Converters.DataCite.Tests;

public class DataCiteSerializerTest
{
    [Fact]
    public async Task Test1()
    {
        await using FileStream stream = SlicesTestHelpers.GetCopiedFileReadStream(
            GetType(),
            "ReferenceFiles\\datacite-example-full-v4.xml"
        );
        
        DataCiteSerializer serializer = new();
        DataCiteResource resource = await serializer.FromXmlAsync(stream);

        Assert.NotNull(resource);
    }

    [Fact]
    public async Task Experiment1()
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
        MemoryStream stream = new();

        await serializer.ToXmlAsync(resource, stream);
        string result = stream.ReadAsStringFromStart();

        Assert.NotNull(result);
    }
}