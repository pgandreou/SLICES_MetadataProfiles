using Slices.V1.Standard;
using Slices.V1.StandardConverters.Common;
using System.Globalization;

namespace Slices.V1.StandardConverters.DublinCore;

public class DublinCoreConverter : ISlicesStandardConverter<DublinCoreResource>
{
    private readonly IStandardXmlSerializer<DublinCoreResource> _serializer;

    public DublinCoreConverter(IStandardXmlSerializer<DublinCoreResource> serializer)
    {
        _serializer = serializer;
    }

    public string ExternalStandard => DublinCoreConstants.StandardId;

    // TODO: handle lang attribute, fill not nullables on SFDO
    public SfdoResource FromExtrenal(DublinCoreResource externalModel)
    {
        SfdoResource sfdo = new();

        if (externalModel.Identifiers != null)
        {
            List<SfdoIdentifier> sfdoIdentifiers = externalModel.Identifiers
                .Select(id => TryParseIdentifier(id.Value))
                .Where(id => id != null)
                .Select(id => id!.Value)
                .ToList();

            if (sfdoIdentifiers.Count > 1)
            {
                sfdo.Identifier = sfdoIdentifiers.FirstOrDefault(
                    id => id.Type != null, // Prefer one that has a type parsed
                    sfdoIdentifiers[0] // Otherwise just pick first
                );

                // The rest become alternate identifiers
                sfdoIdentifiers.Remove(sfdo.Identifier);
                sfdo.AlternateIdentifiers = sfdoIdentifiers;
            }
            else if (sfdoIdentifiers.Count > 0)
            {
                // Only 1 ID which becomes the primary (no alts)
                sfdo.Identifier = sfdoIdentifiers[0];
            }
        }

        if (externalModel.Creators != null)
        {
            sfdo.Creators = externalModel.Creators
                .Select(c => new SfdoCreator { Name = c.Value })
                .ToList();
        }

        if (externalModel.Titles != null)
        {
            sfdo.Name = externalModel.Titles.FirstOrDefault()?.Value!;
        }

        if (externalModel.Descriptions != null)
        {
            sfdo.Description = externalModel.Descriptions.FirstOrDefault()?.Value!;
        }

        if (externalModel.Subjects != null)
        {
            sfdo.Subjects = externalModel.Subjects
                .Select(s => s.Value)
                .ToList();
        }

        if (externalModel.Contributors != null)
        {
            sfdo.Contributors = externalModel.Contributors
                .Select(c => new SfdoContributor { Name = c.Value })
                .ToList();
        }

        if (externalModel.Relations != null)
        {
            sfdo.RelatedObjects = externalModel.Relations
                .Select(r => TryParseIdentifier(r.Value))
                .Where(r => r != null)
                .Select(r => new SfdoRelationLink { Identifier = r!.Value })
                .ToList();
        }

        if (externalModel.Languages != null)
        {
            foreach (DublinCoreElement language in externalModel.Languages)
            {
                try
                {
                    CultureInfo cultureInfo = CultureInfo.GetCultureInfo(language.Value);

                    if (cultureInfo != null)
                    {
                        sfdo.PrimaryLanguage.Add(new LanguageIso639_3 { Code = cultureInfo.ThreeLetterISOLanguageName });
                    }
                }
                catch (CultureNotFoundException)
                {
                }
            }
        }

        if (externalModel.Rights != null)
        {
            sfdo.Rights = externalModel.Rights.FirstOrDefault()?.Value!;
        }

        return sfdo;
    }

    private static readonly IReadOnlySet<string> SupportedIdentifiderTypes = new HashSet<string>
    {
        SfdoIdentifierTypes.Doi,
        SfdoIdentifierTypes.Url,
        SfdoIdentifierTypes.Orcid,
        SfdoIdentifierTypes.Arxiv,
    };

    private static SfdoIdentifier? TryParseIdentifier(string originalIdentifier)
    {
        if (string.IsNullOrWhiteSpace(originalIdentifier)) return null;

        foreach (string identifierType in SupportedIdentifiderTypes)
        {
            string identifierPrefix = identifierType + ":";

            if (originalIdentifier.StartsWith(identifierPrefix, StringComparison.InvariantCultureIgnoreCase))
            {
                return new SfdoIdentifier
                {
                    Type = identifierType,
                    Value = originalIdentifier.Substring(identifierPrefix.Length),
                };
            }
        }

        return new SfdoIdentifier()
        {
            Value = originalIdentifier,
        };
    }

    public SfdoResource FromSerializedExtrenal(TextReader serializedReader, string? format)
    {
        if (format == null) format = "xml";

        if (format != "xml")
        {
            throw new ArgumentOutOfRangeException(nameof(format), "Only \"xml\" is supported");
        }

        return FromExtrenal(_serializer.FromXml(serializedReader));
    }

    public DublinCoreResource ToExtrenal(SfdoResource digitalObject)
    {
        throw new NotImplementedException();
    }

    public void ToSerializedExtrenal(SfdoResource digitalObject, string? format, TextWriter serializedWriter)
    {
        throw new NotImplementedException();
    }
}
