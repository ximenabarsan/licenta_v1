using MedicalClinic.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MedicalClinic.ViewModels
{

    public class ServicesViewModel : IViewModel, INotifyPropertyChanged
    {
        private int _idService;
        private string _nameService;
        private float _costService;
        IView viewToClose;
        private DataGridViewModel _selectedService;
        private ObservableCollection<String> _specializations;
        private DelegateCommand _showViewCommand;
        private DelegateCommand _editServicesCommand;
        private DelegateCommand _addServiceCommand;
        private DelegateCommand _deleteServiceCommand;
        private ObservableCollection<DataGridViewModel> _dataSource; 





        public ServicesViewModel()
        {
            _showViewCommand = new DelegateCommand(ShowView, null);
            _editServicesCommand = new DelegateCommand(EditServices, null);
            _addServiceCommand = new DelegateCommand(AddService, CanAdd);
            _deleteServiceCommand = new DelegateCommand(DeleteService, null);
            _dataSource = new ObservableCollection<DataGridViewModel>();
            _dataSource = FillServices();
            CreateSelectedService();
           

        }

       

        public int IdService {
            get { return _idService; }
        }

        public string NameService
        {
            get { return _nameService; }
            set
            {
                _nameService = value;
                NotifyPropertyChanged("NameService");
            }
        }

       

        public ObservableCollection<DataGridViewModel> DataSource
        {
            get
            {
                return _dataSource;
            }
            set { _dataSource = value; NotifyPropertyChanged("DataSource"); }
        }

        public float CostService {
            get { return _costService; }
            set
            {
                _costService = value;
                NotifyPropertyChanged("CostService");
            }
        }

       
        
        public DataGridViewModel SelectedService { get { return _selectedService; } set { _selectedService = value; NotifyPropertyChanged("SelectedService"); } }


        public bool AdminRights{
            get
            { 
            return Thread.CurrentPrincipal.IsInRole("Administrator") ? true : false;
            }
            
        }
        public bool PatientRights
        {
            get
            {
                return Thread.CurrentPrincipal.IsInRole("Pacient") ? true : false;
            }

        }
        public bool IsSelected {
            get {
                return SelectedService == null ? false : true;
            }
        }
        public ObservableCollection<String> SpecializationsCombo
        {
            get
            {
                return _specializations;
            }
            set
            {
                _specializations = value;
                NotifyPropertyChanged("SpecializationsCombo");
            }
        }




        public DelegateCommand ShowViewCommand { get { return _showViewCommand; } }
        private void ShowView(object obj)
        { 
            if (AdminRights)
            {
                AdminViewModel adminViewModel = new AdminViewModel();
                IView adminView = new AdminWindow(adminViewModel);
                adminViewModel.settoClose(adminView);
                adminView.Show();
                viewToClose.Close();

            }
            if (PatientRights)
            {

                PatientViewModel patientViewModel = new PatientViewModel();
                IView patientView = new PatientWindow(patientViewModel);
                patientViewModel.settoClose(patientView);
                patientView.Show();
                viewToClose.Close();
            }


        }

        public DelegateCommand EditServicesCommand { get { return _editServicesCommand; } }

      

        public void EditServices(object parameter)
        {
            if (SelectedService!= null) {
                using (var context = new MedicalDBEntities()) {
                  
                    Service serv = context.Services
                           .Where(s => s.idService == SelectedService.IdService)
                           .FirstOrDefault<Service>();

                    serv.nameService = SelectedService.NameService;
                    serv.cost = SelectedService.CostService;
                    serv.idSpecialization = GetSpecializationByName(SelectedService.NameSpecialization);
                    context.SaveChanges();
                    MessageBox.Show("Edit realizat cu succes");


                }
            }
        }

        public DelegateCommand AddServiceCommand { get { return _addServiceCommand; } }


        private bool CanAdd(object obj)
        {
            return true;
        }

        private void AddService(object parameter)
        {
            Service serv = new Service();
            serv.nameService = SelectedService.NameService;
            serv.cost = SelectedService.CostService;
            serv.idSpecialization = GetSpecializationByName(SelectedService.NameSpecialization);
            var context = new MedicalDBEntities();
            var services = context.Services;
            if (SelectedService != null)
            {
                services.Add(serv);
                context.SaveChanges();
                MessageBox.Show(serv.idSpecialization.ToString()+" "+serv.idService.ToString());

                ObservableCollection<DataGridViewModel> dataa = FillServices();

                _dataSource.Clear();
               foreach(DataGridViewModel x in dataa)
                {
                    _dataSource.Add(x);

                }
                

               
                
            }
            else MessageBox.Show("Null");
            
        }

        public DelegateCommand DeleteServiceCommand { get { return _deleteServiceCommand; } }
        private void DeleteService(object obj)
        {
            var context = new MedicalDBEntities();
            var services = context.Services;
            Service service =context.Services
                           .Where(s => s.idService == SelectedService.IdService)
                           .FirstOrDefault<Service>();


            services.Remove(service);
            context.SaveChanges();
            DataSource.Remove(SelectedService);
            CreateSelectedService();
            MessageBox.Show("Sters cu succes");
 }


        #region Methods

        private int GetSpecializationByName(string name)
        {
            var context = new MedicalDBEntities();

            foreach (Specialization s in context.Specializations)
            {
                if (name == s.nameSpecialization)
                    return s.idSpecialization;


            }
            return 0;
        }

        public void settoClose(IView close)
        {
            this.viewToClose = close;
        }

        public ObservableCollection<DataGridViewModel> FillServices()
        {
            using (MedicalDBEntities context = new MedicalDBEntities())
            {
                var query1=from i in context.Specializations select i;
                var query2 = from i in context.Services select i;
                ObservableCollection<Service> serv = new ObservableCollection<Service>(query2);
                ObservableCollection<Specialization> spec = new ObservableCollection<Specialization>(query1);
                

                DataGridViewModel data = new DataGridViewModel(serv, spec);
                _specializations = data.getStringSpecializations();

                NotifyPropertyChanged("DataSource");
                return data.FillDataGrid();
            }
           
        }

        public void CreateSelectedService() {


            DataGridViewModel data = new DataGridViewModel();

            SelectedService = data;
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
