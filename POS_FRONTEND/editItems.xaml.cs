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

namespace POS_FRONTEND
{
    /// <summary>
    /// Interaction logic for editItems.xaml
    /// </summary>
    public partial class editItems : Window
    {
        public editItems()
        {
            InitializeComponent();
        }

        // Event handler for TextChanged event of txtP_ID
        private void txtP_ID_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Add the logic to handle the text change event
            string input = (sender as TextBox).Text;
            // Example: Validate or perform actions when the TextBox content changes
            // Example: Perform validation or update other fields
        }

        // Event handler for SelectionChanged event of cmbSupplier
        private void cmbSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Add the logic to handle the selection change event of the ComboBox
            var selectedSupplier = (sender as ComboBox).SelectedItem;
            // Example: Perform actions based on the selected supplier
            // Example: Update some fields or perform validation
        }
    }
}
