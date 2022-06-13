using MaterialUI.Core;
using MaterialUI.Helpers;
using MaterialUI.MVVM.Model;
using MaterialUI.MVVM.Model.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MaterialUI.MVVM.ViewModel
{
    internal class HomeViewModel : ObservableObject
    {
        private SerialPort io = new SerialPort();
        private DataDAO _dataDao;
        private ExcelGenerator _excelGenerator;
        private bool open = false;
        public HomeViewModel()
        {
            _dataDao = new DataDAO();
            _excelGenerator = new ExcelGenerator();

            if (ReadConfJSon.GetConfiguration() != "")
                _portSelected = ReadConfJSon.GetConfiguration();

            _initialDate = DateTime.Now;
            _finalDate = DateTime.Now;

            io.BaudRate = 9600;

            if (!_portSelected.Equals(""))
            {
                io.PortName = _portSelected;
                InitArduino();
            }
                

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
            if (_portSelected.Equals(""))
            {
                ReadConfJSon.SetConfiguration(_portSelected);

            }
            else
            {
                if (!_portSelected.Equals("") && !open)
                    io.PortName = _portSelected;
                try
                {                    
                    if(!open)
                        InitArduino();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            
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
            BackgroundWorker bw = new BackgroundWorker();
            if (Data.Count > 0)
            {
                bw.DoWork += DoWorkFunction;                
                bw.RunWorkerAsync();
            }
        }

        private void DoWorkFunction(object sender, DoWorkEventArgs e)
        {            
            _excelGenerator.ListToExcel(Data.ToList());
        }
        #endregion

        #region DataFromArduino
        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string inData = io.ReadLine();
            // deserialization of the data
            var inDataDes = JsonSerializer.Deserialize<DeviceSerializer>(inData);
            if(inDataDes.deviceStatus)
            {
                Debug.WriteLine(inDataDes.type);
                if(inDataDes.data != null)
                {
                    _dataDao.InsertData(inDataDes.data);
                }
            }
                        
        }

        private void InitArduino() {
            open = true;
            
            try
            {
                io.Open();
                io.Write("connect");
                io.DataReceived += new SerialDataReceivedEventHandler(SerialPortDataReceived);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                if (!io.IsOpen)
                {
                    io.Close();
                }
            }
        }
        #endregion
    }
}
