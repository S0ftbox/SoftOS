using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Sys = Cosmos.System;

namespace SoftOS.Commands
{
    public class File : Command
    {
        public File(string name) : base(name) { }

        public override string Execute(string[] args)
        {
            string response = "";

            switch(args[0])
            {
                case "createfile":
                    try
                    {
                        Sys.FileSystem.VFS.VFSManager.CreateFile(args[1]);
                        response = "The file " + args[1] + " was created";
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                    }
                    break;
                case "deletefile":
                    try
                    {
                        Sys.FileSystem.VFS.VFSManager.DeleteFile(args[1]);
                        response = "The file " + args[1] + " was deleted";
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                    }
                    break;
                case "createdir":
                    try
                    {
                        Sys.FileSystem.VFS.VFSManager.CreateDirectory(args[1]);
                        response = "The directory " + args[1] + " was created";
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                    }
                    break;
                case "deletedir":
                    try
                    {
                        Sys.FileSystem.VFS.VFSManager.DeleteDirectory(args[1], true);
                        response = "The directory " + args[1] + " was deleted";
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                    }
                    break;
                case "editfile":
                    try
                    {
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(args[1]).GetFileStream();

                        if (fs.CanWrite)
                        {
                            int ctr = 0;
                            StringBuilder sb = new StringBuilder();
                            foreach(string s in args)
                            {
                                if(ctr>1)
                                    sb.Append(s+ ' ');
                                ++ctr;
                            }
                            string txt = sb.ToString();
                            Byte[] data = Encoding.ASCII.GetBytes(txt.Substring(0,txt.Length-1));
                            fs.Write(data,0, data.Length);
                            fs.Close();
                            response = "The file " + args[1] + " was edited";
                        }
                        else
                        {
                            response = "Unable to write to the file.";
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                    }
                    break;
                case "readfile":
                    try
                    {
                        FileStream fs = (FileStream)Sys.FileSystem.VFS.VFSManager.GetFile(args[1]).GetFileStream();

                        if (fs.CanRead)
                        {
                            Byte[] data = new Byte[256];
                            fs.Read(data, 0, data.Length);
                            response= Encoding.ASCII.GetString(data);
                            fs.Close();
                        }
                        else
                        {
                            response = "Unable to read from the file.";
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        response = ex.ToString();
                    }
                    break;
                case "help":
                    response = "createfile - create the file \ndeletefile - delete the file \ncreatedir - create the directory \ndeletedir - delete the directory \neditfile - edit the file \nreadfile - read the file \nhelp - show help";
                    break;
                default:
                    response = "The argument is incorrect";
                    break;
            }

            return response;
        }
    }
}
