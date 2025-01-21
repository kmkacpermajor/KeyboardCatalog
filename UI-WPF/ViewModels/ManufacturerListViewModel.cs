using LukomskiMajorkowski.KeyboardCatalog.INTERFACES;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace LukomskiMajorkowski.KeyboardCatalog.UI_WPF.ViewModels
{
    public class ManufacturerListViewModel : ViewModelBase
    {
        private readonly IDAO _dao;
        public ObservableCollection<IManufacturer> Manufacturers { get; set; }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                FilterManufacturers();
            }
        }

        private ObservableCollection<IManufacturer> _filteredManufacturers;
        public ObservableCollection<IManufacturer> FilteredManufacturers
        {
            get => _filteredManufacturers;
            set
            {
                _filteredManufacturers = value;
                OnPropertyChanged(nameof(FilteredManufacturers));
            }
        }

        private IManufacturer _selectedManufacturer;
        public IManufacturer SelectedManufacturer
        {
            get => _selectedManufacturer;
            set
            {
                _selectedManufacturer = value;
                OnPropertyChanged(nameof(SelectedManufacturer));
                OnPropertyChanged(nameof(IsEditEnabled));
                OnPropertyChanged(nameof(IsDeleteEnabled));
            }
        }

        public ICommand AddManufacturerCommand { get; }
        public ICommand EditManufacturerCommand { get; }
        public ICommand DeleteManufacturerCommand { get; }

        public bool IsEditEnabled => SelectedManufacturer != null;
        public bool IsDeleteEnabled => SelectedManufacturer != null;

        public ManufacturerListViewModel(IDAO dao)
        {
            _dao = dao;

            Manufacturers = new ObservableCollection<IManufacturer>(_dao.GetAllManufacturers());
            FilteredManufacturers = new ObservableCollection<IManufacturer>(Manufacturers);

            AddManufacturerCommand = new RelayCommand<object>(_ => OpenAddManufacturerForm());
            EditManufacturerCommand = new RelayCommand<IManufacturer>(OpenEditManufacturerForm, CanEditManufacturer);
            DeleteManufacturerCommand = new RelayCommand<IManufacturer>(DeleteManufacturer, CanDeleteManufacturer);
        }

        private void OpenAddManufacturerForm()
        {
            var manufacturerForm = new ManufacturerFormWindow(_dao);
            if (manufacturerForm.ShowDialog() == true)
            {
                RefreshManufacturers();
            }
        }

        private void OpenEditManufacturerForm(IManufacturer manufacturer)
        {
            if (manufacturer == null) return;

            var manufacturerForm = new ManufacturerFormWindow(_dao, manufacturer);
            if (manufacturerForm.ShowDialog() == true)
            {
                RefreshManufacturers();
            }
        }

        private void DeleteManufacturer(IManufacturer manufacturer)
        {
            if (manufacturer == null) return;

            if (MessageBox.Show($"Are you sure you want to delete {manufacturer.Name}?", "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _dao.DeleteManufacturer(manufacturer.Id);
                RefreshManufacturers();
            }
        }

        private void RefreshManufacturers()
        {
            Manufacturers.Clear();
            var sortedManufacturers = _dao.GetAllManufacturers().OrderBy(m => m.Id);
            foreach (var manufacturer in sortedManufacturers)
            {
                Manufacturers.Add(manufacturer);
            }
            FilterManufacturers();
        }

        private void FilterManufacturers()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredManufacturers = new ObservableCollection<IManufacturer>(Manufacturers);
            }
            else
            {
                var lowerSearchText = SearchText.ToLower();
                FilteredManufacturers = new ObservableCollection<IManufacturer>(
                    Manufacturers.Where(m => m.Id.ToString().Contains(lowerSearchText) ||
                                             m.Name.ToLower().Contains(lowerSearchText)));
            }
        }

        private bool CanEditManufacturer(IManufacturer manufacturer) => manufacturer != null;

        private bool CanDeleteManufacturer(IManufacturer manufacturer) => manufacturer != null;
    }
}
