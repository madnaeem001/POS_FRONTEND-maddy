using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Data.SqlClient;
using POSSystem;
using System;
using System.Collections.Generic;
using System.Windows;

namespace POS_FRONTEND
{
    public partial class MyDashboard : Window
    {
        public MyDashboard()
        {
            InitializeComponent();
            DataContext = this;
            LoadSalesData();
            LoadTotalSales();  // Load Total Sales
            LoadTotalStockValue(); // Load Total Stock Value
            LoadProfit(); // Load Profit
            LoadTotalStockQuantity();
        }

        public ChartValues<decimal> SalesData { get; set; } = new ChartValues<decimal>();
        public List<string> SalesDates { get; set; } = new List<string>();

        private void LoadSalesData()
        {
            using (var connection = DBConnection.GetConnection())
            {
                try
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            CONVERT(DATE, SaleDate) AS SaleDay, 
                            SUM(TotalSales) AS DailySales 
                        FROM 
                            Sales 
                        WHERE 
                            SaleDate >= DATEADD(DAY, -8, GETDATE())
                        GROUP BY 
                            CONVERT(DATE, SaleDate)
                        ORDER BY 
                            SaleDay;";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        DateTime saleDate = reader.GetDateTime(0);
                        SalesDates.Add(saleDate.ToString("yyyy-MM-dd"));
                        SalesData.Add(reader.GetDecimal(1));
                    }

                    reader.Close();
                    SalesChart.Update(true, true); // Update the chart
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while fetching sales data: {ex.Message}");
                }
            }
        }

        private void LoadTotalSales()
        {
            try
            {
                using (var connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT SUM(TotalSales) AS TotalSalesAmount FROM Sales";

                    SqlCommand command = new SqlCommand(query, connection);
                    object result = command.ExecuteScalar();

                    TotalSalesTextBlock.Text = result != DBNull.Value ? Convert.ToDecimal(result).ToString("C") : "$0.00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching total sales: {ex.Message}");
            }
        }

        private void LoadTotalStockValue()
        {
            try
            {
                using (var connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT SUM(StockQuantity * CostPrice) AS TotalStockValue FROM Products";

                    SqlCommand command = new SqlCommand(query, connection);
                    object result = command.ExecuteScalar();

                    TotalStockValueTextBlock.Text = result != DBNull.Value ? Convert.ToDecimal(result).ToString("C") : "$0.00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching total stock value: {ex.Message}");
            }
        }

        private void LoadProfit()
        {
            try
            {
                using (var connection = DBConnection.GetConnection())
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                           SUM(td.Subtotal) - SUM(pd.CostPrice * td.Quantity) AS Profit
                        FROM 
                           TransactionDetails td
                        JOIN 
                           Products pd ON td.ProductID = pd.ProductID;";

                    SqlCommand command = new SqlCommand(query, connection);
                    object result = command.ExecuteScalar();

                    ProfitTextBlock.Text = result != DBNull.Value
                        ? Convert.ToDecimal(result).ToString("C")
                        : "0.00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while calculating profit: {ex.Message}");
            }
        }

        private void LoadTotalStockQuantity()
        {
            try
            {
                using (var connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT SUM(StockQuantity) AS TotalStockQuantity FROM Products";

                    SqlCommand command = new SqlCommand(query, connection);
                    object result = command.ExecuteScalar();

                    TotalStockQuantityTextBlock.Text = result != DBNull.Value
                        ? Convert.ToInt32(result).ToString()
                        : "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while fetching total stock quantity: {ex.Message}");
            }
        }

        // Logout confirmation and handling
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to logout?",
                "Logout Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();

                // Open login page again
                LoginPage loginPage = new LoginPage();
                if (this.WindowState == WindowState.Maximized)
                {
                    loginPage.WindowState = WindowState.Maximized;
                }
                else
                {
                    loginPage.Width = this.Width;
                    loginPage.Height = this.Height;
                    loginPage.Left = this.Left;
                    loginPage.Top = this.Top;
                }

                loginPage.Show();
            }
        }

        // Navigation to other windows
        private void SalesButton_Click(object sender, RoutedEventArgs e)
        {
            Sales salesWindow = new Sales();
            SetWindowProperties(salesWindow);
            salesWindow.Show();
            this.Close();
        }

        private void Goto_Items(object sender, RoutedEventArgs e)
        {
            ItemsPage ipage = new ItemsPage();
            SetWindowProperties(ipage);
            ipage.Show();
            this.Close();
        }

        private void Goto_Reports(object sender, RoutedEventArgs e)
        {
            reports myreports = new reports();
            SetWindowProperties(myreports);
            myreports.Show();
            this.Close();
        }

        private void Goto_Settings(object sender, RoutedEventArgs e)
        {
            settings spage = new settings();
            SetWindowProperties(spage);
            spage.Show();
            this.Close();
        }

        private void Goto_Cat(object sender, RoutedEventArgs e)
        {
            Categories catpage = new Categories();
            SetWindowProperties(catpage);
            catpage.Show();
            this.Close();
        }

        // Method to set window properties (size, position, maximized state)
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
    }
}
