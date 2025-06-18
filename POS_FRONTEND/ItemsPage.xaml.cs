using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using POS_FRONTEND;

namespace POSSystem
{
    public partial class ItemsPage : Window
    {
        // Define the Item class to represent the data
        public class Item
        {
            public string CustomerName { get; set; }
            public string ItemName { get; set; }
            public string StockCode { get; set; }
            public int ItemsSold { get; set; }
            public string Category { get; set; }
            public string Supplier { get; set; }
            public int StockLevel { get; set; }
            public decimal Price { get; set; }
            public decimal Cost { get; set; }
            public decimal Profit { get; set; }
            public decimal Margin { get; set; }
            public decimal Markup { get; set; }
        }

        // List to store items
        private List<Item> itemsList = new List<Item>();

        public ItemsPage()
        {
            InitializeComponent();
            // Add some sample items to the list
            AddSampleItems();
        }

        // Function to add items to the list
        private void AddItem(string customerName, string itemName, string stockCode, int itemsSold,
                              string category, string supplier, int stockLevel, decimal price,
                              decimal cost, decimal profit, decimal margin, decimal markup)
        {
            // Create a new Item and add to the list
            Item newItem = new Item
            {
                CustomerName = customerName,
                ItemName = itemName,
                StockCode = stockCode,
                ItemsSold = itemsSold,
                Category = category,
                Supplier = supplier,
                StockLevel = stockLevel,
                Price = price,
                Cost = cost,
                Profit = profit,
                Margin = margin,
                Markup = markup
            };

            // Add item to the list
            itemsList.Add(newItem);

            // Refresh the DataGrid
            SalesDataGrid.ItemsSource = null;
            SalesDataGrid.ItemsSource = itemsList;
        }

        // Function to add sample items to the list
        private void AddSampleItems()
        {
            AddItem("John Doe", "Product A", "S001", 10, "Electronics", "Supplier A", 100, 150.00m, 100.00m, 50.00m, 33.33m, 50.00m);
            AddItem("Jane Smith", "Product B", "S002", 20, "Clothing", "Supplier B", 200, 50.00m, 30.00m, 20.00m, 40.00m, 60.00m);
            AddItem("Bob Brown", "Product C", "S003", 15, "Furniture", "Supplier C", 150, 200.00m, 120.00m, 80.00m, 40.00m, 66.67m);
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
                this.Close();
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

        private void SalesButton_Click(object sender, RoutedEventArgs e)
        {
            Sales salesWindow = new Sales();
            if (this.WindowState == WindowState.Maximized)
            {
                salesWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                salesWindow.Width = this.Width;
                salesWindow.Height = this.Height;
                salesWindow.Left = this.Left;
                salesWindow.Top = this.Top;
            }
            salesWindow.Show();
            this.Hide();
        }



        private void goToDashboard(object sender, RoutedEventArgs e)
        {
            MyDashboard dashboardWindow = new MyDashboard();
            if (this.WindowState == WindowState.Maximized)
            {
                dashboardWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                dashboardWindow.Width = this.Width;
                dashboardWindow.Height = this.Height;
                dashboardWindow.Left = this.Left;
                dashboardWindow.Top = this.Top;
            }
            dashboardWindow.Show();
            this.Close();
        }

        private void Goto_Reports(object sender, RoutedEventArgs e)
        {
            reports myreports = new reports();
            if (this.WindowState == WindowState.Maximized)
            {
                myreports.WindowState = WindowState.Maximized;
            }
            else
            {
                myreports.Width = this.Width;
                myreports.Height = this.Height;
                myreports.Left = this.Left;
                myreports.Top = this.Top;
            }
            myreports.Show();
            this.Close();
        }

        private void openAddItems(object sender, RoutedEventArgs e)
        {
            appItem apage = new appItem();
            apage.ShowDialog();
        }

        private void openEditItems(object sender, RoutedEventArgs e)
        {
            editItems edipage = new editItems();
            edipage.ShowDialog();
        }

        private void Goto_Settings(object sender, RoutedEventArgs e)
        {
            settings spage = new settings();

            if (this.WindowState == WindowState.Maximized)
            {
                spage.WindowState = WindowState.Maximized;
            }
            else
            {
                spage.Width = this.Width;
                spage.Height = this.Height;
                spage.Left = this.Left;
                spage.Top = this.Top;
            }
            spage.Show();
            this.Close();
        }

        // SearchBox Functionality

        // 1. GotFocus Event Handler
        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox searchBox = sender as TextBox;
            if (searchBox != null && searchBox.Text == "Search...")
            {
                searchBox.Text = ""; // Clear placeholder text
            }
        }

        // 2. LostFocus Event Handler
        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox searchBox = sender as TextBox;
            if (searchBox != null && string.IsNullOrWhiteSpace(searchBox.Text))
            {
                searchBox.Text = "Search..."; // Restore placeholder text if empty
            }
        }

        private void Goto_Cat(object sender, RoutedEventArgs e)
        {
            Categories catpage = new Categories();

            if (this.WindowState == WindowState.Maximized)
            {
                catpage.WindowState = WindowState.Maximized;
            }
            else
            {
                catpage.Width = this.Width;
                catpage.Height = this.Height;
                catpage.Left = this.Left;
                catpage.Top = this.Top;
            }

            catpage.Show();
            this.Close();
        }

        // 3. TextChanged Event Handler
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchBox = sender as TextBox;
            if (searchBox != null)
            {
                string searchText = searchBox.Text;

                // TODO: Implement your search filtering logic here
            }
        }

        private void SalesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Optional: Add your logic here
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // Optional: Add your logic here
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            // Optional: Add your logic here
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Your button click logic
            MessageBox.Show("Button was clicked!");
        }
    }
}
