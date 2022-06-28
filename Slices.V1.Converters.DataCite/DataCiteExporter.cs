using Slices.V1.Converters.Common;
using Slices.V1.Converters.DataCite.Model;
using Slices.V1.Model;

namespace Slices.V1.Converters.DataCite;

public class DataCiteExporter : ISlicesExporter<DataCiteResource>
{
    public DataCiteResource ToExternal(SfdoResource sfdo)
    {
        DataCiteResource dataCiteResource = new();

        dataCiteResource.identifier = new()
        {
            identifierType = sfdo.Identifier.Type,
            Value = sfdo.Identifier.Value,
        };

        dataCiteResource.creators = sfdo.Creators
            .Select(c =>
            {
                DataCiteCreator dataCiteCreator = new()
                {
                    creatorName = new() { Value = c.Name },
                };

                if (c.Identifier.HasValue)
                {
                    dataCiteCreator.nameIdentifier = new[] { new DataCiteNameIdentifier() {
                        nameIdentifierScheme = c.Identifier.Value.Type,
                        Value = c.Identifier.Value.Value,
                    }};
                }

                return dataCiteCreator;
            })
            .ToArray();

        dataCiteResource.titles = new[] { new DataCiteResourceTitle() { Value = sfdo.Name } };

        if (sfdo.Subjects.IsSet)
        {
            dataCiteResource.subjects = sfdo.Subjects.Value
                .Select(s => new DataCiteResourceSubject { Value = s })
                .ToArray();
        }

        if (sfdo.Contributors.IsSet)
        {
            dataCiteResource.contributors = sfdo.Contributors.Value
                .Select(c =>
                {
                    DataCiteResourceContributor dataCiteContributor = new DataCiteResourceContributor
                    {
                        contributorName = new() { Value = c.Name },
                    };

                    if (c.Identifier.HasValue)
                    {
                        dataCiteContributor.nameIdentifier = new[] { new DataCiteNameIdentifier() {
                            nameIdentifierScheme = c.Identifier.Value.Type,
                            Value = c.Identifier.Value.Value,
                        }};
                    }

                    return dataCiteContributor;
                })
                .ToArray();
        }

        // TODO: DataCite doesn't use iso-3
        dataCiteResource.language = sfdo.PrimaryLanguage.FirstOrDefault().Code;

        dataCiteResource.alternateIdentifiers = sfdo.AlternateIdentifiers
            .Select(id => new DataCiteResourceAlternateIdentifier { Type = id.Type, Value = id.Value })
            .ToArray();

        if (sfdo.RelatedObjects.IsSet)
        {
            dataCiteResource.relatedIdentifiers = sfdo.RelatedObjects.Value
                .Select(ro =>
                {
                    DataCiteResourceRelatedIdentifier dcRelatedId = new();

                    {
                        if (Enum.TryParse(ro.Identifier.Type, ignoreCase: true, out DataCiteRelatedIdentifierType dataCiteType))
                        {
                            dcRelatedId.relatedIdentifierType = dataCiteType;
                            dcRelatedId.Value = ro.Identifier.Value;
                        }
                        else
                        {
                            dcRelatedId.relatedIdentifierType = DataCiteRelatedIdentifierType.Handle;
                            dcRelatedId.Value = ro.Identifier.ToString();
                        }
                    }

                    if (ro.ResourceType != null)
                    {
                        dcRelatedId.resourceTypeGeneralSpecified = true;

                        if (Enum.TryParse(ro.ResourceType, ignoreCase: true, out DataCiteResourceTypeGeneral dataCiteType))
                        {
                            dcRelatedId.resourceTypeGeneral = dataCiteType;
                        }
                        else
                        {
                            dcRelatedId.resourceTypeGeneral = DataCiteResourceTypeGeneral.Other;
                        }
                    }

                    dcRelatedId.relationType = Enum.Parse<DataCiteRelationType>(ro.RelationshipType!); // TODO: this is very prone to errors

                    return dcRelatedId;
                })
                .ToArray();
        }

        if (sfdo.Version.IsSet)
        {
            dataCiteResource.version = sfdo.Version.Value;
        }

        if (sfdo.Rights.IsSet || sfdo.RightsURI.IsSet)
        {
            dataCiteResource.rightsList = new[] { new DataCiteResourceRights
            {
                rightsURI = sfdo.RightsURI.ValueOrDefault()?.ToString(),
                Value = sfdo.Rights.ValueOrDefault(),
            }};
        }

        if (sfdo.Description.IsSet)
        {
            dataCiteResource.descriptions = new[]
            {
                new DataCiteResourceDescription { Text = sfdo.Description.Value }
            };
        }

        return dataCiteResource;
    }
}