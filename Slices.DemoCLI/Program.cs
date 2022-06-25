using CommandLine;
using Slices.Common;
using Slices.V1.Model;
using Slices.V1.Converters.Common;
using Slices.V1.Converters.DataCite;
using Slices.V1.Converters.DublinCore;
using Slices.V1.Converters.EoscProviderProfile;

bool success = false;

await Parser.Default.ParseArguments<Options>(args)
    .WithParsedAsync(async o =>
    {
        //Console.WriteLine(Environment.CurrentDirectory);
        //Console.WriteLine(o.SourcePath);
        //Console.WriteLine(o.SourceType);
        //Console.WriteLine(o.DestinationType);

        IStandardConverterCollection converterCollection = new StandardConverterCollection(new ISlicesStandardConverter[]
        {
            new DataCiteConverter(new DataCiteImporter(), new DataCiteExporter(), new DataCiteSerializer()),
            new DublinCoreConverter(new DublinCoreImporter(), new DublinCoreExporter(), new DublinCoreSerializer()),
            new EoscProviderConverter(new EoscProviderImporter(), new EoscProviderExporter(), new EoscProviderSerializer()),
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

        await using FileStream sourceStream = new(o.SourcePath, new FileStreamOptions
        {
            Mode = FileMode.Open,
            Access = FileAccess.Read,
            Share = FileShare.Read,
        });

        await using MemoryStream destinationStream = new();

        // From external to SLICES
        SfdoResource sfdo = await converterCollection.CovertersByStandard[o.SourceType]
            .FromSerializedExternalAsync(sourceStream, null);
        
        // From SLICES to external
        await converterCollection.CovertersByStandard[o.DestinationType]
            .ToSerializedExternalAsync(sfdo, null, destinationStream);

        Console.WriteLine(destinationStream.ReadAsStringFromStart());

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
