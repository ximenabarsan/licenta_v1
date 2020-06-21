using MedicalClinic.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MedicalClinic.ViewModels
{

    public class RegistrationViewModel : INotifyPropertyChanged, IViewModel, INotifyDataErrorInfo
    {


        private string _name;
        private string _surname;
        private string _cnp;
        private string _email;
        private string _password;
        private string _telephone;
        private ObservableCollection<Specialization> _specializations;
        private Specialization _selectedSpecialization;
        private DelegateCommand _registerCommand;
        private DelegateCommand _showViewCommand;
        private DelegateCommand _editUserData;
        IView toClose;
        private readonly Dictionary<string, ICollection<string>>
       _validationErrors = new Dictionary<string, ICollection<string>>();
        private readonly IValidationService validationservice;


        private bool isEditWindow;
       



        #region Properties Region
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged("Name");
                ValidateName(_name);
            }
        }
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; NotifyPropertyChanged("Surname");
                ValidateSurname(_surname);
            }
        }
        public string CNP
        {
            get { return _cnp; }
            set { _cnp = value; NotifyPropertyChanged("CNP");
                ValidateCnp(_cnp);
            }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; NotifyPropertyChanged("Email");
                ValidateEmail(_email); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; NotifyPropertyChanged("Password");
                ValidatePassword(_password);
            }
        }
        public string Telephone
        {
            get { return _telephone; }
            set { _telephone = value; NotifyPropertyChanged("Telephone");
                ValidateTelephone(_telephone);
            }
        }

      

        public bool AdminRights
        {
            get
            {
                return Thread.CurrentPrincipal.IsInRole("Administrator") ? true : false;

            }
            
        }

        public bool DoctorRights
        {
            get {  return Thread.CurrentPrincipal.IsInRole("Doctor") ? true : false; }
        
        }

        public bool PatientRights { 
            get
            {
                return Thread.CurrentPrincipal.IsInRole("Pacient") ? true : false;
            }
        
        }


        public bool IsEditable {

            get {
                return isEditWindow ? false : true;
            }
           
        }

        public bool VisibleComboRoleAndSpecialization{
            get
            {
                return ((AdminRights && IsEditable)||(DoctorRights && IsEditable)) ? true : false;

            }
        }

        public bool NotIsEditable
        {

            get { return !IsEditable; }
        }

        public ObservableCollection<Specialization> SpecializationsCombo{
            get {
                return _specializations;
            }
            set
            {
                _specializations = value;
                NotifyPropertyChanged("SpecializationsCombo");
            }
        }

        public Specialization SelectedSpecialization {
            get {
                return _selectedSpecialization;
            }
            set {
                _selectedSpecialization = value;
                NotifyPropertyChanged("SelectedSpecialization");
            }
        
        
        }

        internal void prepareToEdit()
        {
            isEditWindow = true;
            FillUserDates();

        }

        public DelegateCommand RegisterCommand{get {return _registerCommand;} }
        public DelegateCommand ShowViewCommand { get { return _showViewCommand; } }

        public DelegateCommand EditUserData { get { return _editUserData; } }
        

        public void settoClose(IView close)
        {

            this.toClose = close;
           
        }


        #endregion
       public RegistrationViewModel()
        {
            _specializations =  new ObservableCollection<Specialization>();
            _registerCommand = new DelegateCommand(Register,CanRegister);
            _showViewCommand = new DelegateCommand(ShowView, null);
            _specializations = FillSpecialization();
            _editUserData = new DelegateCommand(EditUser, CanEdit);
            isEditWindow = false ;
            validationservice= new IValidationService();
        }

      

        public void Register(object parameter) {
            ComboBox comboRole = parameter as ComboBox;
            int role =Int32.Parse(((ComboBoxItem)comboRole.SelectedItem).Tag.ToString());
            var context = new MedicalDBEntities();
            var users = context.Users;
            User user = new User();
            User user1 = new User();
            if (Email != null && Name != null && Surname != null && CNP != null && Password != null && Telephone != null && _validationErrors.Values.Count==0)
            {
                if (this.CheckUser(Email))
                {
                    user.email = Email;
                    user.nameUser = Name;
                    user.surnameUser = Surname;
                    user.password = Password;
                    user.telephone = Telephone;
                    user.roleUser = role;
                    user.CNP = CNP;
                    users.Add(user);

                    if (user.roleUser == 2 && SelectedSpecialization != null)
                    {
                        var doctors = context.Doctors;
                        Doctor doctor = new Doctor();
                        doctor.idSpecialization = SelectedSpecialization.idSpecialization;
                        doctor.idUser = user.idUser;
                        doctors.Add(doctor);
                    }
                    context.SaveChanges();

                    MessageBox.Show("Contul dumneavoastra a fost inregistrat cu succes");
                    IView authenticationWindow = null;

                    if (AdminRights)
                    {
                        AdminViewModel adminViewModel = new AdminViewModel();
                        IView adminView = new AdminWindow(adminViewModel);
                        adminViewModel.settoClose(adminView);
                        adminView.Show();
                        toClose.Close();

                    }

                    else
                    {
                        AuthenticationViewModel viewModel = new AuthenticationViewModel(new AuthenticationService());
                        authenticationWindow = new AuthenticationWindow(viewModel);
                        viewModel.settoClose(authenticationWindow);
                        authenticationWindow.Show();
                        toClose.Close();
                    }
                }
                else { MessageBox.Show("Exista deja un cont cu acest email"); }
            }
            else MessageBox.Show("Va rugăm să introduceți toate datele");
                    
        }

        public bool CanRegister(object parameter) {

            return true;
        }

        public void ShowView(object parameter) 
        {
            if (AdminRights)
            {
                AdminViewModel adminViewModel = new AdminViewModel();
                IView adminView = new AdminWindow(adminViewModel);
                adminViewModel.settoClose(adminView);
                adminView.Show();
                toClose.Close();

            }

            else if(DoctorRights)
            {
                DoctorViewModel doctorViewModel = new DoctorViewModel();
                IView doctorView = new DoctorWindow(doctorViewModel);
                doctorViewModel.settoClose(doctorView);
                doctorView.Show();
                toClose.Close();
            }

            else if(PatientRights) {

                PatientViewModel patientViewModel = new PatientViewModel();
                IView patientView = new PatientWindow(patientViewModel);
                patientViewModel.settoClose(patientView);
                patientView.Show();
                toClose.Close();
            }

            else
            {

                IView authenticationWindow = null;
                AuthenticationViewModel viewModel = new AuthenticationViewModel(new AuthenticationService());
                authenticationWindow = new AuthenticationWindow(viewModel);
                viewModel.settoClose(authenticationWindow);
                authenticationWindow.Show();
                toClose.Close();
            }


        }
        private bool CanEdit(object obj)
        {   
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            return customPrincipal.Identity.IsAuthenticated;
           
        }

        private void EditUser(object obj)
        {
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            string currentEmail = customPrincipal.Identity.Email;
            var context = new MedicalDBEntities();
            var query = context.Users
                       .Where(s => s.email == currentEmail)
                       .FirstOrDefault<User>();
            var currentUser = query;
            currentUser.nameUser = Name;
            currentUser.surnameUser = Surname;
            currentUser.CNP = CNP;
            currentUser.email = Email;
            currentUser.password = Password;
            currentUser.telephone = Telephone;
            context.SaveChanges();

        }


        #region Methods

        public ObservableCollection<Specialization> FillSpecialization() {
            using (MedicalDBEntities context = new MedicalDBEntities())
            {
                var query = from speci in context.Specializations select speci;

                ObservableCollection<Specialization> spec = new ObservableCollection<Specialization>(query);


                return spec;
            }
        }

        public void FillUserDates() {
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            string currentEmail=customPrincipal.Identity.Email;
            var context = new MedicalDBEntities();
                var query = context.Users
                           .Where(s => s.email == currentEmail)
                           .FirstOrDefault<User>();
            var currentUser = query;
            Name = currentUser.nameUser;
            Surname = currentUser.surnameUser;
            CNP = currentUser.CNP;
            Email = currentUser.email;
            Password = currentUser.password;
            Telephone = currentUser.telephone;
            if (currentUser.roleUser == 2) {
                var currentDoctor = context.Doctors
                    .Where(s => s.idUser == currentUser.idUser)
                    .FirstOrDefault<Doctor>();
                SelectedSpecialization = currentDoctor.Specialization;
            }
            
            

        }

        public bool CheckUser(string email)
        {
            using (var context = new MedicalDBEntities())
            {
                var query = context.Users
                   .Where(s => s.email == email)
                   .FirstOrDefault<User>();
               
                if (query != null)
                {
                    return false;

                }
            }
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

        #region INotifyDataErrorInfo members
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


        private async void ValidateEmail(string username)
        {
            const string propertyKey = "Email";
            ICollection<string> validationErrors = null;
            /* Call service asynchronously */
            bool isValid = await Task<bool>.Run(() =>
            {
                return validationservice.ValidateEmailRegister(username, out validationErrors);
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
        private async void ValidatePassword(string password)
        {
            const string propertyKey = "Password";
            ICollection<string> validationErrors = null;
            bool isValid = await Task<bool>.Run(() =>
            {
                return validationservice.ValidatePasswordRegistration(password, out validationErrors);
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
        private async void ValidateName(string name)
        {
            const string propertyKey = "Name";
            ICollection<string> validationErrors = null;
            bool isValid = await Task<bool>.Run(() =>
            {
                return validationservice.ValidateNameRegister(name, out validationErrors);
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
        private async void ValidateSurname(string surname)
        {
            const string propertyKey = "Surname";
            ICollection<string> validationErrors = null;
           
            bool isValid = await Task<bool>.Run(() =>
            {
                return validationservice.ValidateNameRegister(surname, out validationErrors);
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
        private async void ValidateTelephone(string telephone)
        {
            const string propertyKey = "Telephone";
            ICollection<string> validationErrors = null;
          
            bool isValid = await Task<bool>.Run(() =>
            {
                return validationservice.ValidateTelephoneRegister(telephone, out validationErrors);
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
        private async void ValidateCnp(string cnp)
        {
            const string propertyKey = "CNP";
            ICollection<string> validationErrors = null;
           
            bool isValid = await Task<bool>.Run(() =>
            {
                return validationservice.ValidateCnpRegister(cnp, out validationErrors);
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
    }
}
