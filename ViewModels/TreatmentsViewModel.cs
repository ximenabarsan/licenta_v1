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
 
    public class TreatmentsViewModel: IViewModel,INotifyPropertyChanged
    {
        public IView viewToClose;
        private DelegateCommand _showDoctorViewCommand;
        private DelegateCommand _showUsersCommand;
        private DelegateCommand _showDiagnosisCommand;
        private DelegateCommand _addDiagnosisCommand;
        private DelegateCommand _showMedicineCommand;
        private DelegateCommand _addMedicineCommand;
        private DelegateCommand _addSelectedMedicinesCommand;
        private DelegateCommand _deleteMedicineCommand;
        private DelegateCommand _addTreatmentCommand;
       

        private string _cnp;
        private string _diagnosis;
        private string _medicine;
        private ObservableCollection<User> _usersSource;
        private User _selectedUser;
        private ObservableCollection<Diagnosi> _diagnosisSource;
        private Diagnosi _selectedDiagnosis;
        private ObservableCollection<Medicine> _medicinesSource;
        private Medicine _selectedMedicine;
        private ObservableCollection<Medicine> _selectedMedicines;
        private Medicine _selectedFinal;
        private String _observations;
        private DateTime _startDate;
        private DateTime _finishDate;
      
     








        public TreatmentsViewModel()
        {
            _showDoctorViewCommand = new DelegateCommand(ShowDoctorView, null);
            _usersSource = new ObservableCollection<User>();
            _diagnosisSource = new ObservableCollection<Diagnosi>();
            _medicinesSource = new ObservableCollection<Medicine>();
            _showUsersCommand = new DelegateCommand(ShowUsers, null);
            _showDiagnosisCommand = new DelegateCommand(ShowDiagnosis, null);
            _addDiagnosisCommand = new DelegateCommand(AddDiagnosis, null);
            _showMedicineCommand = new DelegateCommand(ShowMedicine, null);
            _addMedicineCommand = new DelegateCommand(AddMedicine, null);
            _selectedMedicines = new ObservableCollection<Medicine>();
            _addSelectedMedicinesCommand = new DelegateCommand(AddSelectedMedicine, null);
            _deleteMedicineCommand = new DelegateCommand(DeleteMedicine, null);
            _addTreatmentCommand = new DelegateCommand(AddTreatment, null);
            _startDate= DateTime.Today;
            _finishDate = DateTime.Today;


        }



        public String Cnp
        {
            get { return _cnp; }
            set { _cnp = value;  NotifyPropertyChanged("CNP"); }
        }

        public String Diagnosis
        {
            get { return _diagnosis; }
            set { _diagnosis = value; NotifyPropertyChanged("Diagnosis"); }
        }

        public String Medicine
        {
            get { return _medicine; }
            set { _medicine = value; }
        }



        public ObservableCollection<User> UsersSource
        {
            get { return _usersSource; }
            set { _usersSource = value; NotifyPropertyChanged("UsersSource"); }
        }

        public User SelectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; NotifyPropertyChanged("SelectedUser"); }
        }

        public ObservableCollection<Diagnosi> DiagnosisSource
        {
            get { return _diagnosisSource; }
            set { _diagnosisSource = value; NotifyPropertyChanged("DiagnisisSource"); }
        }

        public Diagnosi SelectedDiagnosis
        {
            get { return _selectedDiagnosis; }
            set { _selectedDiagnosis = value; NotifyPropertyChanged("SelectedDiagnosis"); }
        }

        public ObservableCollection<Medicine> MedicinesSource
        {
            get { return _medicinesSource; }
            set { _medicinesSource = value;NotifyPropertyChanged("MedicinesSource"); }
        }

        public Medicine SelectedMedicine
        {
            get { return _selectedMedicine; }
            set { _selectedMedicine = value; }
        }
         
        public ObservableCollection<Medicine> SelectedMedicines//medicamentele alese pt bd
        {
            get { return _selectedMedicines; }
            set { _selectedMedicines = value;NotifyPropertyChanged("SelectedMedicines"); }
        }

        public Medicine SelectedFinal
        {
            get { return _selectedFinal; }
            set { _selectedFinal = value; NotifyPropertyChanged("SelectedFinal"); }
        }

        public String Observations
        {
            get { return _observations; }
            set { _observations = value; NotifyPropertyChanged("Observations"); }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; NotifyPropertyChanged("StartDate"); }

        }
        public DateTime FinishDate
        {
            get { return _finishDate; }
            set { _finishDate = value; NotifyPropertyChanged("FinishDate"); }

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


        public DelegateCommand ShowUsersCommand { get { return _showUsersCommand; } }


        private void ShowUsers(object obj)
        {
            if (Cnp != null)
            {
               _usersSource=FillUsers();
                NotifyPropertyChanged("UsersSource");
            }
            else MessageBox.Show("Introduceti mai intai o parte din CNP");

        }


        public DelegateCommand ShowDiagnosisCommand { get { return _showDiagnosisCommand; } }

        private void ShowDiagnosis(object obj)
        {
            if (Diagnosis != null)
            {
                _diagnosisSource = FillDiagnosis();
                NotifyPropertyChanged("DiagnosisSource");
            }
            else MessageBox.Show("Trebuie sa introduceti diagnosticul");

        }


        public DelegateCommand AddDiagnosisCommand { get { return _addDiagnosisCommand; } }

        private void AddDiagnosis(object obj)
        {
            if (Diagnosis != null)
            {
                var context = new MedicalDBEntities();

                var query = context.Diagnosis
                            .Where(s => s.nameDiagnosis == Diagnosis)
                            .FirstOrDefault<Diagnosi>();
                if (query == null)
                {
                    var diagnosis = context.Diagnosis;
                    Diagnosi diag = new Diagnosi();
                    diag.nameDiagnosis = Diagnosis;
                    diagnosis.Add(diag);
                    context.SaveChanges();
                    _diagnosisSource = FillDiagnosis();
                    NotifyPropertyChanged("DiagnosisSource");
                    MessageBox.Show("Ati adaugat cu succes!");

                }
                else
                {
                    MessageBox.Show("Diagnosticul exista deja!");
                }
            }
            else MessageBox.Show("Va rugam introduceti diagnosticul!");

        }

        public DelegateCommand ShowMedicineCommand { get { return _showMedicineCommand; } }


        private void ShowMedicine(object obj)
        {
            if (Medicine != null)
            {
                _medicinesSource = FillMedicines();
                NotifyPropertyChanged("MedicinesSource");
            }
            else MessageBox.Show("Trebuie sa introduceti denumirea medicamentului");

        }


        public DelegateCommand AddMedicineCommand { get { return _addMedicineCommand; } }

        private void AddMedicine(object obj)
        {
            if (Medicine != null)
            {
                var context = new MedicalDBEntities();

                var query = context.Medicines
                            .Where(s => s.nameMedicine  == Medicine)
                            .FirstOrDefault<Medicine>();
                if (query == null)
                {
                    var medicines = context.Medicines;
                    Medicine med = new Medicine();
                     med.nameMedicine = Medicine;
                    medicines.Add(med);
                    context.SaveChanges();
                    _medicinesSource = FillMedicines();
                    NotifyPropertyChanged("MedicinesSource");
                    MessageBox.Show("Ati adaugat cu succes!");

                }
                else
                {
                    MessageBox.Show("Diagnosticul exista deja!");
                }
            }
            else MessageBox.Show("Va rugam introduceti denumirea medicamentului!");

        }


       public DelegateCommand AddSelectedMedicinesCommand { get { return _addSelectedMedicinesCommand; } }

        private void AddSelectedMedicine(object obj)
        {
            if (SelectedMedicine != null)
            {
                _selectedMedicines.Add(SelectedMedicine);
                NotifyPropertyChanged("SelectedMedicines");
            }
            else MessageBox.Show("Trebuie sa alegeti medicamentul mai intai");
        }

        public DelegateCommand DeleteMedicineCommand { get { return _deleteMedicineCommand; } }

        private void DeleteMedicine(object obj)
        {
            if (SelectedFinal != null)
            {
                var context = new MedicalDBEntities();
                var medicines = context.Medicines;
                _selectedMedicines.Remove(SelectedFinal);
                NotifyPropertyChanged("SelectedMedicines");
            }
            else MessageBox.Show("Trebuie sa alegeti medicamentul pe care doriti sa il stergeti mai intai!");

        }
         public DelegateCommand AddTreatmentCommand { get { return _addTreatmentCommand; } }

        private void AddTreatment(object obj)
        {
            if(SelectedUser!=null && SelectedDiagnosis !=null && SelectedMedicines!=null && StartDate!=null && FinishDate!=null)
            {
                Treatment treat = new Treatment();
                treat.idUser = SelectedUser.idUser;
                treat.idDiagnosis = SelectedDiagnosis.idDiagnosis;
                treat.dateStart = StartDate;
                treat.dateFinish = FinishDate;
                treat.observation = Observations;
                treat.idDoctor = GetDoctorId();
                var context = new MedicalDBEntities();
                var treatments = context.Treatments;
                treatments.Add(treat);
                var prescriptions = context.Prescriptions;
                foreach(Medicine m in SelectedMedicines)
                {
                    Prescription p = new Prescription();
                    p.idTreatment = treat.idTreatment;
                    p.idMedicine = m.idMedicine;
                    prescriptions.Add(p);

                }
                MessageBox.Show("Tratament adaugat cu succes!");
                context.SaveChanges();
                _usersSource = null;
                NotifyPropertyChanged("UsersSource");
                _diagnosisSource = null;
                NotifyPropertyChanged("DiagnosisSource");
                _medicinesSource = null;
                NotifyPropertyChanged("MedicinesSource");
                _observations = null;
                NotifyPropertyChanged("Observations");
                _startDate =default ;
                NotifyPropertyChanged("StartDate");
                _finishDate = default;
                NotifyPropertyChanged("FinishDate");
                _selectedMedicines = null;
                NotifyPropertyChanged("SelectedMedicines");
                _cnp = null;
                NotifyPropertyChanged("Cnp");
                _diagnosis = null;
                NotifyPropertyChanged("Diagnosis");
                _medicine = null;
                NotifyPropertyChanged("Medicine");

            }
            else
            {
                MessageBox.Show("Introduceți mai întâi toate valorile!");
            }
        }






        #region Methods
        public void settoClose(IView close)
        {

            this.viewToClose = close;

        }

        public ObservableCollection<User> FillUsers()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();
            var context = new MedicalDBEntities();
            var matches = from m in context.Users
                          where m.CNP.Contains(Cnp)
                          select m;
            if (matches != null)
            {
                foreach (var i in matches)
                {
                    users.Add(i);
                }

                return users;
            }
            else { MessageBox.Show("Nu sunt useri care au acest CNP");
               return null;
            }
        }

        public ObservableCollection<Diagnosi> FillDiagnosis()
        {
            ObservableCollection<Diagnosi> diagnosis = new ObservableCollection<Diagnosi>();
            var context = new MedicalDBEntities();
            var matches = from m in context.Diagnosis
                          where m.nameDiagnosis.Contains(Diagnosis)
                          select m;
            if (matches != null)
            {
                foreach (var i in matches)
                {
                   diagnosis.Add(i);
                }

                return diagnosis;
            }
            else
            {
                MessageBox.Show("Nu exista acest diagnostic, puteti sa il adaugati!");
                return null;
            }

        }

        public ObservableCollection<Medicine> FillMedicines()
        {
            ObservableCollection<Medicine> medicines = new ObservableCollection<Medicine>();
            var context = new MedicalDBEntities();
            var matches = from m in context.Medicines
                          where m.nameMedicine.Contains(Medicine)
                          select m;
            if (matches != null)
            {
                foreach (var i in matches)
                {
                    medicines.Add(i);
                }

                return medicines;
            }
            else
            {
                MessageBox.Show("Nu sunt medicamente care sa aiba acest nume");
                return null;
            }
        }


        public int GetDoctorId()
        {
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
            return doc.idDoctor;


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
