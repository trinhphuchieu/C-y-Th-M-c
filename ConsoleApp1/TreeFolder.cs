using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class TreeFolder
    {
        private string pNameFolder;
        private int pDeepth;
        private string nameFolder {
            get
            {
                return nameFolder;
            }
            set
            {
                nameFolder = value;
            }
        }
        private string deepth
        {
            get
            {
                return deepth;
            }
            set
            {
                deepth = value;
            }
        }

        public TreeFolder(string nameFolder, int deepth)
        {
            this.pNameFolder = nameFolder;
            pDeepth = deepth;
        }
        
        public override bool Equals(object obj)
        {
            return this.nameFolder.Equals(obj.ToString());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }



    }
}
