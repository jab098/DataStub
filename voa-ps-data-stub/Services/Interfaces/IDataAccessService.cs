using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace voa_ps_data_stub.Services.Interfaces
{
    public interface IDataAccessService
    {
        string ReadFile(string path);
    }
}
