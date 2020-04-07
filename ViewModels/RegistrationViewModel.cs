using MedicalClinic.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
        private  DelegateCommand _registerCommand;
        IView toClose;

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

        public bool AdminRights {
            get { 
               return Thread.CurrentPrincipal.IsInRole("Administrator")? true: false ;
            }
        }
        public DelegateCommand RegisterCommand{get {return _registerCommand;} }


        public void settoClose(IView close)
        {

            this.toClose = close;

        }


        #endregion
        public RegistrationViewModel()
        {
           _registerCommand = new DelegateCommand(Register,CanRegister);

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
            
            if (user !=null)
            {   
                users.Add(user);
                context.SaveChanges();

                MessageBox.Show("Contul dumneavoastra a fost inregistrat cu succes");
                IView authenticationWindow = null;
                AuthenticationViewModel viewModel = new AuthenticationViewModel(new AuthenticationService());
                authenticationWindow = new AuthenticationWindow(viewModel);
                viewModel.settoClose(authenticationWindow);
                authenticationWindow.Show();





                toClose.Close();

                

            }
            else
                MessageBox.Show("Trebuie sa completati campurile !");
           
        }

        public bool CanRegister(object parameter) {

            return true;
        }

        #region Methods
        


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
