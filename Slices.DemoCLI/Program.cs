using CommandLine;
using Slices.V1.Model;
using Slices.V1.Converters.Common;
using Slices.V1.Converters.DataCite;
using Slices.V1.Converters.DublinCore;

bool success = false;

Parser.Default.ParseArguments<Options>(args)
    .WithParsed(o =>
    {
        //Console.WriteLine(Environment.CurrentDirectory);
        //Console.WriteLine(o.SourcePath);
        //Console.WriteLine(o.SourceType);
        //Console.WriteLine(o.DestinationType);

        IStandardConverterCollection converterCollection = new StandardConverterCollection(new ISlicesStandardConverter[]
        {
            new DataCiteConverter(new DataCiteImporter(), new DataCiteExporter(), new DataCiteSerializer()),
            new DublinCoreConverter(new DublinCoreImporter(), new DublinCoreExporter(), new DublinCoreSerializer()),
        });

        if (!converterCollection.CovertersByStandard.ContainsKey(o.SourceType))
        {
            Console.Error.WriteLine("Invalid source type");
            return;
        }

        if (!converterCollection.CovertersByStandard.ContainsKey(o.DestinationType))
        {
            Console.Error.WriteLine("Invalid destination type");
            return;
        }

        StringWriter writer = new();
        using StreamReader reader = new(new FileStream(o.SourcePath, new FileStreamOptions
        {
            Mode = FileMode.Open,
            Access = FileAccess.Read,
            Share = FileShare.Read,
        }));

        SfdoResource sfdo = converterCollection.CovertersByStandard[o.SourceType].FromSerializedExternal(reader, null);
        converterCollection.CovertersByStandard[o.DestinationType].ToSerializedExternal(sfdo, null, writer);

        Console.WriteLine(writer.ToString());

        success = true;
    });

return success ? 0 : 1;

public class Options
{
    [Option(Required = true)]
    public string SourcePath { get; set; } = null!;

    [Option(Required = true)]
    public string SourceType { get; set; } = null!;

    [Option(Required = true)]
    public string DestinationType { get; set; } = null!;
}
