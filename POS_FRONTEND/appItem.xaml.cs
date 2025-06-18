using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace POS_FRONTEND
{
    public partial class appItem : Window
    {
        private IManufacturerService manufacturerService; // Interface to simulate manufacturer data retrieval

        public appItem()
        {
            InitializeComponent();
            manufacturerService = new ManufacturerService(); // You can inject this via Dependency Injection if needed
        }

        // Auto-generate Product ID when category is selected
        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Implement logic when the category is selected
            string selectedCategory = cmbCategory.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedCategory))
            {
                // For example, set a default value for P_ID when a category is selected
                // Assuming you have logic to generate P_ID based on the selected category
                txtP_ID.Text = GenerateProductId(selectedCategory); // Example method for generating P_ID
            }
        }

        private string GenerateProductId(string category)
        {
            // Logic for generating Product ID, e.g., combining category name with a random number or other method
            return category.Substring(0, 3).ToUpper() + new Random().Next(1000, 9999).ToString();
        }

        // Add this method to handle the SelectionChanged event for cmbSupplier
        private void cmbSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Implement logic when the supplier is selected
            string selectedSupplier = cmbSupplier.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedSupplier))
            {
                // For example, you can perform some action based on the selected supplier
                // You can add logic here, like updating related fields based on the supplier
                Console.WriteLine($"Supplier selected: {selectedSupplier}");
            }
        }



        // When cost or sale price is entered, calculate profit
        private void PriceFields_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse(txtCostPrice.Text, out decimal cost) && decimal.TryParse(txtSalePrice.Text, out decimal sale))
            {
                decimal profit = sale - cost;
                txtPriceDifference.Text = profit.ToString("F2");
            }
        }

        // Reload manufacturers from database
        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            List<string> manufacturers = manufacturerService.GetManufacturers(); // Get manufacturers from service (abstraction)
            cmbManufacturer.ItemsSource = manufacturers;
        }
    }

    // Abstraction for manufacturer data retrieval
    public interface IManufacturerService
    {
        List<string> GetManufacturers();
    }

    // Example implementation of the service
    public class ManufacturerService : IManufacturerService
    {
        public List<string> GetManufacturers()
        {
            // Simulate getting manufacturers from a database or other data source
            return new List<string> { "Manufacturer A", "Manufacturer B", "Manufacturer C" };
        }
    }
}
