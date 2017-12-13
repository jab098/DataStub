using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using voa_ps_data_stub.Helpers;
using Microsoft.VisualBasic;

namespace voa_ps_data_stub.Helpers
{

    public static class ContextHelper
    {
        public static URLData GetData(HttpContext context)
        {
            string urlPath = context.Request.Path;
            IQueryCollection queryData = context.Request.Query;

            string fileName = string.Empty;
            string folderPath = string.Empty;

            if (queryData.Count > 0)
            {
                fileName = queryData.First().Value + ".json";
                folderPath = urlPath;
            }
            else
            {
                if (urlPath.LastIndexOf('/') < 0)
                    throw new BadUrlFormatException("No file found in URL");

                fileName = urlPath.Substring(urlPath.LastIndexOf('/'));

                if (fileName.Length == 0)
                    throw new BadUrlFormatException("File name cannot be found");
                fileName = fileName + ".json";

                folderPath = urlPath.Substring(0, urlPath.LastIndexOf('/'));
                if (folderPath.Length == 0)
                    throw new BadUrlFormatException("Directory name cannot be found");
            }
            return new URLData(folderPath, fileName);
        }

        public class BadUrlFormatException : Exception
        {
            public string Message;
            public BadUrlFormatException(string message)
            {
                Message = message;
            }
        }
    }
}
