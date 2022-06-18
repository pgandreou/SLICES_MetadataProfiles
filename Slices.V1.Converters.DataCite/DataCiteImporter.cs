﻿using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Slices.V1.Converters.Common;
using Slices.V1.Converters.DataCite.Model;
using Slices.V1.Model;

namespace Slices.V1.Converters.DataCite;

public class DataCiteImporter : ISlicesImporter<DataCiteResource>
{
    public SfdoResource FromExternal(DataCiteResource externalModel)
    {
        SfdoResource sfdo = new();

        sfdo.Identifier = new()
        {
            Type = externalModel.identifier.identifierType,
            Value = externalModel.identifier.Value
        };

        sfdo.AlternateIdentifiers = externalModel.alternateIdentifiers
            .Select(id => new SfdoIdentifier { Type = id.Type, Value = id.Value })
            .ToList();

        sfdo.Creators = externalModel.creators
            .Select(c => new SfdoCreator
            {
                Name = c.creatorName.Value,
                Identifier = PickIdentifierForCreatorLike(c.nameIdentifier),
            })
            .ToList();

        if (PickBestByLang(externalModel.language, externalModel.titles, t => t.lang, out DataCiteResourceTitle? title))
        {
            sfdo.Name = title.Value;
        }

        if (PickBestByLang(externalModel.language, externalModel.descriptions, t => t.lang, out DataCiteResourceDescription? description))
        {
            sfdo.Description = description.Text;
        }

        sfdo.Subjects = PickBestByLang(externalModel.language, externalModel.subjects, s => s.lang)
            .Select(s => s.Value)
            .ToList();

        // TODO: keywords

        //slicesObject.DateTimeCreated = DateTime.Now;

        sfdo.Version = externalModel.version;

        if (externalModel.contributors != null)
        {
            sfdo.Contributors = externalModel.contributors
                .Select(c => new SfdoContributor
                {
                    Name = c.contributorName.Value,
                    Identifier = PickIdentifierForCreatorLike(c.nameIdentifier),
                })
                .ToList();
        }

        sfdo.RelatedObjects = externalModel.relatedIdentifiers
            .Select(ri => new SfdoRelationLink
            {
                Identifier = new SfdoIdentifier { Type = ri.relatedIdentifierType.ToString(), Value = ri.Value },
                RelationshipType = ri.relationType.ToString(),
                ResourceType = ri.resourceTypeGeneralSpecified ? ri.relatedIdentifierType.ToString() : null,
            })
            .ToList();

        if (externalModel.language != null)
        {
            try
            {
                CultureInfo culture = CultureInfo.GetCultureInfo(externalModel.language);

                sfdo.PrimaryLanguage.Add(new LanguageIso639_3 { Code = culture.ThreeLetterISOLanguageName });
            }
            catch (CultureNotFoundException)
            {
            }
        }

        if (PickBestByLang(externalModel.language, externalModel.rightsList, t => t.lang, out DataCiteResourceRights? rights))
        {
            sfdo.Rights = rights.Value;
            sfdo.RightsURI = new Uri(rights.rightsURI);
        }

        return sfdo;
    }

    private static SfdoIdentifier? PickIdentifierForCreatorLike(DataCiteNameIdentifier[]? dataCiteNameIdentifiers)
    {
        if (dataCiteNameIdentifiers == null) return null;

        // Try find one with scheme set
        DataCiteNameIdentifier? dcId = dataCiteNameIdentifiers.FirstOrDefault(id => !string.IsNullOrWhiteSpace(id.nameIdentifierScheme));

        // If failed, pick just first
        if (dcId == null)
        {
            dcId = dataCiteNameIdentifiers.FirstOrDefault();
        }

        // If still nothing, then we have no id
        if (dcId == null) return null;

        return new SfdoIdentifier
        {
            Type = dcId.nameIdentifierScheme,
            Value = dcId.Value,
        };
    }

    private static bool PickBestByLang<TItem>(
        string? resourceLang,
        IEnumerable<TItem> items,
        Func<TItem, string> langSelector,
        [NotNullWhen(true)] out TItem? selectedItem
    )
        where TItem : class
    {
        selectedItem = PickBestByLang(resourceLang, items, langSelector).FirstOrDefault();

        return selectedItem != null;
    }

    private static IEnumerable<TItem> PickBestByLang<TItem>(
        string? resourceLang,
        IEnumerable<TItem> items,
        Func<TItem, string> langSelector
    )
        where TItem : class
    {
        if (!items.Any()) return Array.Empty<TItem>();

        (string? Lang, TItem Item)[] langedItems = DeriveItemLangs(items, langSelector).ToArray();

        // Prefer English
        IEnumerable<(string? Lang, TItem Item)> selectedTuples = langedItems.Where(t => t.Lang == "en");

        // Otherwise prefer the resource lang
        if (!selectedTuples.Any())
        {
            resourceLang = GeneralizeLang(resourceLang);

            if (!string.IsNullOrWhiteSpace(resourceLang))
            {
                selectedTuples = langedItems.Where(t => t.Lang == resourceLang);
            }
        }

        // Otherwise try to find something that has a lang set
        if (!selectedTuples.Any())
        {
            selectedTuples = langedItems.Where(t => !string.IsNullOrWhiteSpace(t.Lang));
        }

        // Otherwise just pick all
        if (!selectedTuples.Any())
        {
            selectedTuples = langedItems;
        }

        return selectedTuples.Select(t => t.Item);
    }

    private static IEnumerable<(string? Lang, TItem Item)> DeriveItemLangs<TItem>(
        IEnumerable<TItem> items,
        Func<TItem, string> langSelector
    )
        => items.Select(item => (GeneralizeLang(langSelector(item)), item));

    private static string? GeneralizeLang(string? lang)
    {
        if (lang == null) return null;

        int separatorIndex = lang.IndexOf("-");

        if (separatorIndex > 0)
        {
            return lang[..separatorIndex];
        }

        return lang;
    }
}