using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Directory_File_Browsing.Models;
using System.Diagnostics;
using System.IO;

namespace Directory_File_Browsing.Controllers
{
    public class ValueController : ApiController
    {
        public Response GetStartPage()
        {
            string rootPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            rootPath = rootPath.Remove(rootPath.Length - 1);

            AppManager model = new AppManager(rootPath);

            Response respond = new Response()
            {
                Folders = model.ListOfFolders,
                Path = model.CurrentPath,
                Disk = model.CurrentDisk,
                Disks = model.ListOfDisks,
                Files = model.ListOfFile,
                Less_10Mb = model.Less_10Mb,
                Beetween_10Mb_50Mb = model.Beetween_10Mb_50Mb,
                More_100Mb = model.More_100Mb,
                IsRoot = model.IsRoot
            };
            return respond;
        }

        public Response GetFolders(string path, string dir)
        {
            Response respond;
            if (dir == "...")
            {
                DirectoryInfo s = Directory.GetParent(path);
                AppManager model = new AppManager(s.FullName);

                respond = new Response()
                {
                    Folders = model.ListOfFolders,
                    Path = model.CurrentPath,
                    Disk = model.CurrentDisk,
                    Disks = model.ListOfDisks,
                    Files = model.ListOfFile,
                    Less_10Mb = model.Less_10Mb,
                    Beetween_10Mb_50Mb = model.Beetween_10Mb_50Mb,
                    More_100Mb = model.More_100Mb,
                    IsRoot = model.IsRoot
                };               
            }
            else
            {
                string d = dir;
                if (d[0] == '_') d = d.Remove(0,1); 
               
                AppManager model = new AppManager(path + "\\" + d);               

                respond = new Response()
                {
                    Folders = model.ListOfFolders,
                    Path = model.CurrentPath,
                    Disk = model.CurrentDisk,
                    Disks = model.ListOfDisks,
                    Files = model.ListOfFile,
                    Less_10Mb = model.Less_10Mb,
                    Beetween_10Mb_50Mb = model.Beetween_10Mb_50Mb,
                    More_100Mb = model.More_100Mb,
                    IsRoot = model.IsRoot
                };

            }          
            return respond;
        }

        public Response GetFoldersOfDisk(string disk)
        {
            AppManager model = new AppManager(disk);

            Response respond = new Response()
            {
                Folders = model.ListOfFolders,
                Path = model.CurrentPath,
                Disk = model.CurrentDisk,
                Disks = model.ListOfDisks,
                Files = model.ListOfFile,
                Less_10Mb = model.Less_10Mb,
                Beetween_10Mb_50Mb = model.Beetween_10Mb_50Mb,
                More_100Mb = model.More_100Mb,
                IsRoot = model.IsRoot
            };

            return respond;
            
        }
    }
}
