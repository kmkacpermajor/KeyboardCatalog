using LukomskiMajorkowski.KeyboardCatalog.CORE;
using LukomskiMajorkowski.KeyboardCatalog.INTERFACES;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using LukomskiMajorkowski.KeyboardCatalog.UI_WPF.Models;

namespace LukomskiMajorkowski.KeyboardCatalog.UI_WPF.ViewModels
{
    public class ProductFormViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly IDAO _dao;
        public IProduct Product { get; set; }
        public ObservableCollection<IManufacturer> Manufacturers { get; set; }

        public IEnumerable<string> KeyboardTypes => Enum.GetNames(typeof(KeyboardType));

        public ICommand SaveCommand { get; }

        private Window _window;

        public ProductFormViewModel(IDAO dao,
                            Window window,
                            IProduct product = null)
        {
            _dao = dao;
            _window = window;

            Product = product ?? new Product();

            Manufacturers = new ObservableCollection<IManufacturer>(_dao.GetAllManufacturers());

            if (Product.Id == 0 && Manufacturers.Count > 0)
            {
                Product.Manufacturer = Manufacturers[0]; // Set to the first item
            }

            SaveCommand = new RelayCommand<object>(_ => Save());
        }

        private bool CanSave()
        {
            bool isValid = !string.IsNullOrWhiteSpace(Product.Name) &&
                           Product.Manufacturer != null;

            return isValid;
        }


        private void Save()
        {
            if (CanSave()) // Validate before saving
            {
                if (Product.Id == 0) // New product
                {
                    _dao.Add(Product);
                }
                else // Existing product
                {
                    _dao.Update(Product);
                }
                CloseForm(); // Close the form after saving
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
                switch (propertyName)
                {
                    case nameof(Product.Name):
                        if (string.IsNullOrWhiteSpace(Product.Name))
                            result = "Name is required.";
                        break;
                    case nameof(Product.Type):
                        if (Product.Type != default(KeyboardType)) // Check for a valid type
                            result = "Type is required.";
                        break;
                    case nameof(Product.Manufacturer):
                        if (Product.Manufacturer == null)
                            result = "Manufacturer is required.";
                        break;
                }
                return result;
            }
        }
    }
}
