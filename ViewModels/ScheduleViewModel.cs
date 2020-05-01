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
    public class ScheduleViewModel : IViewModel, INotifyPropertyChanged
    {
        private ObservableCollection<KeyValuePair<int, string>> _days;
        private KeyValuePair<int, string> _selectedDay;
        private ObservableCollection<int> _startHours;
        private ObservableCollection<int> _finishHours;
        private int _startHour;
        private int _finishHour;
        private DelegateCommand _editScheduleCommand;
        private DelegateCommand _showDoctorViewCommand;
        public IView viewToClose;

        public ScheduleViewModel()
        {
            _days = new ObservableCollection<KeyValuePair<int, string>>();
            _startHours = new ObservableCollection<int>();
            _finishHours = new ObservableCollection<int>();
            _days = FillDays();
            _editScheduleCommand = new DelegateCommand(EditSchedule, null);
            _showDoctorViewCommand = new DelegateCommand(ShowDoctorView, null);
        }


        public ObservableCollection<KeyValuePair<int, string>> Days {

            get { return _days; }

            set { _days = value; NotifyPropertyChanged("Days"); }

        }


        public KeyValuePair<int, string> SelectedDay
        {
            get { return _selectedDay; }
            set { _selectedDay = value; NotifyPropertyChanged("SelectedDay");
                _startHours = FillStartHours();
                NotifyPropertyChanged("StartHours");
            }

        }

        public ObservableCollection<int> StartHours
        {
            get { return _startHours; }
            set { _startHours = value; NotifyPropertyChanged("StartHours"); }
        }

        public ObservableCollection<int> FinishHours
        {
            get { return _finishHours; }
            set { _finishHours = value; NotifyPropertyChanged("FinishHours");

            }
        }
        public int StartHour
        {
            get { return _startHour; }
            set { _startHour = value; NotifyPropertyChanged("StartHour");
                _finishHours = FillFinishHours();
                NotifyPropertyChanged("FinishHours");
            }
        }

        public int FinishHour
        {
            get { return _finishHour; }
            set { _finishHour = value; NotifyPropertyChanged("FinishHour"); }
        }



        public DelegateCommand EditScheduleCommand { get { return _editScheduleCommand; } }


        private void EditSchedule(object obj)
        {   if (SelectedDay.Value != null && StartHour.ToString() != null && FinishHour.ToString() != null)
            {
                CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
                string currentEmail = customPrincipal.Identity.Email;
                var context = new MedicalDBEntities();
                var query = context.Users
                           .Where(s => s.email == currentEmail)
                           .FirstOrDefault<User>();
                var doctor = from doc in context.Doctors
                             where doc.idUser == query.idUser
                             select doc.idDoctor;
                int idDoc = doctor.FirstOrDefault<int>();
                
                var schedules = context.Schedules
                           .Where(s => s.idDoctor == idDoc && s.idDay == SelectedDay.Key)
                           .FirstOrDefault<Schedule>();
                
                if (schedules != null)
                {
                    
                    schedules.StartSchedule = StartHour;
                    schedules.FinishSchedule = FinishHour;
                    context.SaveChanges();
                    MessageBox.Show("Editarea programului din ziua de " + SelectedDay.Value + "s-a realizat cu succes! ");
                }
                else
                {
                    Schedule sch = new Schedule();
                    sch.idDoctor = doctor.FirstOrDefault<int>();
                    sch.StartSchedule = StartHour;
                    sch.FinishSchedule = FinishHour;
                    sch.idDay = SelectedDay.Key;
                    var sched = context.Schedules;
                    sched.Add(sch);
                    context.SaveChanges();
                    MessageBox.Show("Adaugarea programului din ziua de " + SelectedDay.Value + "s-a realizat cu succes! ");

                }
            }
            else MessageBox.Show("Va rugam selectati ziua, ora de incepere a programului si ora de finalizare a programului!");

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

        #region Methods

        public ObservableCollection<int> FillStartHours()
        {
            ObservableCollection<int> hours = new ObservableCollection<int>();

            for (int i = 8; i <= 17; i++)
            {
                hours.Add(i);
            }
            return hours;
        }

        public ObservableCollection<int> FillFinishHours()
        {
            ObservableCollection<int> hours = new ObservableCollection<int>();

            for (int i = StartHour + 1; i <= 18; i++)
            {
                hours.Add(i);
            }
            return hours;

        }




        public ObservableCollection<KeyValuePair<int, string>>FillDays(){
            var context = new MedicalDBEntities();
            var query = from day in context.Weeks
                        select day;
            ObservableCollection<KeyValuePair<int, string>> days = new ObservableCollection<KeyValuePair<int, string>>();
            foreach (var day in query)
            {
                days.Add(new KeyValuePair<int, string>(day.idDay, day.nameDay));
            }
            return days;
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
