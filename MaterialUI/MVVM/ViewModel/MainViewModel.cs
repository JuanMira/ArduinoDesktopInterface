using MaterialUI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialUI.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ConfigurationViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public ConfigurationViewModel ConfigurationVM { get; set; }

        private object? _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel() 
        {
            HomeVM = new HomeViewModel();
            ConfigurationVM = new ConfigurationViewModel();
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            ConfigurationViewCommand = new RelayCommand(o =>
            {
                CurrentView = ConfigurationVM;
            });
        }
    }
}
