using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Cosmos.Debug.Kernel.Plugs.Asm;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace UproxiDOS.System
{
    public static class CommandHandler
    {
        public static void RunCommand(string command)
        {
            string[] words = command.Split(' ');
            if (words.Length > 0)
            {
                if (words[0] == "test")
                {
                    if (words.Length == 1)
                    {
                        Console.WriteLine("test cmd, run test --help for subcommands.");
                    }
                    else
                    {
                        if (words[1] == "error")
                        {
                            ConsoleMsg.Error("test err");
                        }
                        else if (words[1] == "warn")
                        {
                            ConsoleMsg.Warn("test warn");
                        }
                        else if (words[1] == "info")
                        {
                            ConsoleMsg.Info("roxi is awesome!");
                        }
                        else if (words[1] == "--help")
                        {
                            Console.WriteLine("test Command Help\n\ntest error - tests the error function\ntest warn - tests the warn function\ntest info - tests the info function");
                        }
                        else if (words[1] == "file")
                        {
                            var file_stream = File.Create(Kernel.Path + "testing.txt");
                            Directory.CreateDirectory(Kernel.Path + @"testdirectory\");
                        }
                        else
                        {
                            ConsoleMsg.Error("Invalid Subcommand!");
                        }
                    }
                }
                else if (words[0] == "info")
                {
                    if (words.Length == 1)
                    {
                        Console.WriteLine(ConsoleMsg.CenterText("UproxiDOS"));
                        Console.WriteLine(ConsoleMsg.CenterText(Kernel.Version));
                        Console.WriteLine("");
                        Console.WriteLine(ConsoleMsg.CenterText("Built on CosmosOS"));
                        Console.WriteLine(ConsoleMsg.CenterText("https://www.gocosmos.org/"));
                        Console.WriteLine("");
                        Console.WriteLine(ConsoleMsg.CenterText("Made with Crack by Team Silly Willies"));
                        Console.WriteLine(ConsoleMsg.CenterText("https://teamsillywillies.glungus.xyz/"));
                    }
                    else
                    {
                        if (words[1] == "system")
                        {
                            uint totalRam = Cosmos.Core.CPU.GetAmountOfRAM();
                            ulong freeRam = Cosmos.Core.GCImplementation.GetAvailableRAM();
                            string cpuName = Cosmos.Core.CPU.GetCPUBrandString();
                            Console.WriteLine(ConsoleMsg.CenterText("System Information"));
                            Console.WriteLine("");
                            Console.WriteLine(ConsoleMsg.CenterText("RAM: " + freeRam + "/" + totalRam + " MB"));
                            Console.WriteLine("");
                            Console.WriteLine(ConsoleMsg.CenterText("CPU: " + cpuName));
                        }
                        else if (words[1] == "--help")
                        {
                            Console.WriteLine("info Command Help\n\ninfo - Displays OS Info\ninfo system - Displays System Info");
                        }
                    }
                }
                else if (words[0] == "clear" || words[0] == "clr")
                {
                    Console.Clear();
                }
                else if (words[0] == "help")
                {
                    Console.WriteLine("System Commands\n\ntest\ninfo\nclear\nclr\nhelp");
                }
                else if (words[0] == "echo")
                {
                    if (words.Length > 1)
                    {
                        Console.WriteLine(string.Join(" ", words.Skip(1)));
                    }
                }
                else if (words[0] == "space")
                {
                    long free = Kernel.fs.GetAvailableFreeSpace(Kernel.Path);
                    Console.WriteLine("Free space: " + free / 1024 + "KB");
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
                    ConsoleMsg.Info("Success!");
                    ConsoleMsg.Warn("UproxiDOS will reboot in 3 seconds!");
                    Thread.Sleep(3000);
                    Cosmos.System.Power.Reboot();
                }
                else if (words[0] == "mkdir")
                {
                    if (words.Length > 1)
                    {
                        string path = words[1];
                        if (!path.EndsWith(@"\"))
                        {
                            path = path + @"\";
                        }

                        path = Kernel.Path + path;
                        ConsoleMsg.Info($"{path}");

                        try
                        {
                            Directory.CreateDirectory(path);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("tuahhh");
                    }
                }
                else if (words[0] == "cd")
                {
                    if (words.Length > 1)
                    {
                        if (words[1] == "..")
                        {
                            if (Kernel.Path != @"0:\")
                            {
                                string tempPath = Kernel.Path.Substring(0, Kernel.Path.Length - 1);
                                Kernel.Path = tempPath.Substring(0, tempPath.LastIndexOf(@"\") + 1);
                            }
                        }
                        else
                        {
                            string path = words[1];

                            if (!path.EndsWith(@"\"))
                            {
                                path = path + @"\";
                            }

                            path = Kernel.Path + path;

                            if (Directory.Exists(path))
                            {
                                Kernel.Path = path;
                            } else
                            {
                                ConsoleMsg.Error("Path does not exist!");
                            }
                        }
                    }
                    else
                    {
                        Kernel.Path = @"0:\";
                    }
                } 
            else if (words[0] == "dir")
            {
                    string path = Kernel.Path;

                    var files_list = Directory.GetFiles(path);
                    var directory_list = Directory.GetDirectories(path);

                    foreach (var directory in directory_list)
                    {
                        Console.WriteLine("[ DIR ] " + directory);
                    }

                    foreach (var file in files_list)
                    {
                        //var content = File.ReadAllText(file);

                        Console.WriteLine("[ FILE ] " + file);
                    }
                    Console.WriteLine("Directory of " + Kernel.Path);
                }
            }
            else if (words[0] == "write")
            {
                if (words.Length > 1)
                {

                }
            }
            else
                {
                    ConsoleMsg.Error("Please enter a Valid Command.");
                }
            }
        }
    }
