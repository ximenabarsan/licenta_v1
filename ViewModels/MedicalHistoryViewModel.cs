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
    public class MedicalHistoryViewModel : IViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<DataGridTreatmentsViewModel> _dataSource;
        private string _cnp;
        public IView viewToClose;
        private DelegateCommand _showViewCommand;
        private DelegateCommand _searchUserCommand;


        public MedicalHistoryViewModel()
        {
            _dataSource = new ObservableCollection<DataGridTreatmentsViewModel>();
            _showViewCommand = new DelegateCommand(ShowView, null);
            _searchUserCommand = new DelegateCommand(SearchUser, null);
            if (!DoctorRights)
            {
                _dataSource = FillGrid();
            }
            
        }


        public ObservableCollection<DataGridTreatmentsViewModel> DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; NotifyPropertyChanged("DataSource"); }
        }

        public String Cnp
        {
            get { return _cnp; }
            set { _cnp = value; NotifyPropertyChanged("Cnp");
                _dataSource = FillGrid();
                NotifyPropertyChanged("DataSource");
                }
        }


        public bool DoctorRights
        {
            get { return Thread.CurrentPrincipal.IsInRole("Doctor") ? true : false; }
        }
        public bool PatientRights
        {
            get { return Thread.CurrentPrincipal.IsInRole("Pacient") ? true : false; }
        }

        public DelegateCommand ShowViewCommand { get { return _showViewCommand; } }

        private void ShowView(object obj)
        {
            if (DoctorRights)
            {
                IView view;
                DoctorViewModel vmodel = new DoctorViewModel();

                view = new DoctorWindow(vmodel);
                vmodel.settoClose(view);
                view.Show();
                viewToClose.Close();
            }
            else
            {
                IView view;
                PatientViewModel vmodel = new PatientViewModel();

                view = new PatientWindow(vmodel);
                vmodel.settoClose(view);
                view.Show();
                viewToClose.Close();
            }
        }

        public DelegateCommand SearchUserCommand { get { return _searchUserCommand; } }
        private void SearchUser(object obj)
        {
            if (Cnp != null)
            {
                ObservableCollection<DataGridTreatmentsViewModel> dataSource = new ObservableCollection<DataGridTreatmentsViewModel>();
                ObservableCollection<Treatment> treatments = new ObservableCollection<Treatment>();
                DataGridTreatmentsViewModel data = new DataGridTreatmentsViewModel();
                CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                string currentEmail = customPrincipal.Identity.Email;
                var context = new MedicalDBEntities();
                var query = context.Users
                           .Where(s => s.email == currentEmail)
                           .FirstOrDefault<User>();
                User user = query;
                var queryDoc = context.Doctors
                           .Where(s => s.idUser == user.idUser)
                           .FirstOrDefault<Doctor>();
                Doctor doc = queryDoc;
                var patient = context.Users
                           .Where(s => s.CNP == Cnp)
                           .FirstOrDefault<User>();
                if (patient != null)
                {
                    var queryTreat = from st in context.Treatments
                                     where st.idDoctor == doc.idDoctor && st.idUser == patient.idUser
                                     select st;

                    if (queryTreat.FirstOrDefault<Treatment>() != null)
                    {
                        foreach (Treatment t in queryTreat)
                        {
                            treatments.Add(t);

                        }
                        dataSource = data.FillTreatmentsGrid(treatments);
                        _dataSource = dataSource;
                        NotifyPropertyChanged("DataSource");
                    }
                    else
                    {
                        MessageBox.Show("Acest pacient nu are tratament momentan");

                    }
                }else MessageBox.Show("Nu s-a gasit pacient cu acest CNP");
            }
            else MessageBox.Show("Introduceti mai intai CNP-ul!");

        }

        #region Methods
        public ObservableCollection<DataGridTreatmentsViewModel> FillGrid()
        {
            
            
                ObservableCollection<DataGridTreatmentsViewModel> dataSource = new ObservableCollection<DataGridTreatmentsViewModel>();
                ObservableCollection<Treatment> treatments = new ObservableCollection<Treatment>();
                DataGridTreatmentsViewModel data = new DataGridTreatmentsViewModel();
                CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                string currentEmail = customPrincipal.Identity.Email;
                var context = new MedicalDBEntities();
                var query = context.Users
                           .Where(s => s.email == currentEmail)
                           .FirstOrDefault<User>();
                User user = query;
                var queryTreat = from st in context.Treatments
                                 where st.idUser == user.idUser
                                 select st;
            if (queryTreat != null)
            {
                foreach (Treatment t in queryTreat)
                {
                    treatments.Add(t);

                }
                dataSource = data.FillTreatmentsGrid(treatments);
                return dataSource;
            }
            else
            {
                MessageBox.Show("Nu aveti introduse tratamente medicale momentan");
                return null;
            }
            
        }

        public void settoClose(IView close)
        {

            this.viewToClose = close;

        }





        #endregion

        #region INotifyPropertyChangedMembers
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    
        

}
}
