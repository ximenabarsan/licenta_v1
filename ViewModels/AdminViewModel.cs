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
    public class AdminViewModel : IViewModel, INotifyPropertyChanged
    {
        private DelegateCommand _logoutCommand;
        private DelegateCommand _showRegisterViewCommand;
        private DelegateCommand _showRegisterForEdit;
        private DelegateCommand _showServicesViewCommand;
        private DelegateCommand _showEditUsersViewCommand;


        public AdminViewModel()
        {
            _logoutCommand = new DelegateCommand(Logout, CanLogout);
            _showRegisterViewCommand = new DelegateCommand(ShowRegisterView, null);
            _showRegisterForEdit = new DelegateCommand(ShowRegisterViewForEdit, null);
            _showServicesViewCommand = new DelegateCommand(ShowServicesView, null);
            _showEditUsersViewCommand = new DelegateCommand(ShowEditUsersView, null);
        }



        #region Properties
        public IView viewToClose;

        public string NameAdmin
        {
            get { return GetFullNameAdmin(); }
        }

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

        public DelegateCommand ShowEditUsersViewComand { get { return _showEditUsersViewCommand; } }


        private void ShowEditUsersView(object obj) {
            
            
        
           EditUsersViewModel viewModel = new EditUsersViewModel();
            EditUsersWindowAdmin view = new EditUsersWindowAdmin(viewModel);
            viewModel.settoClose(view);
            view.Show();

            viewToClose.Close();

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

        public DelegateCommand ShowRegisterViewCommand { get { return _showRegisterViewCommand; } }
        private void ShowRegisterView(object parameter)
        {
            IView view;
            RegistrationViewModel rmodel = new RegistrationViewModel();
            view = new RegisterWindow(rmodel);

            rmodel.settoClose(view);

            view.Show();

            viewToClose.Close();

        }

        public DelegateCommand ShowRegisterForEdit { get { return _showRegisterForEdit; } }
        private void ShowRegisterViewForEdit(object obj)
        {
            IView view;
            RegistrationViewModel rmodel = new RegistrationViewModel();
            rmodel.prepareToEdit();
            view = new RegisterWindow(rmodel);

            rmodel.settoClose(view);
           

            view.Show();

            viewToClose.Close();

            

            
        }
        public DelegateCommand ShowServicesViewCommand{ get { return _showServicesViewCommand; } }

        private void ShowServicesView(object obj)
        {
            IView view;
            ServicesViewModel smodel = new ServicesViewModel();
           
            view = new ServicesWindow(smodel);
            smodel.settoClose(view);
            view.Show();
            viewToClose.Close();


        }


        #endregion


        #region Methods
        public void settoClose(IView close)
        {

            this.viewToClose = close;

        }
        public string GetFullNameAdmin()
        {

            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            string currentEmail = customPrincipal.Identity.Email;
            var context = new MedicalDBEntities();
            User user = context.Users
                       .Where(s => s.email == currentEmail)
                       .FirstOrDefault<User>();

            return user.nameUser + " " + user.surnameUser;
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