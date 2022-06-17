namespace Slices.V1.Model.Annotations;

/// <summary>
/// When set on a property, indicates that the property will have a value only when the
/// SfdoResource::ResourceTypes contains ones of the specified types 
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class SlicesAssociatedResourceTypes : Attribute
{
    public readonly SfdoResourceType[] Types;

    public SlicesAssociatedResourceTypes(params SfdoResourceType[] types)
    {
        Types = types;
    }
}