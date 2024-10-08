﻿using System;
using Sys = Cosmos.System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace SoftOS.System
{
    public static class ConsoleCommands
    {
        public static void RunCommand(string command)
        {
            string[] words = command.Split(' ');
            if(words.Length > 0 )
            {
                if (words[0] == "info" ||  words[0] == "help")
                {
                    Console.WriteLine("SoftOS " + Kernel.Version);
                }
                else if (words[0] == "format")
                {
                    if (Kernel.fs.Disks[0].Partitions.Count > 0)
                    {
                        Kernel.fs.Disks[0].DeletePartition(0);
                    }
                    Kernel.fs.Disks[0].Clear();
                    Kernel.fs.Disks[0].CreatePartition((int)(Kernel.fs.Disks[0].Size / (1024 * 1024)));
                    Kernel.fs.Disks[0].FormatPartition(0, "FAT32", true);
                    WriteMessage.WriteInfo("Format complete");
                    WriteMessage.WriteWarning("System will now reboot");
                    Thread.Sleep(3000);
                    Sys.Power.Reboot();
                }
                else if(words[0] == "space")
                {
                    long freeMem = Kernel.fs.GetAvailableFreeSpace(Kernel.Path);
                    Console.WriteLine("Free space: " + freeMem / 1024 + "kB");
                }
                else if (words[0] == "dir")
                {
                    var directories = Directory.GetDirectories(Kernel.Path);
                    var files = Directory.GetFiles(Kernel.Path);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Directories (" + directories.Length + ")");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    for (int i = 0; i < directories.Length; i++)
                    {
                        Console.WriteLine(directories[i]);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Files (" + files.Length + ")");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    for (int i = 0; i < files.Length; i++)
                    {
                        Console.WriteLine(files[i]);
                    }
                }
                else if(words[0] == "echo")
                {
                    if(words.Length > 1)
                    {
                        string wholeString = "";
                        for (int i = 1; i < words.Length; i++)
                        {
                            wholeString += words[i] + " ";
                        }
                        int pathIndex = wholeString.LastIndexOf('>');
                        string text = wholeString.Substring(0, pathIndex);
                        string path = wholeString.Substring(pathIndex + 1);
                        if (!path.Contains(@"\"))
                        {
                            path = Kernel.Path + path;
                        }
                        if(path.EndsWith(' '))
                        {
                            path = path.Substring(0, path.Length - 1);
                        }
                        var fileStream = File.Create(path);
                        fileStream.Close();
                        File.WriteAllText(path, text);

                    }
                    else 
                    {
                        WriteMessage.WriteError("Invalid syntax");
                    }
                }
                else if(words[0] == "cat")
                {
                    if(words.Length > 1)
                    {
                        string path = words[1];
                        if (!path.Contains(@"\"))
                        {
                            path = Kernel.Path + path;
                        }
                        if (path.EndsWith(' '))
                        {
                            path = path.Substring(0, path.Length - 1);
                        }
                        if (File.Exists(path))
                        {
                            string text = File.ReadAllText(path);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine(text);
                        }
                        else
                        {
                            WriteMessage.WriteError("File " + path + " not found");
                        }
                    }
                    else
                    {
                        WriteMessage.WriteError("Invalid syntax");
                    }
                }
                else if (words[0] == "del")
                {
                    if(words.Length > 1)
                    {
                        string path = words[1];
                        if (!path.Contains(@"\"))
                        {
                            path = Kernel.Path + path;
                        }
                        if (path.EndsWith(' '))
                        {
                            path = path.Substring(0, path.Length - 1);
                        }
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                            WriteMessage.WriteInfo("Deleted " + path);
                        }
                        else
                        {
                            WriteMessage.WriteError("File " + path + " not found");
                        }
                    }
                    else
                    {
                        WriteMessage.WriteError("Invalid syntax");
                    }
                }
                else if (words[0] == "mkdir")
                {
                    if (words.Length > 1)
                    {
                        string path = words[1];
                        if (!path.Contains(@"\"))
                        {
                            path = Kernel.Path + path;
                        }
                        if (path.EndsWith(' '))
                        {
                            path = path.Substring(0, path.Length - 1);
                        }
                        Directory.CreateDirectory(path);
                    }
                    else
                    {
                        WriteMessage.WriteError("Invalid syntax");
                    }
                }
                else if (words[0] == "cd")
                {
                    if (words.Length > 1)
                    {
                        if (words[1] == "..")
                        {
                            if(Kernel.Path != @"0:\")
                            {
                                string tmpPath = Kernel.Path.Substring(0, Kernel.Path.Length - 1);
                                Kernel.Path = tmpPath.Substring(0, tmpPath.LastIndexOf(@"\"));
                                return;
                            }
                            else
                            {
                                return;
                            }
                        }
                        string path = words[1];
                        if (!path.Contains(@"\"))
                        {
                            path = Kernel.Path + path + @"\";
                        }
                        if (path.EndsWith(' '))
                        {
                            path = path.Substring(0, path.Length - 1);
                        }
                        if(!path.EndsWith(@"\"))
                        {
                            path += @"\";
                        }
                        if (Directory.Exists(path))
                        {
                            Kernel.Path = path;
                            //Directory.CreateDirectory(path);
                        }
                        else
                        {
                            WriteMessage.WriteError("Directory " + path + " not found");
                        }
                    }
                    else
                    {
                        Kernel.Path = @"0:\";
                    }
                }
                else if (words[0] == "removedir")
                {
                    if (words.Length > 1)
                    {
                        string path = words[1];
                        if (!path.Contains(@"\"))
                        {
                            path = Kernel.Path + path + @"\";
                        }
                        if (path.EndsWith(' '))
                        {
                            path = path.Substring(0, path.Length - 1);
                        }
                        if (!path.EndsWith(@"\"))
                        {
                            path += @"\";
                        }
                        if (Directory.Exists(path))
                        {
                            Directory.Delete(path, true);
                        }
                        else
                        {
                            WriteMessage.WriteError("Directory " + path + " not found");
                        }
                    }
                }
                else if (words[0] == "gui")
                {
                    Boot.OnBoot();
                }
            }
            else
            {
                WriteMessage.WriteError("Invalid command!");
            }
        }
    }
}
