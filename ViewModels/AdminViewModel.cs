using MedicalClinic.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MedicalClinic.ViewModels
{
    public class AdminViewModel : IViewModel,INotifyPropertyChanged
    {
        private DelegateCommand _logoutCommand;
        
        public AdminViewModel()
        {
            _logoutCommand = new DelegateCommand(Logout, CanLogout);     
        }
        #region Properties
        public IView viewToClose;

        #endregion

        #region Commands

        public DelegateCommand LogoutCommand { get { return _logoutCommand; } }

        public void Logout(object parameter) {
            
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal != null)
            {
                customPrincipal.Identity = new AnonymousIdentity();
            }
            ShowAuthenticationView();

        }

        private void ShowAuthenticationView()
        {
            IView authenticationWindow = null;
            AuthenticationViewModel viewModel = new AuthenticationViewModel(new AuthenticationService());
            authenticationWindow = new AuthenticationWindow(viewModel);
            viewModel.settoClose(authenticationWindow);
            authenticationWindow.Show();

           viewToClose.Close();

        }

        public bool CanLogout(object parameter)
        {
            return true;
        }

        #endregion

       
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

}