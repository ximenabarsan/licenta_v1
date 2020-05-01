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

    public class RegistrationViewModel : INotifyPropertyChanged, IViewModel
    {


        private string _name;
        private string _surname;
        private string _cnp;
        private string _email;
        private string _password;
        private string _telephone;
        private ObservableCollection<Specialization> _specializations = new ObservableCollection<Specialization>();
        private Specialization _selectedSpecialization;
        private DelegateCommand _registerCommand;
        private DelegateCommand _showViewCommand;
        private DelegateCommand _editUserData;
        IView toClose;


        private bool isEditWindow;
       



        #region Properties Region
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyPropertyChanged("Name"); }
        }
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; NotifyPropertyChanged("Surname"); }
        }
        public string CNP
        {
            get { return _cnp; }
            set { _cnp = value; NotifyPropertyChanged("CNP"); }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; NotifyPropertyChanged("Email"); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; NotifyPropertyChanged("Password"); }
        }
        public string Telephone
        {
            get { return _telephone; }
            set { _telephone = value; NotifyPropertyChanged("Telephone"); }
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
           _registerCommand = new DelegateCommand(Register,CanRegister);
            _showViewCommand = new DelegateCommand(ShowView, null);
            _specializations = FillSpecialization();
            _editUserData = new DelegateCommand(EditUser, CanEdit);
            isEditWindow = false ;
           
        }

      

        public void Register(object parameter) {
            ComboBox comboRole = parameter as ComboBox;
            int role =Int32.Parse(((ComboBoxItem)comboRole.SelectedItem).Tag.ToString());
            var context = new MedicalDBEntities();
            var users = context.Users;
            User user = new User();
            User user1 = new User();
            user.email = Email; 
            user.nameUser = Name;
            user.surnameUser = Surname;
            user.password = Password;
            user.telephone = Telephone;
            user.roleUser = role;
            user.CNP = CNP;


            if (user != null)
            {
                users.Add(user);
                if (user.roleUser == 2)
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
            else MessageBox.Show("Trebuie sa completati campurile !");
                    
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
            if (customPrincipal.Identity.IsAuthenticated == false)
            {
                MessageBox.Show("Trebuie sa fiti autentificati pentru a va edita datele");
            }
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
            /*if (currentUser.roleUser == 2) {
                var currentDoctor = context.Doctors
                    .Where(s => s.idUser == currentUser.idUser)
                    .FirstOrDefault<Doctor>();
                 currentDoctor.Specialization=SelectedSpecialization;

            }*/
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
