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

namespace MedicalClinic.ViewModels
{
    public class PatientsAppoimentsViewModel:IViewModel,INotifyPropertyChanged
    {
        private ObservableCollection<DataGridAppoimentsViewModel> _dataSource;
        private DataGridAppoimentsViewModel _selectedAppoiment;
        private DelegateCommand _showPatientViewCommand;
        public IView viewToClose;
        private DelegateCommand _deleteAppoimentCommand;

        public PatientsAppoimentsViewModel()
        {
            _showPatientViewCommand = new DelegateCommand(ShowView, null);
            _deleteAppoimentCommand = new DelegateCommand(DeleteAppoiment, null);
            _dataSource = new ObservableCollection<DataGridAppoimentsViewModel>();
            _dataSource = FillDataGrid();
        }

        public ObservableCollection<DataGridAppoimentsViewModel>DataSource
        {
            get { return _dataSource; }
            set {
                _dataSource = value;
                NotifyPropertyChanged("DataSource"); }
        }

        public DataGridAppoimentsViewModel SelectedAppoiment
        {
            get { return _selectedAppoiment; }
            set { _selectedAppoiment = value; NotifyPropertyChanged("SelectedAppoiment"); }
        }



        public DelegateCommand ShowPatientViewCommand { get { return _showPatientViewCommand; } }

        private void ShowView(object obj)
        {
            IView view;
            PatientViewModel vmodel = new PatientViewModel();

            view = new PatientWindow(vmodel);
            vmodel.settoClose(view);
            view.Show();
            viewToClose.Close();

        }

        public DelegateCommand DeleteAppoimenCommand { get { return _deleteAppoimentCommand; } }

        private void DeleteAppoiment(object obj)
        {
            if (SelectedAppoiment != null)
            {
                var context = new MedicalDBEntities();
                var query = context.Appoiments
                           .Where(s => s.idAppoiment == SelectedAppoiment.AppoimentId)
                           .FirstOrDefault<Appoiment>();
                Appoiment ap = query;
                var appoiments = context.Appoiments;
                _dataSource.Remove(SelectedAppoiment);
                appoiments.Remove(ap);
                context.SaveChanges();
                MessageBox.Show("Programare stearsa cu succes");

                

            }
            else MessageBox.Show("Va rugam selectati programarea pe care doriti sa o stergeti");

        }

        #region Methods

        private ObservableCollection<DataGridAppoimentsViewModel> FillDataGrid()
        {
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            string currentEmail = customPrincipal.Identity.Email;
            var context = new MedicalDBEntities();
            var query = context.Users
                       .Where(s => s.email == currentEmail)
                       .FirstOrDefault<User>();
            User currentUser = query;


            var appoimentsQuery = from st in context.Appoiments
                                  where st.idUser == currentUser.idUser
                                  select st;
            var app= appoimentsQuery.FirstOrDefault<Appoiment>();
            ObservableCollection<Appoiment> appoiments = new ObservableCollection<Appoiment>();
           foreach(var ap in appoimentsQuery)
            {
                appoiments.Add(ap);
            }
            DataGridAppoimentsViewModel data = new DataGridAppoimentsViewModel();
            ObservableCollection<DataGridAppoimentsViewModel> source = new ObservableCollection<DataGridAppoimentsViewModel>();
            source = data.FillAppoimentsGrid(appoiments);
            return source;
        }

        public void settoClose(IView close)
        {

            this.viewToClose = close;

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
