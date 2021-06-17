using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Interfaces
{
    public interface IEmployeeImportService
    {
        Task<List<string>> Import(Stream stream);
    }
}
