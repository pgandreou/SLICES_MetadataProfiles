using System;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Slices.Common;
using Slices.V1.Converters.Common;
using Slices.V1.Converters.Common.Exceptions;
using Slices.V1.Converters.DataCite;
using Slices.V1.Converters.DublinCore;
using Slices.V1.Converters.EoscProviderProfile;
using Slices.V1.Model;

namespace Slices.ConverterGUI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public class DropdownOption
    {
        public string Id { get; init; } = null!;
        public string Label { get; init; } = "";
    }

    private readonly IStandardConverterCollection _converterCollection = new StandardConverterCollection(
        new ISlicesStandardConverter[]
        {
            new DataCiteConverter(
                new DataCiteImporter(), new DataCiteExporter(), new DataCiteSerializer()
            ),

            new DublinCoreConverter(
                new DublinCoreImporter(), new DublinCoreExporter(), new DublinCoreSerializer()
            ),

            new EoscProviderConverter(
                new EoscProviderImporter(), new EoscProviderExporter(), new EoscProviderSerializer()
            ),
        }
    );

    private DropdownOption? _selectedSourceStandard;
    private DropdownOption? _selectedSourceFormat;

    private DropdownOption? _selectedDestinationStandard;
    private DropdownOption? _selectedDestinationFormat;

    private string _destinationValue = "";
    private string _sourceValue = "";

    public MainWindowViewModel()
    {
        Standards = _converterCollection.CovertersByStandard.Keys
            .Select(standardId => new DropdownOption
            {
                Id = standardId,
                Label = standardId,
            })
            .ToArray();

        SourceFormatPlaceholder = this.WhenAnyValue(x => x.SelectedSourceStandard)
            .Select(StandardToFormatPlaceholder);

        SourceFormats = this.WhenAnyValue(x => x.SelectedSourceStandard)
            .Select(StandardToFormatOptions);

        DestinationFormatPlaceholder = this.WhenAnyValue(x => x.SelectedDestinationStandard)
            .Select(StandardToFormatPlaceholder);

        DestinationFormats = this.WhenAnyValue(x => x.SelectedDestinationStandard)
            .Select(StandardToFormatOptions);

        IObservable<bool> convertEnabled = this
            .WhenAnyValue(
                x => x.SelectedSourceStandard,
                x => x.SelectedSourceFormat,
                x => x.SelectedDestinationStandard,
                x => x.SelectedDestinationFormat,
                x => x.SourceValue
            )
            .Select(tuple =>
                tuple.Item1 is not null &&
                tuple.Item2 is not null &&
                tuple.Item3 is not null &&
                tuple.Item4 is not null &&
                !string.IsNullOrWhiteSpace(tuple.Item5)
            );

        Convert = ReactiveCommand.CreateFromTask(
            DoConvert,
            convertEnabled
        );
    }

    private static string StandardToFormatPlaceholder(DropdownOption? option)
    {
        return option is null ? "Please select the standard" : "Click to select";
    }

    private DropdownOption[] StandardToFormatOptions(DropdownOption? standardOption)
    {
        if (standardOption is null)
        {
            return Array.Empty<DropdownOption>();
        }

        return _converterCollection.CovertersByStandard[standardOption.Id]
            .SupportedFormats.Formats
            .Where(descriptor => descriptor.IsText) // Currently we have no way to use binary data in the UI
            .Select(descriptor => new DropdownOption { Id = descriptor.FormatId, Label = descriptor.FormatId })
            .ToArray();
    }

    private async Task DoConvert()
    {
        DestinationValue = "";
        
        await using MemoryStream sourceStream = SourceValueAsStream();
        await using MemoryStream destinationStream = new();

        SfdoResource sfdo;
        try
        {
            // From external to SLICES
            sfdo = await _converterCollection.CovertersByStandard[SelectedSourceStandard!.Id]
                .FromSerializedExternalAsync(sourceStream, SelectedSourceFormat!.Id);
        }
        catch (StandardSerializationException)
        {
            DestinationValue = "Failed to parse source value";
            return;
        }
        
        try
        {
            // From SLICES to external
            await _converterCollection.CovertersByStandard[SelectedDestinationStandard!.Id]
                .ToSerializedExternalAsync(sfdo, SelectedDestinationFormat!.Id, destinationStream);
        }
        catch (StandardSerializationException)
        {
            DestinationValue = "Failed to convert output to text";
            return;
        }

        DestinationValue = destinationStream.ReadAsStringFromStart();
    }

    private MemoryStream SourceValueAsStream()
    {
        MemoryStream sourceStream = new();
        
        using (StreamWriter writer = new(sourceStream, leaveOpen: true))
        {
            writer.Write(SourceValue);
        }
        
        // This needs to be called after StreamWriter is disposed - otherwise the position jumps somewhere
        sourceStream.Seek(0, SeekOrigin.Begin);

        return sourceStream;
    }

    public DropdownOption[] Standards { get; }

    public IObservable<string> SourceFormatPlaceholder { get; }

    public IObservable<DropdownOption[]> SourceFormats { get; }

    public DropdownOption? SelectedSourceStandard
    {
        get => _selectedSourceStandard;
        set => this.RaiseAndSetIfChanged(ref _selectedSourceStandard, value);
    }

    public DropdownOption? SelectedSourceFormat
    {
        get => _selectedSourceFormat;
        set => this.RaiseAndSetIfChanged(ref _selectedSourceFormat, value);
    }

    public IObservable<string> DestinationFormatPlaceholder { get; }

    public IObservable<DropdownOption[]> DestinationFormats { get; }

    public DropdownOption? SelectedDestinationStandard
    {
        get => _selectedDestinationStandard;
        set => this.RaiseAndSetIfChanged(ref _selectedDestinationStandard, value);
    }

    public DropdownOption? SelectedDestinationFormat
    {
        get => _selectedDestinationFormat;
        set => this.RaiseAndSetIfChanged(ref _selectedDestinationFormat, value);
    }

    public ReactiveCommand<Unit, Unit> Convert { get; }

    public string SourceValue
    {
        get => _sourceValue;
        set => this.RaiseAndSetIfChanged(ref _sourceValue, value);
    }

    public string DestinationValue
    {
        get => _destinationValue;
        set => this.RaiseAndSetIfChanged(ref _destinationValue, value);
    }
}