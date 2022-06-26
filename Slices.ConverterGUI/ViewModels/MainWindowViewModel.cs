using System;
using System.Collections.ObjectModel;
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

    private string _destinationValue = "";

    public MainWindowViewModel()
    {
        foreach (string standardId in _converterCollection.CovertersByStandard.Keys)
        {
            Standards.Add(new DropdownOption
            {
                Id = standardId,
                Label = standardId,
            });
        }

        SourceFormatPlaceholder = this.WhenAnyValue(x => x.SelectedSourceStandard)
            .Select(option => option is null ? "Please select the standard" : "Click to select");

        SourceFormats = this.WhenAnyValue(x => x.SelectedSourceStandard)
            .Select(standardOption =>
            {
                if (standardOption is null)
                {
                    return Array.Empty<DropdownOption>();
                }

                return _converterCollection.CovertersByStandard[standardOption.Id]
                    .SupportedFormats.Formats
                    .Where(descriptor => descriptor.IsText) // Currently we have no way to use binary data in the UI
                    .Select(descriptor => new DropdownOption
                    {
                        Id = descriptor.FormatId,
                        Label = descriptor.FormatId
                    })
                    .ToArray();
            });

        IObservable<bool> convertEnabled = this
            .WhenAnyValue(
                x => x.SelectedSourceStandard,
                x => x.SelectedSourceFormat
            )
            .Select(tuple =>
                tuple.Item1 is not null &&
                tuple.Item2 is not null
            );

        Convert = ReactiveCommand.Create(
            () => { DestinationValue += "1"; },
            convertEnabled
        );
    }

    public ObservableCollection<DropdownOption> Standards { get; } = new();

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

    public ReactiveCommand<Unit, Unit> Convert { get; }

    public string DestinationValue
    {
        get => _destinationValue;
        set => this.RaiseAndSetIfChanged(ref _destinationValue, value);
    }
}