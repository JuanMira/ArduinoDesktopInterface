using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialUI.MVVM.Model.Repository
{
    internal class DataDAO
    {
        private readonly DatabaseContext _context;
        private readonly List<DataModel> _dataTest;

        public DataDAO()
        {
            _context = new DatabaseContext();
            _dataTest = new List<DataModel>
            {
                new DataModel {Id = 1, Data1 = "Test", Data2 = "Test" , Data3 = "Test" , Data4 = "Test" , Data5 = "Test" , Data6 = "Test", DateCreate = DateTime.UtcNow},
                new DataModel {Id = 2, Data1 = "Test", Data2 = "Test" , Data3 = "Test" , Data4 = "Test" , Data5 = "Test" , Data6 = "Test", DateCreate = DateTime.UtcNow},
                new DataModel {Id = 3, Data1 = "Test", Data2 = "Test" , Data3 = "Test" , Data4 = "Test" , Data5 = "Test" , Data6 = "Test", DateCreate = DateTime.UtcNow}
            };
        }

        public ObservableCollection<DataModel> GetDataByDates(DateTime initialDate, DateTime finalDate)
        {
            //var data = _context.Data
            //    .Where(x => x.DateCreate > initialDate && x.DateCreate < finalDate)
            //    .ToList();

            //return data;
            ObservableCollection<DataModel> _d = new ObservableCollection<DataModel>();
            var data = _dataTest.Where(x => x.DateCreate >= initialDate && x.DateCreate <= finalDate);
            foreach (var d in data)
                _d.Add(d);
            return _d;
        }


    }
}
