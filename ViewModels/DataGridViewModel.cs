using MedicalClinic.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalClinic.ViewModels
{
    public class DataGridViewModel:INotifyPropertyChanged
    {

        private int _idService;
        private string _nameService;
        private double  _costService;
        private int _idSpecialization;
        private string _nameSpecialization;
        private ObservableCollection<Service> _services;
        private ObservableCollection<Specialization> _specializations;
      
        
        
        public int IdService { get { return _idService; } set { _idService = value; NotifyPropertyChanged("IdService"); }  }

        public string NameService { get { return _nameService; } set { _nameService = value; NotifyPropertyChanged("NameService"); } }
        public double CostService { get { return _costService; } set { _costService = value; NotifyPropertyChanged("CostService"); } }
        public int IdSpecialization { get { return _idSpecialization; }  set { _idSpecialization = value; NotifyPropertyChanged("IdSpecialization"); } }

        public string NameSpecialization { get { return _nameSpecialization; } set { _nameSpecialization = value; NotifyPropertyChanged("NameSpecialization"); } }
        public ObservableCollection<Service> Services { get { return _services; } set { _services = value;NotifyPropertyChanged("Services"); } }
        public ObservableCollection<Specialization> Specializations { get { return _specializations; }set { _specializations = value;NotifyPropertyChanged("Specializations"); } }
       
       

        public DataGridViewModel(ObservableCollection<Service> services,ObservableCollection<Specialization>specializations)
        {
            this.Services = services;
            this.Specializations = specializations;
        }
        public DataGridViewModel()
        {

        }

        public ObservableCollection<DataGridViewModel> FillDataGrid() {

            ObservableCollection<DataGridViewModel> dataSource = new ObservableCollection<DataGridViewModel>();
            foreach (Service s in Services) {
                DataGridViewModel data = new DataGridViewModel();
                data.IdService = s.idService;
                data.NameService = s.nameService;
                data.CostService = s.cost;
                data.IdSpecialization = s.idSpecialization;
                data.NameSpecialization = getNameSpecialization(s.idSpecialization);
                dataSource.Add(data);
            
            }
           
            return dataSource;
        }

        private string getNameSpecialization(int idSpecialization)
        {
            foreach (Specialization s in Specializations) {
                if (s.idSpecialization == idSpecialization)
                    return s.nameSpecialization;
            
            }
            return null;
        }
       

        internal ObservableCollection<string> getStringSpecializations()
        {
            ObservableCollection<string> spec = new ObservableCollection<string>();
            foreach (Specialization s in Specializations)
            {
                spec.Add(s.nameSpecialization);
            }

            return spec;
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
