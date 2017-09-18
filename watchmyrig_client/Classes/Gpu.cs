using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace watchmyrig_client
{
    public class Gpu
    {
        private string name;
        private int temp;

        public Gpu(string _name, int _temp)
        {
            this.name = _name;
            this.temp = _temp;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public int Temp
        {
            get { return this.temp; }
            set { this.temp = value; }
        }
    }
}
