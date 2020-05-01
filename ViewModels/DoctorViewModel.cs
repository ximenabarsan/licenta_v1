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
        private DelegateCommand _showEditViewCommand;
        private DelegateCommand _showScheduleCommand;
        private DelegateCommand _showTreatmentCommand;
        private DelegateCommand _showAppoimentsCommand;
        private DelegateCommand _showMedicalHistoryCommand;

        public DoctorViewModel()
        {
            _logoutCommand = new DelegateCommand(Logout, CanLogout);
            _showEditViewCommand = new DelegateCommand(ShowEditView, null);
            _showScheduleCommand = new DelegateCommand(ShowSchedule, null);
            _showTreatmentCommand = new DelegateCommand(ShowTreatmentView, null);
            _showAppoimentsCommand = new DelegateCommand(ShowAppoiments, null);
            _showMedicalHistoryCommand = new DelegateCommand(ShowMedicalHistory, null);
        }
        #region Properties


        public IView viewToClose;

        public string NameDoctor
        {
            get { return GetFullNameDoctor(); }
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


        public DelegateCommand ShowEditViewCommand { get { return _showEditViewCommand; } }

       private void ShowEditView(object obj)
        {
            IView view;
            RegistrationViewModel rmodel = new RegistrationViewModel();
            rmodel.prepareToEdit();
            view = new RegisterWindow(rmodel);

            rmodel.settoClose(view);


            view.Show();

            viewToClose.Close();

        }


        public DelegateCommand ShowScheduleCommand { get { return _showScheduleCommand; } }

        private void ShowSchedule(object obj)
        {
            IView view;
            ScheduleViewModel vmodel = new ScheduleViewModel();

            view = new ScheduleWindow(vmodel);
            vmodel.settoClose(view);
            view.Show();
            viewToClose.Close();
        }

        public DelegateCommand ShowTreatmentCommand { get { return _showTreatmentCommand; } }


        private void ShowTreatmentView(object obj)
        {
            IView view;
            TreatmentsViewModel vmodel = new TreatmentsViewModel();

            view = new AddTreatmentsWindow(vmodel);
            vmodel.settoClose(view);
            view.Show();
            viewToClose.Close();

        }

        public DelegateCommand ShowAppoimentsCommand { get { return _showAppoimentsCommand; } }

        private void ShowAppoiments(object obj)
        {
            IView view;
            DoctorAppoimentsViewModel vmodel = new DoctorAppoimentsViewModel();

            view = new DoctorAppoimentsWindow(vmodel);
            vmodel.settoClose(view);
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


        #region Methods

        public string GetFullNameDoctor()
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
        #region Methods
        public void settoClose(IView close)
        {

            this.viewToClose = close;

        }
        #endregion
    }
}
