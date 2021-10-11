using System.Collections.Generic;

using TaskListMVVM.Models;

namespace TaskListMVVM.FileServices
{
    public interface IFileService
    {
        List<Customer> Open(string filename);
        void Save(string filename, List<Customer> customersList);
    }
}
