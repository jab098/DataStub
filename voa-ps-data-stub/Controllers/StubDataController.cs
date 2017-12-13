using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using voa_ps_data_stub.Services.Interfaces;
using voa_ps_data_stub.Helpers;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using static voa_ps_data_stub.Helpers.ContextHelper;
using Microsoft.AspNetCore.Hosting;

namespace voa_ps_data_stub.Controllers
{

    public class StubDataController : Controller
    {
        IDataAccessService _dataAccess;
        HttpContext _context;
        IHostingEnvironment _hostEnv;

        
        public StubDataController(IDataAccessService dataAccess, IHttpContextAccessor contextAccessor, IHostingEnvironment hostEnv)
        {
            _dataAccess = dataAccess;
            _context = contextAccessor.HttpContext;
            _hostEnv = hostEnv;
        }

        [HttpGet]
        public IActionResult Get()
        {
            URLData pathData;
            string fileData;

            try
            {
                pathData = ContextHelper.GetData(_context);
                fileData = _dataAccess.ReadFile(pathData.Path);
            }
            catch (BadUrlFormatException)
            {
                return new NotFoundResult();
            }
            catch (FileNotFoundException)
            {
                return new NotFoundResult();
            }

            return Content(fileData, "application/json");
        }

        [HttpPost]
        public IActionResult Post()
        {
            URLData pathData;
            string fileData;

            try
            {

                pathData = ContextHelper.GetData(_context);

                var path = pathData.Path;
                var postPath = path.Substring(5, path.Length-5);

                fileData = _dataAccess.ReadFile(postPath);
            }
            catch (BadUrlFormatException)
            {
                return new NotFoundResult();
            }
            catch (FileNotFoundException)
            {
                return new NotFoundResult();
            }

            var result = JsonConvert.DeserializeObject(fileData);
            return new OkObjectResult(result);
        }
    }
}
