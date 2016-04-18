using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.AccessControl;

namespace Directory_File_Browsing.Models
{
    public class AppManager
    {
        List<string> less_10Mb = new List<string>();
        List<string> beetween_10Mb_50Mb = new List<string>();
        List<string> more_100Mb = new List<string>();
        #region New
        public string CurrentPath { get; set; }
        public string CurrentDisk { get; set; }
        public int Less_10Mb { get; set; }
        public int Beetween_10Mb_50Mb { get; set; }
        public int More_100Mb { get; set; }
        public Dictionary<string, bool> ListOfFolders { get; set; }
        public List<string> ListOfFile { get; set; }
        public Dictionary<string, bool> ListOfDisks { get; set; }
        public bool IsRoot { get; set; }

        
        public AppManager(string path)
        {
            CurrentPath = path;
            CurrentDisk = Directory.GetDirectoryRoot(path);
            ListOfFolders = new Dictionary<string, bool>();
            ListOfFile = new List<string>();
            ListOfDisks = new Dictionary<string, bool>();
            
            IsDirRoot(CurrentPath);
            GetNumberOfFiles(path);
            GetListOfFolders(path);
            GetListOfFiles(path);                               
            GetListOfDisks();
        }

        // Determine if we have access to disk
        bool IsDiskAccess(DriveInfo d)
        {
            bool access;           
                try
                {
                    Directory.GetDirectories(d.Name);
                    access = true;
                }
                catch(Exception)
                {
                    access = false;
                }                           
            return access;
        }

        void GetListOfDisks()
        {
            DriveInfo[] di = DriveInfo.GetDrives();
            foreach(var r in di)
            {
                ListOfDisks.Add(r.Name, IsDiskAccess(r));
            }
        }

        // Determine if we have access to folder
        bool IsFolderAccess(string path)
        {
            bool access;
            try
            {
                Directory.GetDirectories(path);
                access = true;
            }
            catch (Exception)
            {
                access = false;
            }
            return access;                    
        }

        void GetListOfFolders(string path)
        {                      
            foreach (var r in Directory.GetDirectories(path).ToList())
            {               
                DirectoryInfo dir = new DirectoryInfo(r);
                string s = dir.Name;
                if (s[0] == '$') { s = s.Insert(0, "_"); }
                ListOfFolders.Add(s, IsFolderAccess(dir.FullName));
            }          
        }

        void GetListOfFiles(string path)
        {        
            foreach (var r in Directory.GetFiles(path).ToList())
            {
                FileInfo file = new FileInfo(r);
                ListOfFile.Add(file.Name);
            }           
        }

        void GetSizeOfFile(string[] files)
        {            
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = new FileInfo(files[i]);
                string type = file.Length <= 10000000 ? "less" : (file.Length >= 10000000 && file.Length <= 50000000) ? "ok" : 
                                                                (file.Length >= 100000000) ? "more" : "";
                switch (type)
                {
                    case "less":
                        less_10Mb.Add(files[i]);
                        break;
                    case "ok":
                        beetween_10Mb_50Mb.Add(files[i]);
                        break;
                    case "more":
                        more_100Mb.Add(files[i]);
                        break;
                }               
            }
            Less_10Mb = less_10Mb.Count;
            Beetween_10Mb_50Mb = beetween_10Mb_50Mb.Count;
            More_100Mb = more_100Mb.Count;
        }       

        void GetNumberOfFiles(string path)
        {
            try
            {
                string[] p = Directory.GetDirectories(path);

                if (p.Length != 0)
                {
                    for (int i = 0; i < p.Length; i++)
                    {
                        GetNumberOfFiles(p[i]);                       
                    }                   
                }
                GetSizeOfFile(Directory.GetFiles(path));
            }
            catch (Exception)
            {
                return;
            }          
        }

        void IsDirRoot(string path)
        {
            if(CurrentDisk == path)
            {
                IsRoot = true;
            }
            else
            {
                IsRoot = false;
            }
        }
        #endregion
    }
}