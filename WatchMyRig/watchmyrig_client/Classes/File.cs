using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace watchmyrig_client
{
    [Serializable]
    public class File
    {
        private string filename;
        private string path;
        private string startupPath;

        public File(string _fileName, string _path, string _startupPath)
        {
            filename = _fileName;
            path = _path;
            startupPath = _startupPath;
        }

        public string FileName
        {
            get
            {
                return this.filename;
            }
            set
            {
                this.filename = value;
            }
        }

        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
            }
        }

        public string StartupPath
        {
            get
            {
                return this.startupPath;
            }
            set
            {
                this.startupPath = value;
            }
        }
    }
}
