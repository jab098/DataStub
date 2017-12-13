using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using voa_ps_data_stub.Services.Interfaces;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace voa_ps_data_stub.Services
{
    public class DataAccessService : IDataAccessService
    {
        private IFileAccess _fileAccess;
        private IHostingEnvironment _hostEnv;
        public DataAccessService(IFileAccess fileAccess, IHostingEnvironment hostEnv)
        {
            _fileAccess = fileAccess;
            _hostEnv = hostEnv;
        }
        public string ReadFile(string path)
        {
            string fullPath = _hostEnv.ContentRootPath + "/Resources" + path;
            if (_fileAccess.Exists(fullPath))
                return _fileAccess.ReadAllText(fullPath);
            else
                throw new FileNotFoundException();
        }
    }
}
