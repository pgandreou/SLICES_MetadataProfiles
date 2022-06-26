using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Slices.V1.Converters.Common;
using Slices.V1.Converters.DataCite;
using Slices.V1.Converters.DublinCore;
using Slices.V1.Converters.EoscProviderProfile;

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
                x => x.SelectedDestinationFormat
            )
            .Select(tuple =>
                tuple.Item1 is not null &&
                tuple.Item2 is not null &&
                tuple.Item3 is not null &&
                tuple.Item4 is not null
            );

        Convert = ReactiveCommand.Create(
            () => { DestinationValue += "1"; },
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
            .SupportedFormats.Formats.Where(descriptor => descriptor.IsText) // Currently we have no way to use binary data in the UI
            .Select(descriptor => new DropdownOption { Id = descriptor.FormatId, Label = descriptor.FormatId })
            .ToArray();
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

    public string DestinationValue
    {
        get => _destinationValue;
        set => this.RaiseAndSetIfChanged(ref _destinationValue, value);
    }
}