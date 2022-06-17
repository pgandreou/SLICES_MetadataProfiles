using Slices.V1.Converters.Common;
using System.Globalization;
using Slices.V1.Model;

namespace Slices.V1.Converters.DublinCore;

public class DublinCoreConverter : ISlicesStandardConverter<DublinCoreResource>
{
    private readonly IStandardXmlSerializer<DublinCoreResource> _serializer;

    public DublinCoreConverter(IStandardXmlSerializer<DublinCoreResource> serializer)
    {
        _serializer = serializer;
    }

    public string ExternalStandard => DublinCoreConstants.StandardId;

    // TODO: handle lang attribute, fill not nullables on SFDO
    public SfdoResource FromExternal(DublinCoreResource externalModel)
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

    public SfdoResource FromSerializedExternal(TextReader serializedReader, string? format)
    {
        if (format == null) format = "xml";

        if (format != "xml")
        {
            throw new ArgumentOutOfRangeException(nameof(format), "Only \"xml\" is supported");
        }

        return FromExternal(_serializer.FromXml(serializedReader));
    }

    public DublinCoreResource ToExternal(SfdoResource sfdo)
    {
        DublinCoreResource dcResource = new();

        dcResource.Titles = new[] { new DublinCoreElement(sfdo.Name) };
        
        dcResource.Creators = sfdo.Creators
            .Select(c => new DublinCoreElement(c.Name))
            .ToArray();

        dcResource.Subjects = sfdo.Subjects
            .Select(s => new DublinCoreElement(s))
            .ToArray();

        if (sfdo.Description != null)
        {
            dcResource.Descriptions = new[] { new DublinCoreElement(sfdo.Description) };
        }

        dcResource.Contributors = sfdo.Contributors
            .Select(c => new DublinCoreElement(c.Name))
            .ToArray();

        dcResource.Identifiers = sfdo.AlternateIdentifiers
            .Prepend(sfdo.Identifier)
            .Select(id => new DublinCoreElement(id.ToString()))
            .ToArray();

        dcResource.Languages = sfdo.PrimaryLanguage
            .Concat(sfdo.OtherLanguages)
            .Select(l => new DublinCoreElement(l.Code))
            .ToArray();

        dcResource.Relations = sfdo.RelatedObjects
            .Select(ro => new DublinCoreElement(ro.Identifier.ToString()))
            .ToArray();

        dcResource.Rights = new[] { new DublinCoreElement(sfdo.Rights) };

        return dcResource;
    }

    public void ToSerializedExternal(SfdoResource sfdo, string? format, TextWriter serializedWriter)
    {
        if (format == null) format = "xml";

        if (format != "xml")
        {
            throw new ArgumentOutOfRangeException(nameof(format), "Only \"xml\" is supported");
        }

        _serializer.ToXml(ToExternal(sfdo), serializedWriter);
    }
}
