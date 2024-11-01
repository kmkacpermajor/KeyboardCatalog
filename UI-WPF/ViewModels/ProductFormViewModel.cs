using CORE;
using INTERFACES;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using UI_WPF.Models;

namespace UI_WPF.ViewModels
{
    public class ProductFormViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly IProductRepository _productRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        public IProduct Product { get; set; }
        public ObservableCollection<IManufacturer> Manufacturers { get; set; }

        public IEnumerable<string> KeyboardTypes => Enum.GetNames(typeof(KeyboardType));

        public ICommand SaveCommand { get; }

        private Window _window;

        public ProductFormViewModel(IProductRepository productRepository,
                            IManufacturerRepository manufacturerRepository,
                            Window window,
                            IProduct product = null)
        {
            _productRepository = productRepository;
            _manufacturerRepository = manufacturerRepository;
            _window = window;

            // Initialize the Product
            Product = product ?? new Product();

            // Populate the Manufacturers collection
            Manufacturers = new ObservableCollection<IManufacturer>(_manufacturerRepository.GetAllManufacturers());

            // Set the default selected Manufacturer if available
            if (Product.Id == 0 && Manufacturers.Count > 0)
            {
                Product.Manufacturer = Manufacturers[0]; // Set to the first item
            }

            SaveCommand = new RelayCommand<object>(_ => Save());

            // Subscribe to property changes
            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Product.Name) ||
                    args.PropertyName == nameof(Product.Type) ||
                    args.PropertyName == nameof(Product.Manufacturer))
                {
                    ((RelayCommand<object>)SaveCommand).RaiseCanExecuteChanged();
                }
            };
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
                    _productRepository.Add(Product);
                }
                else // Existing product
                {
                    _productRepository.Update(Product);
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
