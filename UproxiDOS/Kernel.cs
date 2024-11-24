using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.FileSystem;
using UproxiDOS.System;
using Sys = Cosmos.System;

namespace UproxiDOS
{
    public class Kernel : Sys.Kernel
    {
        public static string Version = "0.1.1b11.24.24";
        public static string Path = @"0:\";
        public static CosmosVFS fs;
        string[] bootFacts = { "This OS isn't a DOS! We just thought UproxiDOS sounded good :3", "This OS was originally named HawkTuOS!", "https://teamsillywillies.glungus.xyz btw" };

        protected override void BeforeRun()
        {
            Console.SetWindowSize(90, 30);
            Console.OutputEncoding = Cosmos.System.ExtendedASCII.CosmosEncodingProvider.Instance.GetEncoding(437);
            fs = new Cosmos.System.FileSystem.CosmosVFS();
            Cosmos.System.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Booting UproxiDOS " + Version);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Random rand = new Random();
            Console.WriteLine("Fun Fact! " + bootFacts[rand.Next(bootFacts.Length)]);
            Console.ForegroundColor = ConsoleColor.White;
        }

        protected override void Run()
        {
            Console.Write(Path + "> ");
            var command = Console.ReadLine();
            CommandHandler.RunCommand(command);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
