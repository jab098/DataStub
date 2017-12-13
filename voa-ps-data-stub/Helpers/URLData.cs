using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace voa_ps_data_stub.Helpers
{
    public class URLData
    {
        public URLData(string directory, string fileName)
        {
            Directory = directory;
            FileName = fileName;
        }

        private string directory;
        public string Directory { get { return directory; } set { directory = value; } }

        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set
            {
                string sanitizedVal = value.Replace("/", string.Empty);
                fileName = sanitizedVal;
            }
        }

        public string Path
        {
            get
            {
                string middleSlash = string.Empty;
                if ((directory[directory.Length - 1]) != '/')
                    middleSlash = "/";

                return directory + middleSlash + fileName;
            }
        }

    }
}
