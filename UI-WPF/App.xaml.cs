using BL;
using INTERFACES;
using System.Configuration;
using System.Data;
using System.Windows;

namespace UI_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Use the factory to create repository instances
            IProductRepository productRepository = RepositoryFactory.CreateProductRepository();
            IManufacturerRepository manufacturerRepository = RepositoryFactory.CreateManufacturerRepository();

            // Pass repositories to the main window
            var mainWindow = new ProductListWindow(productRepository, manufacturerRepository);
            mainWindow.Show();
        }
    }

}
