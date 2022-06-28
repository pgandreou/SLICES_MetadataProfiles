using Slices.V1.Model;

namespace Slices.TestsSupport;

public static class SfdoTestSamples
{
    public static SfdoResource DataCiteSample()
    {
        SfdoResource sfdo = new();

        sfdo.Identifier = new() { Type = SfdoIdentifierTypes.Doi, Value = "10.5072/example-full" };
        sfdo.AlternateIdentifiers.Add(new() { Type = SfdoIdentifierTypes.Url, Value = "https://schema.datacite.org/meta/kernel-4.4/example/datacite-example-full-v4.4.xml" });

        sfdo.Creators.Add(new()
        {
            Identifier = new() { Type = SfdoIdentifierTypes.Orcid, Value = "0000-0001-5000-0007" },
            Name = "Miller, Elizabeth",
        });

        sfdo.Name = "Full DataCite XML Example";
        sfdo.Description = "XML example of all DataCite Metadata Schema v4.4 properties.";

        sfdo.Subjects = new List<string>
        {
            "computer science"
        };

        sfdo.Version = "4.4";

        sfdo.Contributors = new List<SfdoContributor>
        {
            new()
            {
                Identifier = new() { Type = SfdoIdentifierTypes.Orcid, Value = "0000-0002-7285-027X" },
                Name = "Starr, Joan",
            }
        };

        sfdo.RelatedObjects = new List<SfdoRelationLink>
        {
            new()
            {
                Identifier = new() { Type = SfdoIdentifierTypes.Url, Value = "https://data.datacite.org/application/citeproc+json/10.5072/example-full" },
                RelationshipType = "HasMetadata",
            },
            
            new()
            {
                Identifier = new() { Type = SfdoIdentifierTypes.Arxiv, Value = "0706.0001" },
                RelationshipType = "IsReviewedBy",
                ResourceType = "Text",
            },
        };

        sfdo.PrimaryLanguage.Add(new() { Code = "eng" });

        sfdo.RightsURI = new Uri("https://creativecommons.org/publicdomain/zero/1.0/");

        return sfdo;
    }
}