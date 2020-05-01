using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalClinic.ViewModels
{
   public  class DataGridAppoimentsViewModel:IViewModel
    {
        private string  _date;
        private int _startHour;
        private int _finishHour;
        private string _doctor;
        private  string _specialization;
        private string _service;
        private int _appoimentId;


         public String Date
        {
            get { return _date; }
            set {  _date = value; }
        }
        public int StartHour
        {
            get { return _startHour; }
            set {_startHour = value; }
        }

        public int FinishHour
        {
            get { return _finishHour ; }
            set {_finishHour = value; }
        }

        public string Doctor
        {
            get { return _doctor; }
            set { _doctor = value; }
        }

        public String Specialization
        {
            get { return _specialization; }
            set { _specialization = value; }
        }

        public String Service
        {
            get { return _service; }
            set { _service = value; }
        }

        public int AppoimentId
        {
            get { return _appoimentId; }
            set { _appoimentId = value; }
        }
        public DataGridAppoimentsViewModel()
        {
            

        }

        public ObservableCollection<DataGridAppoimentsViewModel> FillAppoimentsGrid(ObservableCollection<Appoiment> Appoiments)
        {
            ObservableCollection<DataGridAppoimentsViewModel> data = new ObservableCollection<DataGridAppoimentsViewModel>();
            foreach(Appoiment ap in Appoiments)
            {
                DataGridAppoimentsViewModel apData = new DataGridAppoimentsViewModel();
                apData.Date = ap.date.ToString("dd/MM/yyyy");
                apData.Doctor = GetDoctor(ap.idDoctor);
                apData.Specialization = GetSpecialization(ap.idService);
                apData.Service = GetService(ap.idService);
                apData.StartHour = ap.startTime;
                apData.FinishHour = ap.startTime+1;
                apData.AppoimentId = ap.idAppoiment;
                data.Add(apData);

                
                
            }
            return data;

        }



        public string GetDoctor(int id)
        {
            var context = new MedicalDBEntities();
            var user = from st in context.Doctors
                                where st.idDoctor == id
                                select st.idUser;
            int userId = user.FirstOrDefault<int>();
            var doctor= from st in context.Users
                        where st.idUser ==userId
                        select st.nameUser;
            string nameDoctor = doctor.FirstOrDefault<String>();
            return nameDoctor;

        }

        public String GetSpecialization(int id)
        {
            var context = new MedicalDBEntities();
            var service = from st in context.Services
                       where st.idService == id
                       select st.idSpecialization;
            int specializationId = service.FirstOrDefault<int>();
            var specialization= from st in context.Specializations
                                    where st.idSpecialization ==specializationId
                                    select st.nameSpecialization;
            string nameSpec = specialization.FirstOrDefault<String>();
            return nameSpec;
        }

        public String GetService(int id)
        {
            var context = new MedicalDBEntities();
            var service = from st in context.Services
                          where st.idService == id
                          select st.nameService;
            string serviceName = service.FirstOrDefault<string>();
            return serviceName;

        }

    }
}
