using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Windows;
using Microsoft.Win32;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using System.Windows.Controls;
using POSSystem;

namespace POS_FRONTEND
{
    public partial class Sales : Window
    {
        public Sales()
        {
            InitializeComponent();
            LoadSalesData(); // Load sales data when window is initialized
        }

        private void ExportCsvButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Open SaveFileDialog to let the user choose where to save the file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                saveFileDialog.FileName = "SalesData.csv"; // Default file name

                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;

                    // Open the file for writing
                    using (var writer = new StreamWriter(filePath))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        // Write the header row (column names) using the CSVHelper library
                        csv.WriteHeader<SalesTransaction>();
                        csv.NextRecord(); // Move to the next line after writing the header

                        // Write each sales transaction record
                        foreach (SalesTransaction transaction in SalesDataGrid.Items)
                        {
                            csv.WriteRecord(transaction); // Write the entire record
                            csv.NextRecord(); // Move to the next line
                        }
                    }

                    // Notify the user that the export was successful
                    MessageBox.Show("Sales data has been exported to CSV successfully.", "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the export process
                MessageBox.Show($"An error occurred while exporting the data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Goto_Settings(object sender, RoutedEventArgs e)
        {
            settings spage = new settings();
            SetWindowProperties(spage);
            spage.Show();
            this.Close();
        }

        private void Goto_Reports(object sender, RoutedEventArgs e)
        {
            reports myreports = new reports();
            SetWindowProperties(myreports);
            myreports.Show();
            this.Close();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Show a confirmation dialog
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to logout?",
                "Logout Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Close the current window (Logout action)
                this.Close();

                // Show the login page again
                LoginPage loginPage = new LoginPage();
                SetWindowProperties(loginPage);
                loginPage.Show();
            }
        }

        private void Goto_Items(object sender, RoutedEventArgs e)
        {
            ItemsPage ipage = new ItemsPage();
            SetWindowProperties(ipage);
            ipage.Show();
            this.Close();
        }

        private void Goto_Cat(object sender, RoutedEventArgs e)
        {
            Categories catpage = new Categories();
            SetWindowProperties(catpage);
            catpage.Show();
            this.Close();
        }

        // Method to set common window properties
        private void SetWindowProperties(Window window)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Maximized;
            }
            else
            {
                window.Width = this.Width;
                window.Height = this.Height;
                window.Left = this.Left;
                window.Top = this.Top;
            }
        }

        // Method to load sales data
        private void LoadSalesData(string startDateFormatted = null, string endDateFormatted = null)
        {
            try
            {
                var salesData = GetSalesDataFromDatabase(startDateFormatted, endDateFormatted);
                SalesDataGrid.ItemsSource = salesData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load sales data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Method to fetch sales data from the database
        private List<SalesTransaction> GetSalesDataFromDatabase(string startDateFormatted, string endDateFormatted)
        {
            List<SalesTransaction> salesData = new List<SalesTransaction>();

            using (var connection = DBConnection.GetConnection()) // Get a new connection each time
            {
                try
                {
                    connection.Open(); // Open the connection

                    // Base query
                    string query = @"
                        SELECT 
                            t.TransactionDate AS 'Time of Sale', 
                            t.TransactionID AS 'Transaction ID', 
                            p.ProductName AS 'Product Name', 
                            td.Quantity AS 'Quantity Sold', 
                            p.SalesPrice AS 'Price', 
                            (td.Quantity * p.SalesPrice) AS 'Total Sales', 
                            t.TotalAmount AS 'Transaction Total', 
                            t.PaidAmount AS 'Paid Amount',
                            t.ChangeAmount AS 'Change Amount',
                            u.FullName AS 'Handled By',
                            ((p.SalesPrice - p.CostPrice) * td.Quantity) AS 'Profit',
                            c.CategoryName AS 'Category'
                        FROM  
                            TransactionDetails td 
                        INNER JOIN Products p ON td.ProductID = p.ProductID 
                        INNER JOIN Transactions t ON td.TransactionID = t.TransactionID 
                        INNER JOIN Users u ON t.UserID = u.UserID 
                        INNER JOIN Categories c ON p.CategoryID = c.CategoryID 
                        WHERE 1 = 1";

                    // Add date range filters if provided
                    if (!string.IsNullOrEmpty(startDateFormatted))
                    {
                        query += " AND CONVERT(DATE, TransactionDate) >= @StartDate";
                    }
                    if (!string.IsNullOrEmpty(endDateFormatted))
                    {
                        query += " AND CONVERT(DATE, TransactionDate) <= @EndDate";
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (!string.IsNullOrEmpty(startDateFormatted))
                        {
                            command.Parameters.AddWithValue("@StartDate", startDateFormatted);
                        }
                        if (!string.IsNullOrEmpty(endDateFormatted))
                        {
                            command.Parameters.AddWithValue("@EndDate", endDateFormatted);
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                salesData.Add(new SalesTransaction
                                {
                                    TimeOfOrder = reader["Time of Sale"].ToString(),
                                    TransactionID = reader["Transaction ID"].ToString(),
                                    ProductName = reader["Product Name"].ToString(),
                                    QuantitySold = Convert.ToInt32(reader["Quantity Sold"]),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    TotalSales = Convert.ToDecimal(reader["Total Sales"]),
                                    TransactionTotal = Convert.ToDecimal(reader["Transaction Total"]),
                                    PaidAmount = Convert.ToDecimal(reader["Paid Amount"]),
                                    Change = Convert.ToDecimal(reader["Change Amount"]),
                                    HandledBy = reader["Handled By"].ToString(),
                                    Profit = Convert.ToDecimal(reader["Profit"]),
                                    Category = reader["Category"].ToString(),
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while retrieving data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return salesData;
        }

        // Event handler for the "Search" button click
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;

            // Check if both start and end dates are selected
            if (startDate.HasValue && endDate.HasValue)
            {
                string startDateFormatted = startDate.Value.ToString("yyyy-MM-dd");
                string endDateFormatted = endDate.Value.ToString("yyyy-MM-dd");

                // Pass the formatted dates to the method to load the sales data
                LoadSalesData(startDateFormatted, endDateFormatted);
            }
            else
            {
                MessageBox.Show("Please select both start and end dates.", "Invalid Date Range", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Event handler for navigating to the dashboard
        private void goToDashboard(object sender, RoutedEventArgs e)
        {
            MyDashboard dashboardWindow = new MyDashboard();
            dashboardWindow.Show();
            this.Close();
        }
    }

    // Class to represent the sales transaction
    public class SalesTransaction
    {
        public string TimeOfOrder { get; set; }
        public string TransactionID { get; set; }
        public string ProductName { get; set; }
        public int QuantitySold { get; set; }
        public decimal Price { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TransactionTotal { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal Change { get; set; }
        public string HandledBy { get; set; }
        public decimal Profit { get; set; }
        public string Category { get; set; }
    }
}
