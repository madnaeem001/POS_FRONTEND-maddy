using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts;
using POSSystem;

namespace POS_FRONTEND
{
    /// <summary>
    /// Interaction logic for Categories.xaml
    /// </summary>
    public partial class Categories : Window
    {
        public Categories()
        {
            InitializeComponent();
        }

        private void goToDashboard(object sender, RoutedEventArgs e)
        {
            // Open the Dashboard Window
            MyDashboard dashboardWindow = new MyDashboard();
            if (this.WindowState == WindowState.Maximized)
            {
                // Set the dashboard window to maximized
                dashboardWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                // Otherwise, set the size and position of the dashboard to match the login window
                dashboardWindow.Width = this.Width;
                dashboardWindow.Height = this.Height;
                dashboardWindow.Left = this.Left;
                dashboardWindow.Top = this.Top;
            }
            dashboardWindow.Show();

            // Close the current Sales window
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

                if (this.WindowState == WindowState.Maximized)
                {
                    // Set the login window to maximized
                    loginPage.WindowState = WindowState.Maximized;
                }
                else
                {
                    // Otherwise, set the size and position of the login window to match the current window
                    loginPage.Width = this.Width;
                    loginPage.Height = this.Height;
                    loginPage.Left = this.Left;
                    loginPage.Top = this.Top;
                }

                // Show the login window
                loginPage.Show();
            }
            // If the user clicks No, do nothing (Logout canceled)
        }

        public ChartValues<int> SalesData { get; set; }

        private void SalesButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the Sales Window
            Sales salesWindow = new Sales();
            if (this.WindowState == WindowState.Maximized)
            {
                // Set the dashboard window to maximized
                salesWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                // Otherwise, set the size and position of the dashboard to match the login window
                salesWindow.Width = this.Width;
                salesWindow.Height = this.Height;
                salesWindow.Left = this.Left;
                salesWindow.Top = this.Top;
            }
            salesWindow.Show();

            // Close the current window or hide it
            this.Hide();
        }

        private void Goto_Items(object sender, RoutedEventArgs e)
        {
            // Create a new instance of ItemsPage
            ItemsPage ipage = new ItemsPage();


            if (this.WindowState == WindowState.Maximized)
            {
                // Set the dashboard window to maximized
                ipage.WindowState = WindowState.Maximized;
            }
            else
            {
                // Otherwise, set the size and position of the dashboard to match the login window
                ipage.Width = this.Width;
                ipage.Height = this.Height;
                ipage.Left = this.Left;
                ipage.Top = this.Top;
            }

            // Show the ItemsPage window
            ipage.Show();


            // Optional: Close the Dashboard window if needed
            this.Close();
        }
        private void Goto_Reports(object sender, RoutedEventArgs e)
        {
            // Open the Dashboard Window
            reports myreports = new reports();
            if (this.WindowState == WindowState.Maximized)
            {
                // Set the dashboard window to maximized
                myreports.WindowState = WindowState.Maximized;
            }
            else
            {
                // Otherwise, set the size and position of the dashboard to match the login window
                myreports.Width = this.Width;
                myreports.Height = this.Height;
                myreports.Left = this.Left;
                myreports.Top = this.Top;
            }
            myreports.Show();

            // Close the current Sales window
            this.Close();
        }
        private void Goto_Settings(object sender, RoutedEventArgs e)
        {
            // Create a new instance of ItemsPage
            settings spage = new settings();


            if (this.WindowState == WindowState.Maximized)
            {
                // Set the dashboard window to maximized
                spage.WindowState = WindowState.Maximized;
            }
            else
            {
                // Otherwise, set the size and position of the dashboard to match the login window
                spage.Width = this.Width;
                spage.Height = this.Height;
                spage.Left = this.Left;
                spage.Top = this.Top;
            }

            // Show the ItemsPage window
            spage.Show();


            // Optional: Close the Dashboard window if needed
            this.Close();
        }




        private void openEditCategories(object sender, RoutedEventArgs e)
        {
            // Create a new instance of ItemsPage
            editCategory edcpage = new editCategory();

            // Show the ItemsPage window
            edcpage.ShowDialog();


            // Optional: Close the Dashboard window if needed
            //this.Close();
        }

        private void SalesDataGrid_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SalesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Logic to remove a selected category
            // Add actual implementation here
            MessageBox.Show("Remove Category clicked!");
        }

        // Edit Category button click handler
        private void openEditItems(object sender, RoutedEventArgs e)
        {
            // Logic to edit the selected category
            // Add actual implementation here
            MessageBox.Show("Edit Category clicked!");
        }

        // Add Category button click handler
        private void openAddItems(object sender, RoutedEventArgs e)
        {
            // Logic to add a new category
            // Add actual implementation here
            MessageBox.Show("Add Category clicked!");
        }
    }
}
