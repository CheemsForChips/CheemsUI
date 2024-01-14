using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CircleControl
{
    class UtilityModel
    {
        /// <summary>
        /// 单例 确保仅有一个功能库
        /// </summary>
        private UtilityModel() { }
        private static UtilityModel UtilityModelInstance=new UtilityModel();
        public static UtilityModel GetUtilityModelInstance()
        {
            if (UtilityModelInstance==null)
            {
                UtilityModelInstance=new UtilityModel();
            }
            return UtilityModelInstance;
        }



        /// <summary>
        /// 功能函数定义
        /// </summary>
        public void ExitCircleControl()
        {
            Application.Current.Shutdown();
        }


        public  void ExecuteCommand(string command)
        {
            var processInfo = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = $"/C {command}",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            using (var process = new Process())
            {
                process.StartInfo = processInfo;
                process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                process.Start();
                process.BeginOutputReadLine();
                process.WaitForExit();
            }
        }
    }
}
