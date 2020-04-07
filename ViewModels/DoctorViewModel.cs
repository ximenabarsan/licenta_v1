using MedicalClinic.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalClinic.ViewModels
{
    public class DoctorViewModel : IViewModel,INotifyPropertyChanged
    {
        


        private DelegateCommand _logoutCommand;


        public DoctorViewModel()
        {
            _logoutCommand = new DelegateCommand(Logout, CanLogout);

        }
        #region Properties


        public IView viewToClose;

        #endregion

        #region Commands

        public DelegateCommand LogoutCommand { get { return _logoutCommand; } }

        public void Logout(object parameter)
        {

            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal != null)
            {
                customPrincipal.Identity = new AnonymousIdentity();
            }
            ShowAuthenticationView();

        }
        public bool CanLogout(object parameter)
        {
            return true;
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

       

        #endregion


        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        #region Methods
        public void settoClose(IView close)
        {

            this.viewToClose = close;

        }
        #endregion
    }
}
