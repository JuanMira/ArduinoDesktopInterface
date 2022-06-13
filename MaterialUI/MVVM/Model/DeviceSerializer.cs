using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialUI.MVVM.Model
{
    internal class DeviceSerializer
    {
        public string type { get; set; }
        public bool deviceStatus { get; set; }
        public DataSerializer? data { get; set; }

        public override string ToString()
        {
            return $"type = {type}; DeviceStatus = {deviceStatus}";
        }
    }
}
