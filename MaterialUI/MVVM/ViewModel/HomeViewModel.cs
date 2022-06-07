using MaterialUI.Core;
using MaterialUI.Helpers;
using MaterialUI.MVVM.Model;
using MaterialUI.MVVM.Model.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MaterialUI.MVVM.ViewModel
{
    internal class HomeViewModel : ObservableObject
    {
        private SerialPort io = new SerialPort();
        private DataDAO _dataDao;
        private ExcelGenerator _excelGenerator;
        public HomeViewModel()
        {
            _dataDao = new DataDAO();
            _excelGenerator = new ExcelGenerator();

            if (ReadConfJSon.GetConfiguration() != "")
                _portSelected = ReadConfJSon.GetConfiguration();

            _initialDate = DateTime.Now;
            _finalDate = DateTime.Now;
            Data = new ObservableCollection<DataModel>();
        }

        #region Query
        private DateTime _initialDate { get; set; }
        public DateTime InitialDate
        {
            get
            {
                return _initialDate;
            }

            set
            {
                _initialDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime _finalDate { get; set; }
        public DateTime FinalDate
        {
            get
            {
                return _finalDate;
            }

            set
            {
                _finalDate = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DataModel> Data { get; }

        #endregion

        #region Ports
        public string[] Ports {
            get {
                return SerialPort.GetPortNames();   
            }
            set
            {                
                OnPropertyChanged();
            }
        }

        private string? _portSelected { get; set; }
        public string? PortSelected
        {
            get
            {
                return _portSelected;
            }
            set
            {
                _portSelected = value;
                Debug.WriteLine(value);
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands
        public ICommand RefreshPortComCommand
        {
            get
            {
                return new RelayCommand(o =>
                {
                    GetPortComs();
                });
            }
        }

        public ICommand ConnectPortCommand
        {
            get
            {
                return new RelayCommand(o =>
                {
                    ConnectPort();
                });
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(o =>
                {
                    Search();
                });
            }
        }

        public ICommand GenerateExcelCommand
        {
            get
            {
                return new RelayCommand(o =>
                {
                    GenerateExcel();
                });
            }
        }

        private void GetPortComs()
        {
            Ports = SerialPort.GetPortNames();                      
        }

        private void ConnectPort()
        {
            ReadConfJSon.SetConfiguration(_portSelected);            
        }

        private void Search()
        {
            var d = _dataDao.GetDataByDates(InitialDate, FinalDate);
            if(d.Count == 0)
            {
                Data.Clear();
            }else
                foreach (var d2 in d)
                    Data.Add(d2);
            OnPropertyChanged();
        }

        private void GenerateExcel()
        {
            if(Data.Count > 0)
                _excelGenerator.ListToExcel(Data.ToList());
        }
        

        #endregion
    }
}
