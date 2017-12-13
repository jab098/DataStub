using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using voa_ps_data_stub.Services.Interfaces;
using System.IO;

namespace voa_ps_data_stub.Services
{
    public class FileAccess : IFileAccess
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
