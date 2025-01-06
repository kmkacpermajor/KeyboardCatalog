using LukomskiMajorkowski.KeyboardCatalog.INTERFACES;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace LukomskiMajorkowski.KeyboardCatalog.UI_WPF.ViewModels
{
    public class ManufacturerListViewModel : ViewModelBase
    {
        private readonly IDAO _dao;

        public ObservableCollection<IManufacturer> Manufacturers { get; set; }

        private IManufacturer _selectedManufacturer;
        public IManufacturer SelectedManufacturer
        {
            get => _selectedManufacturer;
            set
            {
                _selectedManufacturer = value;
                OnPropertyChanged(nameof(SelectedManufacturer));
                OnPropertyChanged(nameof(IsEditEnabled)); // Notify that IsEditEnabled has changed
                OnPropertyChanged(nameof(IsDeleteEnabled)); // Notify that IsDeleteEnabled has changed
            }
        }

        public ICommand AddManufacturerCommand { get; }
        public ICommand EditManufacturerCommand { get; }
        public ICommand DeleteManufacturerCommand { get; }

        public bool IsEditEnabled => SelectedManufacturer != null; // Inline check for Edit button
        public bool IsDeleteEnabled => SelectedManufacturer != null; // Inline check for Delete button

        public ManufacturerListViewModel(IDAO dao)
        {
            _dao = dao;

            Manufacturers = new ObservableCollection<IManufacturer>(_dao.GetAllManufacturers());

            AddManufacturerCommand = new RelayCommand<object>(_ => OpenAddManufacturerForm());
            EditManufacturerCommand = new RelayCommand<IManufacturer>(OpenEditManufacturerForm, CanEditManufacturer);
            DeleteManufacturerCommand = new RelayCommand<IManufacturer>(DeleteManufacturer, CanDeleteManufacturer);
        }

        private void OpenAddManufacturerForm()
        {
            var manufacturerForm = new ManufacturerFormWindow(_dao);
            if (manufacturerForm.ShowDialog() == true) // Check if the form was successful
            {
                RefreshManufacturers(); // Refresh the list after adding a new manufacturer
            }
        }

        private void OpenEditManufacturerForm(IManufacturer manufacturer)
        {
            if (manufacturer == null) return;

            var manufacturerForm = new ManufacturerFormWindow(_dao, manufacturer);
            if (manufacturerForm.ShowDialog() == true) // Check if the form was successful
            {
                RefreshManufacturers(); // Refresh the list after editing
            }
        }

        private void DeleteManufacturer(IManufacturer manufacturer)
        {
            if (manufacturer == null) return;

            // Confirm deletion
            if (MessageBox.Show($"Are you sure you want to delete {manufacturer.Name}?", "Confirm Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _dao.DeleteManufacturer(manufacturer.Id);
                RefreshManufacturers();
            }
        }

        private void RefreshManufacturers()
        {
            Manufacturers.Clear();
            var sortedManufacturers = _dao.GetAllManufacturers().OrderBy(m => m.Id); // Sort by ID
            foreach (var manufacturer in sortedManufacturers)
            {
                Manufacturers.Add(manufacturer);
            }
        }

        private bool CanEditManufacturer(IManufacturer manufacturer)
        {
            return manufacturer != null;
        }

        private bool CanDeleteManufacturer(IManufacturer manufacturer)
        {
            return manufacturer != null;
        }
    }
}
