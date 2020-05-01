using MedicalClinic.Views;
using System;
using System.Collections;
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
    public class AppoimentsViewModel : IViewModel, INotifyPropertyChanged
    {
        public IView viewtoClose;
        private ObservableCollection<Specialization> _specializations;
        private Specialization _selectedSpecialization;
        private ObservableCollection<Service> _services;
        private Service _selectedService;
        private DateTime _selectedDate;
        private int _selectedStartHour;
        private ObservableCollection<int> _availableStartHours;
        private ObservableCollection<KeyValuePair<int,string>> _availableDoctors;
        private KeyValuePair<int,string> _selectedDoctor;
        private DateTime _tomorrowDate;
        private DateTime _maxDate;



        private DelegateCommand _showPatientViewCommand;
        private DelegateCommand _addAppoimentCommand;




        public AppoimentsViewModel()
        {
            _showPatientViewCommand = new DelegateCommand(ShowPatientView, null);
            _specializations = FillSpecialization();
            _services = new ObservableCollection<Service>();
            _availableStartHours = new ObservableCollection<int>();
            _tomorrowDate = DateTime.Today.AddDays(1);
            _maxDate = DateTime.Today.AddDays(30);
            _selectedDate = DateTime.Today.AddDays(1);
            _addAppoimentCommand = new DelegateCommand(AddAppoiment,null);

        }

       




        #region Properties
        public ObservableCollection<Specialization> Specializations
        {
            get {
                return _specializations;
            }

            set
            {
                _specializations = value;
                NotifyPropertyChanged("Specializations");
            }

        }


        public Specialization SelectedSpecialization {
            get {
                return _selectedSpecialization;
            }
            set {
                _selectedSpecialization = value;
                NotifyPropertyChanged("SelectedSpecialization");
                this.Services = FillServices(_selectedSpecialization.idSpecialization);
                NotifyPropertyChanged("Services");
                this.SelectedService = null;
                NotifyPropertyChanged("SelectedService");
                if (SelectedSpecialization != null)
                {
                    this.AvailableDoctors = FillDoctors(_selectedSpecialization.idSpecialization);
                    NotifyPropertyChanged("AvailableDoctors");

                }
            }
        }

        public ObservableCollection<Service> Services {
            get { return _services;
            }
            set {

                _services = value;
                NotifyPropertyChanged("Services");
            }

        }

        public Service SelectedService {

            get {
                return _selectedService;
            }
            set
            {
                _selectedService = value;
                NotifyPropertyChanged("SelectedService");
                
            }

        }


        public DateTime SelectedDate {
            get { return _selectedDate.Date; }

            set {
                _selectedDate = value;
                NotifyPropertyChanged("SelectedDate");
                //MessageBox.Show(SelectedDate.ToShortDateString());
               if (SelectedDate != null && SelectedDoctor.Value!=null)
               {
                    this.AvailableHours = FillAvailableHours();
                    NotifyPropertyChanged("AvailableHours");
                }
            }

        }

        public ObservableCollection<int> AvailableHours {
            get {
                return _availableStartHours;
            }
            set {
                _availableStartHours = value;
                NotifyPropertyChanged("AvailableHours");

            }
        }

        public int SelectedStartHour {
            get {
                return _selectedStartHour;
            }
            set {
                _selectedStartHour = value;
                NotifyPropertyChanged("SelectedStartHour");
            }

        }
       

        public ObservableCollection<KeyValuePair<int,string>> AvailableDoctors {

            get
            {
                return _availableDoctors;
            }
            set {
                _availableDoctors = value;
                NotifyPropertyChanged("AvailableDoctors");

            }
        }

        public KeyValuePair<int,string> SelectedDoctor {
            get
            {
                return _selectedDoctor;
            }
            set
            {
                _selectedDoctor = value;
                NotifyPropertyChanged("SelectedDoctor");
            }
        }

        public DateTime TomorrowDate
        {
            get
            {
                return _tomorrowDate;
            }
            set
            {
                _tomorrowDate = value;
                NotifyPropertyChanged("TomorrowDate");
            }
        }
        
        public DateTime MaxDateAppoiment {

            get
            {
                return _maxDate;
            }
            set
            {
                _maxDate =value;
                NotifyPropertyChanged("MaxDateAppoiment");
            }
        
        }


        #endregion







        #region Commands


        public DelegateCommand ShowPatientViewCommand { get { return _showPatientViewCommand; } }

        private void ShowPatientView(object obj)
        {
            PatientViewModel patientViewModel = new PatientViewModel();
            IView patientView = new PatientWindow(patientViewModel);
            patientViewModel.settoClose(patientView);
            patientView.Show();
            viewtoClose.Close();
        }
        public DelegateCommand AddAppoimentCommand { get { return _addAppoimentCommand; } }

        private void AddAppoiment(object obj)
        {
            using (var context = new MedicalDBEntities())
            {
                CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                string currentEmail = customPrincipal.Identity.Email;
                var query = context.Users
                           .Where(s => s.email == currentEmail)
                           .FirstOrDefault<User>();
                var currentUser = query;
                Appoiment ap = new Appoiment();
                if (SelectedDate != null && SelectedSpecialization != null && SelectedService != null && SelectedStartHour.ToString() != null)
                {
                    ap.idUser = query.idUser;
                    ap.idService = SelectedService.idService;
                    ap.idDoctor = SelectedDoctor.Key;
                    ap.date = SelectedDate;
                    ap.startTime = SelectedStartHour;
                    context.Appoiments.Add(ap);
                    context.SaveChanges();
                    MessageBox.Show("Programare realizata cu succes");
                }
                else MessageBox.Show("Introduceti toate datele!");
            }

        }

       /* private bool CanAddAppoiment(object obj)
        {
            if (SelectedSpecialization != null && SelectedService != null && SelectedDoctor.Value != null && SelectedStartHour.ToString() != null)
            {
                return true;
            }
            return false;
        }*/

        #endregion

        #region Methods



        private ObservableCollection<int> FillAvailableHours()
        {
            
            using (MedicalDBEntities context = new MedicalDBEntities())
            {
               

                ObservableCollection<int> existingHours = new ObservableCollection<int>();
                var selectExHours = from st in context.Appoiments
                                    where st.date == SelectedDate && st.idDoctor == SelectedDoctor.Key
                                    select st.startTime;
                int x = selectExHours.Count();
                if (x>0)
                {


                    foreach (int i in selectExHours)
                    {
                        existingHours.Add(i);
                    }
                }
                ObservableCollection<int> availableHours = new ObservableCollection<int>();
                int idDay = 0;
                

                switch (SelectedDate.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        idDay = 1;
                        break;
                    case DayOfWeek.Tuesday:
                        idDay = 2;
                        break;
                    case DayOfWeek.Wednesday:
                        idDay = 3;
                        break;
                    case DayOfWeek.Thursday:
                        idDay = 4;
                        break;
                    case DayOfWeek.Friday:
                        idDay = 5;
                        break;
                    default:
                        MessageBox.Show("Nu e in timpul saptamanii");
                        break;
                }

                if (idDay == 0)
                    MessageBox.Show("Va rugam sa introduceti o data din timpul saptamanii");
                else
                {

                    var selectDaySchedule = from st in context.Schedules
                                            where st.idDay == idDay && st.idDoctor == SelectedDoctor.Key
                                            select st;
                    Schedule scheduleDoctor = selectDaySchedule.FirstOrDefault<Schedule>();

                    if (scheduleDoctor == null)
                        MessageBox.Show("Doctorul respectiv nu are program in aceasta zi");

                    else
                    {
                        
                        for (int i = scheduleDoctor.StartSchedule; i < scheduleDoctor.FinishSchedule; i++)
                        {
                            foreach (int j in existingHours)
                            {
                                if (i != j) availableHours.Add(i);
                            }
                            if (existingHours.Count == 0)
                            {
                                availableHours.Add(i);

                            }
                        }


                        return availableHours;
                    }
                }
                return null;
            }


              
            }
        private ObservableCollection<Service> FillServices(int id)
        {
        
            using (var context = new MedicalDBEntities())
            {
                //if (_services != null) _services.Clear();
                var query = from st in context.Services
                            where st.idSpecialization == id
                            select st;
                if (query == null) { MessageBox.Show("Nu sunt servicii la aceasta specializare"); }
                else
                {

                    ObservableCollection<Service> services = new ObservableCollection<Service>(query);
                   
                    return services;
                }
                return null;
            }
        }


        public void settoClose(IView close)
        {

            this.viewtoClose = close;

        }

        public ObservableCollection<Specialization> FillSpecialization()
        {
            using (MedicalDBEntities context = new MedicalDBEntities())
            {
                var query = from speci in context.Specializations select speci;

                ObservableCollection<Specialization> spec = new ObservableCollection<Specialization>(query);


                return spec;
            }
        }


        public ObservableCollection<KeyValuePair<int,string>> FillDoctors(int id)
        {   
             using (MedicalDBEntities context = new MedicalDBEntities())
            {
                ObservableCollection<String> doctors = new ObservableCollection<String>();
             
                var query = from usr in context.Users
                            join doc in context.Doctors on
                            usr.idUser equals doc.idUser
                            where doc.idSpecialization==id
                            select new {DoctorName=usr.nameUser, DoctorId=doc.idDoctor };
                ObservableCollection<KeyValuePair<int, string>> doctorsName=new ObservableCollection<KeyValuePair<int, string>>();

                foreach (var v in query)
                {
                    doctorsName.Add(new KeyValuePair<int, string> (v.DoctorId,v.DoctorName));
                    Console.WriteLine(v.DoctorName);
                  
                }
               

                //if (query == null) { MessageBox.Show("Nu sunt servicii la aceasta specializare"); }
                return doctorsName;
                
                
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
