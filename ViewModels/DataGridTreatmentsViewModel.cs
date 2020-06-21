using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalClinic.ViewModels
{
    public class DataGridTreatmentsViewModel
    {
     

        public DataGridTreatmentsViewModel()
        {

        }


        public string NamePatient { get; set; }
        public string SurnamePatient { get; set; }
        public string Cnp { get; set; }
        public string Diagnosis { get; set; }
        public string NameDoctor { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Medicines { get; set; }
        public string Observations { get; set; }
        public bool DoctorRights
        {
            get { return Thread.CurrentPrincipal.IsInRole("Doctor") ? true : false; }
        }
        public bool PatientRights
        {
            get { return Thread.CurrentPrincipal.IsInRole("Pacient") ? true : false; }
        }


        public ObservableCollection<DataGridTreatmentsViewModel> FillTreatmentsGrid(ObservableCollection<Treatment> treatments)
        {
            ObservableCollection<DataGridTreatmentsViewModel> dataSource = new ObservableCollection<DataGridTreatmentsViewModel>();

            foreach(Treatment t in treatments)
            {
                DataGridTreatmentsViewModel data = new DataGridTreatmentsViewModel();
                data.NamePatient = GetPatientName(t.idUser);
                data.SurnamePatient = GetPatientSurname(t.idUser);
                data.Cnp = GetPatientCnp(t.idUser);
                data.Diagnosis = GetDiagnosis(t.idDiagnosis);
                data.NameDoctor = GetDoctorName(t.idDoctor);
                data.StartDate = t.dateStart;
                data.FinishDate = t.dateFinish;
                data.Medicines = GetMedicines(t.idTreatment);
                data.Observations = t.observation;
                dataSource.Add(data);
            }
            return dataSource;
        }

        #region Methods

        public string GetPatientName(int id)
        {
            var context = new MedicalDBEntities();

            var query = from st in context.Users
                        where st.idUser == id
                        select st.nameUser;
            return query.FirstOrDefault<String>();


        }

        public string GetPatientSurname(int id)
        {
            var context = new MedicalDBEntities();

            var query = from st in context.Users
                        where st.idUser == id
                        select st.surnameUser;
            return query.FirstOrDefault<String>();
        }

        public string GetPatientCnp(int id)
        {
            var context = new MedicalDBEntities();

            var query = from st in context.Users
                        where st.idUser == id
                        select st.CNP;
            return query.FirstOrDefault<String>();
        }

        public string GetDiagnosis(int id)
        {
            var context = new MedicalDBEntities();

            var query = from st in context.Diagnosis
                        where st.idDiagnosis == id
                        select st.nameDiagnosis;
            return query.FirstOrDefault<String>();
        }

        public string GetDoctorName(int id)
        {
            var context = new MedicalDBEntities();

            var query = from st in context.Doctors
                        where st.idDoctor == id
                        select st.idUser;
            var query1 = from st in context.Users
                        where st.idUser == query.FirstOrDefault<int>()
                        select st.nameUser;

            return query1.FirstOrDefault<String>();
        }

        public string GetMedicines(int id)
        {
            string medicines = "";
            var context = new MedicalDBEntities();

            var query = from st in context.Prescriptions
                        where st.idTreatment == id
                        select st;
            foreach(Prescription p in query)
            {
                var query1= from st in context.Medicines
                            where st.idMedicine== p.idMedicine
                            select st.nameMedicine;
                medicines = medicines + " " + query1.FirstOrDefault<String>();
            }
            return medicines;

        }





        #endregion

    }
}
