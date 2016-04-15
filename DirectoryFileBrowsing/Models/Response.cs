using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Directory_File_Browsing.Models
{
    public class Response
    {
        public string Path { get; set; }
        public string Disk { get; set; }
        public Dictionary<string, bool> Folders { get; set; }
        public List<string> Files { get; set; }
        public int Less_10Mb { get; set; }
        public int More_100Mb { get; set; }
        public int Beetween_10Mb_50Mb { get; set; }
        public Dictionary<string, bool> Disks { get; set; }
        public bool IsRoot { get; set; }
    }
}