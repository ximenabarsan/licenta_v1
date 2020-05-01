using MedicalClinic.ViewModels;
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

namespace MedicalClinic.Views
{
    /// <summary>
    /// Interaction logic for EditUsersWindowAdmin.xaml
    /// </summary>
    public partial class EditUsersWindowAdmin : Window,IView
    {
        public EditUsersWindowAdmin(EditUsersViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }
        public IViewModel ViewModel
        {

            get { return DataContext as IViewModel; }
            set { DataContext = value; }
        }

        
    }
}
