using System.Collections.ObjectModel;
using ReactiveUI;
using Slices.V1.Converters.Common;
using Slices.V1.Converters.DataCite;
using Slices.V1.Converters.DublinCore;
using Slices.V1.Converters.EoscProviderProfile;

namespace Slices.ConverterGUI.ViewModels
{
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
        }

        public ObservableCollection<DropdownOption> Standards { get; } = new();

        public DropdownOption? SelectedSourceStandard
        {
            get => _selectedSourceStandard;
            set => this.RaiseAndSetIfChanged(ref _selectedSourceStandard, value);
        }
    }
}