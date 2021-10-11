using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

using TaskListMVVM.Models;

namespace TaskListMVVM.FileServices
{
    public class JsonFileService : IFileService
    {
        List<Customer> IFileService.Open(string filename)
        {
            List<Customer> customers = new List<Customer>();
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(List<Customer>));

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                customers = jsonFormatter.ReadObject(fs) as List<Customer>;
            }
            return customers;
        }

        public void Save(string filename, List<Customer> customersList)
        {
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(List<Customer>));

            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, customersList);
            }
        }
    }
}