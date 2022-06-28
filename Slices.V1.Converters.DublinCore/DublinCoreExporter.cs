using Slices.V1.Converters.Common;
using Slices.V1.Model;

namespace Slices.V1.Converters.DublinCore;

public class DublinCoreExporter : ISlicesExporter<DublinCoreResource>
{
    public DublinCoreResource ToExternal(SfdoResource sfdo)
    {
        DublinCoreResource dcResource = new();

        dcResource.Titles = new[] { new DublinCoreElement(sfdo.Name) };
        
        dcResource.Creators = sfdo.Creators
            .Select(c => new DublinCoreElement(c.Name))
            .ToArray();

        if (sfdo.Subjects.IsSet)
        {
            dcResource.Subjects = sfdo.Subjects.Value
                .Select(s => new DublinCoreElement(s))
                .ToArray();
            
        }

        if (sfdo.Description.IsSet)
        {
            dcResource.Descriptions = new[] { new DublinCoreElement(sfdo.Description.Value) };
        }

        if (sfdo.Contributors.IsSet)
        {
            dcResource.Contributors = sfdo.Contributors.Value
                .Select(c => new DublinCoreElement(c.Name))
                .ToArray();
        }

        dcResource.Dates = GenerateDates(sfdo);

        dcResource.Types = sfdo.ResourceTypes
            .Select(t => t switch
            {
                SfdoResourceType.Publication => "publication",
                SfdoResourceType.Data => "dataset",
                
                _ => throw new ArgumentOutOfRangeException(nameof(t), t, "SFDO type not supported for export")
            })
            .Select(t => new DublinCoreElement(t))
            .ToArray();
        
        dcResource.Identifiers = sfdo.AlternateIdentifiers
            .Prepend(sfdo.Identifier)
            .Select(id => new DublinCoreElement(id.ToString()))
            .ToArray();

        dcResource.Languages = sfdo.PrimaryLanguage
            .Concat(sfdo.OtherLanguages)
            .Select(l => new DublinCoreElement(l.Code))
            .ToArray();

        if (sfdo.RelatedObjects.IsSet)
        {
            dcResource.Relations = sfdo.RelatedObjects.Value
                .Select(ro => new DublinCoreElement(ro.Identifier.ToString()))
                .ToArray();
        }

        if (sfdo.Rights.IsSet)
        {
            dcResource.Rights = new[] { new DublinCoreElement(sfdo.Rights.Value) };
        }

        return dcResource;
    }

    private static DublinCoreElement[] GenerateDates(SfdoResource sfdo)
    {
        List<DateOnly> dates = new();

        // Put this first as the most "important" one
        if (sfdo.DatesIssued.IsSet) dates.AddRange(sfdo.DatesIssued.Value);
        
        // Then these ones
        if (sfdo.DateTimeStart.IsSet) dates.Add(DateOnly.FromDateTime(sfdo.DateTimeStart.Value));
        if (sfdo.DateTimeEnd.IsSet) dates.Add(DateOnly.FromDateTime(sfdo.DateTimeEnd.Value));
        
        // And now finally the ones which can have multiple values 
        if (sfdo.DateSubmitted.IsSet) dates.Add(sfdo.DateSubmitted.Value);
        if (sfdo.DatesModified.IsSet) dates.AddRange(sfdo.DatesModified.Value);
        if (sfdo.DatesAccepted.IsSet) dates.AddRange(sfdo.DatesAccepted.Value);
        if (sfdo.DatesCopyrighted.IsSet) dates.AddRange(sfdo.DatesCopyrighted.Value);
        
        return dates
            .Select(d => new DublinCoreElement(d.ToString("yyyy-MM-dd")))
            .ToArray();
    }
}