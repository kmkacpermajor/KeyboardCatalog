using LukomskiMajorkowski.KeyboardCatalog.CORE;
using LukomskiMajorkowski.KeyboardCatalog.INTERFACES;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using LukomskiMajorkowski.KeyboardCatalog.UI_WPF.Models;

namespace LukomskiMajorkowski.KeyboardCatalog.UI_WPF.ViewModels
{
    public class ManufacturerFormViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly IDAO _dao;
        public IManufacturer Manufacturer { get; set; }

        public ICommand SaveCommand { get; }
        private Window _window;

        public ManufacturerFormViewModel(IDAO dao, Window window, IManufacturer manufacturer = null)
        {
            _dao = dao;
            _window = window;

            // Initialize the Manufacturer
            Manufacturer = manufacturer ?? new Manufacturer();

            SaveCommand = new RelayCommand<object>(_ => Save());

            // Subscribe to property changes
            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Manufacturer.Name))
                {
                    ((RelayCommand<object>)SaveCommand).RaiseCanExecuteChanged();
                }
            };
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Manufacturer.Name);
        }

        private void Save()
        {
            if (CanSave())
            {
                if (Manufacturer.Id == 0) // New manufacturer
                {
                    _dao.Add(Manufacturer);
                }
                else // Existing manufacturer
                {
                    _dao.Update(Manufacturer);
                }
                CloseForm();
            }
            else
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CloseForm()
        {
            _window.DialogResult = true; // Set dialog result to true to indicate success
            _window.Close(); // Close the window
        }

        // IDataErrorInfo implementation
        public string Error => null;

        public string this[string propertyName]
        {
            get
            {
                string result = null;
                if (propertyName == nameof(Manufacturer.Name) && string.IsNullOrWhiteSpace(Manufacturer.Name))
                {
                    result = "Name is required.";
                }
                return result;
            }
        }
    }
}
