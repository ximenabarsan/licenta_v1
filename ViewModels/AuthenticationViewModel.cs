using MedicalClinic.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MedicalClinic.ViewModels
{
    public interface IViewModel { }

    public class AuthenticationViewModel : IViewModel, INotifyPropertyChanged,INotifyDataErrorInfo
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly DelegateCommand _loginCommand;
        private readonly DelegateCommand _showRegisterViewCommand;
        private string _email;
        private string _status;
        private readonly Dictionary<string, ICollection<string>>
        _validationErrors = new Dictionary<string, ICollection<string>>();
        private readonly IValidationService validationservice=new IValidationService();
        IView viewtoClose;


        public AuthenticationViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _loginCommand = new DelegateCommand(Login, CanLogin);
            _showRegisterViewCommand = new DelegateCommand(ShowRegisterView, null);
            
        }



        #region Properties
        public string Email
        {
            get { return _email; }
            set { _email = value; NotifyPropertyChanged("Email");
                ValidateEmailAuthentication(Email);
                }
        }

        public string AuthenticatedUser
        {
            get
            {
                if (IsAuthenticated)
                {
                    return string.Format("Salut, {0}. {1} !",
                        Thread.CurrentPrincipal.Identity.Name,
                        Thread.CurrentPrincipal.IsInRole("Administrator") ? " Sunteti logat ca si administrator"
                            : Thread.CurrentPrincipal.IsInRole("Doctor") ? "Sunteti logat ca si doctor" : "Sunteti logat ca si pacient");

                }

                return "Nu sunteti autentificat!";
            }
          
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; NotifyPropertyChanged("Status"); }
        }

        public bool IsAuthenticated
        {
            get { return Thread.CurrentPrincipal.Identity.IsAuthenticated; }
        }
        #endregion

        #region Commands
        public DelegateCommand LoginCommand { get { return _loginCommand; } }

        public DelegateCommand ShowRegisterViewCommand { get { return _showRegisterViewCommand; } }
        #endregion

        private void Login(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;
            try
            {
               
                User user = _authenticationService.AuthenticateUser(Email, clearTextPassword);
                CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                if (customPrincipal == null)
                    MessageBox.Show("Custom principal e null");
                string[] rol= { };
                if (user.roleUser == 1)rol = new string[] { "Administrator" };
                if (user.roleUser == 2) rol = new string[] { "Doctor" };
                if (user.roleUser == 3) rol = new string[] { "Pacient" };

                customPrincipal.Identity = new CustomIdentity(user.nameUser, user.idUser, rol, user.email, user.telephone, user.surnameUser, user.CNP, user.password);

                //Fac update la UI
                NotifyPropertyChanged("AuthenticatedUser");
                NotifyPropertyChanged("IsAuthenticated");
                _loginCommand.RaiseCanExecuteChanged();
                
                // Resetez valorile
                Email = string.Empty; 
                passwordBox.Password = string.Empty; 
                Status = string.Empty;

                //Afisez main view-ul potrivit fiecarui tip de utilizator
                if(IsAuthenticated)
                switch (user.roleUser) {

                    case 1:
                        ShowAdminView(parameter);break;
                       
                    case 2:
                        ShowDoctorView(parameter);break;
                    case 3:
                            ShowPatientView(parameter);break;   
                }
            }

            catch (UnauthorizedAccessException)
            {
                Status = "Va rugam sa introduceti date valide";
                MessageBox.Show(Status);
            }
            catch (Exception ex)
            {
                Status = string.Format("ERROR: {0}", ex.Message);
            }
        }

        private bool CanLogin(object parameter)
        {
            return !IsAuthenticated;
        }


        private void ShowAdminView(object parameter)
        {

            IView view;
            AdminViewModel viewmodel = new AdminViewModel();
            view = new AdminWindow(viewmodel);

            viewmodel.viewToClose = view;
            view.Show();
            viewtoClose.Close();

        }
        private void ShowRegisterView(object parameter)
        {   
            IView view;
            RegistrationViewModel rmodel = new RegistrationViewModel();
            view = new RegisterWindow(rmodel);

            rmodel.settoClose(view);
            
            view.Show();
            
            viewtoClose.Close();

        }

        private void ShowDoctorView(object parameter)
        {
            IView view;
            DoctorViewModel viewmodel = new DoctorViewModel();
            view = new DoctorWindow(viewmodel);

            viewmodel.settoClose(view);

            view.Show();

            viewtoClose.Close();
        }
        private void ShowPatientView(object parameter)
        {
            IView view=null;
            PatientViewModel viewmodel = new PatientViewModel();
            view = new PatientWindow(viewmodel);

            viewmodel.settoClose(view);

            view.Show();

            viewtoClose.Close();
        }



        #region Methods
        public void settoClose(IView close)
        {

            this.viewtoClose = close;

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

        #region INotifuDataErrorsMembers
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)
                || !_validationErrors.ContainsKey(propertyName))
                return null;

            return _validationErrors[propertyName];
        }

        public bool HasErrors
        {
            get { return _validationErrors.Count > 0; }
        }

        #endregion

        #region ValidationRegion
        private async void ValidateEmailAuthentication(string email)
        {
            const string propertyKey = "Email";
            ICollection<string> validationErrors = null;
            bool isValid = await Task<bool>.Run(() =>
            {
                return validationservice.ValidateEmailAuthentication(email, out validationErrors);
            })
            .ConfigureAwait(false);

            if (!isValid)
            {
                _validationErrors[propertyKey] = validationErrors;
                RaiseErrorsChanged(propertyKey);
            }
            else if (_validationErrors.ContainsKey(propertyKey))
            {
                _validationErrors.Remove(propertyKey);
                RaiseErrorsChanged(propertyKey);
            }
        }
        #endregion
    }
}