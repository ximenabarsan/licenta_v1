using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalClinic.ViewModels
{
  public class DataGridAppoimentsDoctorViewModel
    {
        private String _nameUser;
        private String _surnameUser;
        private String _cnp;
        private DateTime _date;
        private String _service;
        private int _startHour;
        private int _finishHour;



        public DataGridAppoimentsDoctorViewModel()
        {
            
        }

        public String NameUser { get { return _nameUser; } set { _nameUser = value; } }
        public String SurnameUser { get { return _surnameUser; } set { _surnameUser = value; } }
        public String Cnp { get { return _cnp; } set { _cnp = value; } }
        public DateTime Date { get { return _date; } set { _date = value; } }
        public String Service { get { return _service; } set { _service = value; } }
        public int StartHour { get { return _startHour; } set { _startHour = value; } }
        public int FinishHour { get { return _finishHour; } set { _finishHour = value; } }



        #region Methods

        public ObservableCollection<DataGridAppoimentsDoctorViewModel> FillDataGrid(ObservableCollection<Appoiment> appoiments)
        {
            ObservableCollection<DataGridAppoimentsDoctorViewModel> dataSource = new ObservableCollection<DataGridAppoimentsDoctorViewModel>();
            foreach(Appoiment a in appoiments)
            {
                DataGridAppoimentsDoctorViewModel data = new DataGridAppoimentsDoctorViewModel();
                data.NameUser = GetNameUser(a.idUser);
                data.SurnameUser = GetSurnameUser(a.idUser);
                data.Cnp = GetCnpUser(a.idUser);
                data.Date = a.date;
                data.StartHour = a.startTime;
                data.FinishHour = a.startTime +1;
                data.Service = GetService(a.idService);
                dataSource.Add(data);
            }
            return dataSource;

        }

        public String GetNameUser(int id)
        {
            var context = new MedicalDBEntities();
           
            var query = from st in context.Users
                        where st.idUser == id
                        select st.nameUser;
            return query.FirstOrDefault<String>();


        }
        public String GetSurnameUser(int id)
        {
            var context = new MedicalDBEntities();

            var query = from st in context.Users
                        where st.idUser == id
                        select st.surnameUser;
            return query.FirstOrDefault<String>();


        }
        public String GetCnpUser(int id)
        {
            var context = new MedicalDBEntities();

            var query = from st in context.Users
                        where st.idUser == id
                        select st.CNP;
            return query.FirstOrDefault<String>();


        }

        public String GetService(int id)
        {
            var context = new MedicalDBEntities();

            var query = from st in context.Services
                        where st.idService == id
                        select st.nameService;
            return query.FirstOrDefault<String>();


        }


        #endregion

    }
}
