using MedicalClinic.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedicalClinic.ViewModels
{
    public class EditUsersViewModel:IViewModel,INotifyPropertyChanged
    {
        public IView viewToClose;

        private DelegateCommand _showAdminViewCommand;
        private DelegateCommand _editUserCommand;
        private DelegateCommand _deleteUserCommand;
        private ObservableCollection<User> _users;
        private User _selectedUser;




        public EditUsersViewModel()
        {
            _showAdminViewCommand = new DelegateCommand(ShowAdminView, null);
            _users = FillUsers();
            _editUserCommand = new DelegateCommand(EditUser, null);
            _deleteUserCommand = new DelegateCommand(DeleteUser, null);
        }


        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { _users = value;NotifyPropertyChanged("Users"); }
        }


        public User SelectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value;NotifyPropertyChanged("SelectedUser"); }
        }


        public DelegateCommand ShowAdminViewCommand { get { return _showAdminViewCommand; } }

        private void ShowAdminView(object obj)
        {
            AdminViewModel adminViewModel = new AdminViewModel();
            IView adminView = new AdminWindow(adminViewModel);
            adminViewModel.settoClose(adminView);
            adminView.Show();
            viewToClose.Close();
        }

        public DelegateCommand EditUserCommand { get { return _editUserCommand; } }

        private void EditUser(object obj)
        {
            if (SelectedUser != null)
            {
                var context = new MedicalDBEntities();
                var query = from st in context.Users
                            where st.idUser == SelectedUser.idUser
                            select st;
                User user = query.FirstOrDefault<User>();
                user.nameUser = SelectedUser.nameUser;
                user.email = SelectedUser.email;
                user.password = SelectedUser.password;
                user.surnameUser = SelectedUser.surnameUser;
                var users = context.Users;
                context.SaveChanges();
                MessageBox.Show("Ati editat cu succes!");
            }
            else MessageBox.Show("Va rugam selectati userul pe care doriti sa il editati");

        }

        public DelegateCommand DeleteUserCommand { get { return _deleteUserCommand; } }

        private void DeleteUser(object obj)
        {
            if (SelectedUser != null)
            {
                var context = new MedicalDBEntities();
                var query = from st in context.Users
                            where st.idUser == SelectedUser.idUser
                            select st;
                User user = query.FirstOrDefault<User>();
                var users = context.Users;
                _users.Remove(SelectedUser);
                users.Remove(user);
                context.SaveChanges();
                MessageBox.Show("User sters cu succes!");

            }
            else MessageBox.Show("Va rugam selectati userul pe care doriti sa il stergeti");

        }

        #region Methods


        public void settoClose(IView close)
        {

            this.viewToClose = close;

        }

        public ObservableCollection<User> FillUsers()
        {
            var context = new MedicalDBEntities();
            var query = from st in context.Users
                        select st;
            ObservableCollection<User> users = new ObservableCollection<User>();
            if (query != null)
            {
                foreach(var u in query)
                {
                    users.Add(u);
                }

                return users;

            }
            else
            {
                MessageBox.Show("Nu exista useri");
                return null;
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
