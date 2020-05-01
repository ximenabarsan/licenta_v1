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
    public class PatientViewModel:IViewModel,INotifyPropertyChanged
    {
        private DelegateCommand _logoutCommand;
        private DelegateCommand _showRegisterForEdit;
        private DelegateCommand _showServicesViewCommand;
        private DelegateCommand _showAppoimentsViewCommand;
        private DelegateCommand _showMyAppoimentsCommand;
        private DelegateCommand _showMedicalHistoryCommand;


        public PatientViewModel()
        {
            _logoutCommand = new DelegateCommand(Logout, CanLogout);
            _showRegisterForEdit = new DelegateCommand(ShowRegisterViewForEdit, null);
            _showServicesViewCommand = new DelegateCommand(ShowServicesView, null);
            _showAppoimentsViewCommand= new DelegateCommand(ShowAppoimentsView, null);
            _showMyAppoimentsCommand = new DelegateCommand(ShowMyAppoiments, null);
            _showMedicalHistoryCommand = new DelegateCommand(ShowMedicalHistory, null);
        }

     
        #region Properties


        public IView viewToClose;

        public string NamePatient
        {
            get { return GetFullNamePatient(); }
        }


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



       

        public DelegateCommand ShowServicesViewCommand { get { return _showServicesViewCommand; } }

        private void ShowServicesView(object obj)
        {
            IView view;
            ServicesViewModel smodel = new ServicesViewModel();

            view = new ServicesWindow(smodel);
            smodel.settoClose(view);
            view.Show();
            viewToClose.Close();


        }

        public DelegateCommand ShowAppoimentsViewCommand { get { return _showAppoimentsViewCommand; } }

        private void ShowAppoimentsView(object obj)
        {
            IView view;
            AppoimentsViewModel apmodel = new AppoimentsViewModel();

            view = new AppoimentsWindow(apmodel);
            apmodel.settoClose(view);
            view.Show();
            viewToClose.Close();
        }


        public DelegateCommand ShowMyAppoimentsCommand { get { return _showMyAppoimentsCommand; } }

        private void ShowMyAppoiments(object obj)
        {
            IView view;
            PatientsAppoimentsViewModel apmodel = new PatientsAppoimentsViewModel();

            view = new PatientAppoimentsWindow(apmodel);
            apmodel.settoClose(view);
            view.Show();
            viewToClose.Close();

        }


        public DelegateCommand ShowMedicalHistoryCommand { get { return _showMedicalHistoryCommand; } }

        private void ShowMedicalHistory(object obj)
        {
            IView view;
            MedicalHistoryViewModel vmodel = new MedicalHistoryViewModel();

            view = new MedicalHistoryWindow(vmodel);
            vmodel.settoClose(view);
            view.Show();
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

       

        public string GetFullNamePatient() {

            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            string currentEmail = customPrincipal.Identity.Email;
            var context = new MedicalDBEntities();
            User user = context.Users
                       .Where(s => s.email == currentEmail)
                       .FirstOrDefault<User>();

            return user.nameUser +" "+ user.surnameUser;
        }

        #endregion
    }
}

    