using MaterialUI.MVVM.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MaterialUI.Helpers
{
    internal class ReadConfJSon
    {
        public static string GetConfiguration()
        {
            DeviceModel items;            
            try
            {
                using (StreamReader r = new StreamReader($"{Environment.CurrentDirectory}/DeviceConfiguration.json"))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<DeviceModel>(json);
                }

                return items.ComName == null || items.ComName == "" ? "" : items.ComName;
            }
            catch(FileNotFoundException ex)
            {
                DeviceModel d = new DeviceModel
                {
                    ComName = ""
                };
                using (StreamWriter file = File.CreateText(@"DeviceConfiguration.json"))
                {
                    JsonSerializer s = new JsonSerializer();
                    
                    s.Serialize(file, d);
                }
                return d.ComName;
            }
            
        }

        public static void SetConfiguration(string portName)
        {
            string json = File.ReadAllText("DeviceConfiguration.json");
            var device = new DeviceModel();
            JsonConvert.PopulateObject(json, device);

            device.ComName = portName;

            using(StreamWriter file = File.CreateText(@"DeviceConfiguration.json"))
            {
                JsonSerializer s = new JsonSerializer();
                s.Serialize(file, device);
            }
        }
    }
}
