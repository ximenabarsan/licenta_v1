using MedicalClinic.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalClinic.ViewModels
{
   public class DoctorAppoimentsViewModel: IViewModel,INotifyPropertyChanged
    {


        private DateTime _date;
        private ObservableCollection<DataGridAppoimentsDoctorViewModel> _dataSource;
        public IView viewToClose;
        private DelegateCommand _showDoctorViewCommand;


        public DoctorAppoimentsViewModel()
        {
            _dataSource = new ObservableCollection<DataGridAppoimentsDoctorViewModel>();
            _date = DateTime.Today;
            _dataSource = FillAppoimentsGrid(_date);
            _showDoctorViewCommand = new DelegateCommand(ShowDoctorView, null);
        }



        public DateTime Date
        {
            get { return _date; }
            set { _date = value;NotifyPropertyChanged("Date");
                _dataSource=FillAppoimentsGrid(_date);
                NotifyPropertyChanged("DataSource");
            }
        }

        public ObservableCollection<DataGridAppoimentsDoctorViewModel> DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; NotifyPropertyChanged("DataSource"); }
        }


        public DelegateCommand ShowDoctorViewCommand { get { return _showDoctorViewCommand; } }
        private void ShowDoctorView(object obj)
        {
            IView view;
            DoctorViewModel vmodel = new DoctorViewModel();

            view = new DoctorWindow(vmodel);
            vmodel.settoClose(view);
            view.Show();
            viewToClose.Close();

        }






        public ObservableCollection<DataGridAppoimentsDoctorViewModel> FillAppoimentsGrid(DateTime date)
        {
            var context = new MedicalDBEntities();
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            string currentEmail = customPrincipal.Identity.Email;
            var query = context.Users
                       .Where(s => s.email == currentEmail)
                       .FirstOrDefault<User>();
            User user = query;
            var queryDoc = context.Doctors
                       .Where(s => s.idUser == user.idUser)
                       .FirstOrDefault<Doctor>();
            Doctor doc = queryDoc;

            var app = context.Appoiments
                        .Where(s => s.idDoctor == doc.idDoctor && s.date == date);
                     
            ObservableCollection<Appoiment> appoiments = new ObservableCollection<Appoiment>();
            foreach(Appoiment ap in app)
            {
                appoiments.Add(ap);
            }
            ObservableCollection<DataGridAppoimentsDoctorViewModel> dataSource = new ObservableCollection<DataGridAppoimentsDoctorViewModel>();
            DataGridAppoimentsDoctorViewModel data = new DataGridAppoimentsDoctorViewModel();
            dataSource=data.FillDataGrid(appoiments);
            return dataSource;

        }


        public void settoClose(IView close)
        {

            this.viewToClose = close;

        }




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
