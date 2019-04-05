using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SqlAutoBackup
{
    class Program
    {

        public static string[] Args = new string[255];

        private System.Timers.Timer aTimer;

        public string Interval;
        public string PathSrc;

        public Program(string interval, string src)
        {
            Interval = interval;
            PathSrc = src;
            StartTimer();
            while (true)
            {
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(2000);
                    Console.Write(".");
                }
            }
        }

        static void Main(string[] args)
        {

            string interval = "720"; // Intervallo in minuti
            string src = "";
            string dest = "";
            string user = "Admin";
            int reservedIndex = -1;
            int reservedIndex2 = -1;
            int reservedIndex3 = -1;
            int reservedIndex4 = -1;
            int reservedIndex5 = -1;
            int reservedIndex6 = -1;
            int reservedIndex7 = -1;
            int reservedIndex8 = -1;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--interval")
                {
                    reservedIndex = i;
                    reservedIndex2 = i + 1;
                    interval = args[i + 1];
                }

                if (args[i] == "--src")
                {
                    reservedIndex3 = i;
                    reservedIndex4 = i + 1;
                    src = args[i + 1];
                }
            }

            int argsCount = 0;
            for(int i = 0; i < args.Length; i++)
            {
                if (i != reservedIndex && i != reservedIndex2 &&
                    i != reservedIndex3 && i != reservedIndex4)
                {
                    Args[argsCount] = args[i];
                    argsCount++;
                }
            }

            var main = new Program(interval, src);

        }

        public void StartTimer()
        {
            Console.WriteLine("Interval: " + Interval);
            Console.WriteLine("Seting Timer");
            int intervalLong = int.Parse(Interval);
            aTimer = new System.Timers.Timer(intervalLong * 60 * 1000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            ExecuteApplication(Args);
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            new Task(() => { ExecuteApplication(Args); }).Start();
        }

        async Task ExecuteApplication(string[] args)
        {
            /*var startInfo = new ProcessStartInfo(PathgSrc);
            string arg = " -u " + User + " ";
            for(int i = 0; i < ArgsCount; i++)
            {
                arg += Args[i] + " ";
            }
            arg += "> " + PathDest;
            arg = "C:\xampp\mysql\bin\mysqldump.exe -u pma assetcopier > c:\backups\assetcopier.sql";
            Console.WriteLine("Executing backup: " + arg);
            startInfo.Arguments = arg;
            startInfo.UseShellExecute = false;
            Process.Start(startInfo);*/

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = PathSrc;
            process.StartInfo = startInfo;
            process.Start();


            Console.Clear();
            Console.Write("Executing script: " + PathSrc);
        }
    }
}
