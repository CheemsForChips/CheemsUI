using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using Walterlv.WindowDetector;


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
        /// 命令行执行 调出任务管理器功能
        /// </summary>
        /// <param name="command"></param>
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

        /// <summary>
        /// 实现了窗口的置顶功能 并且能够toggle
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        public void ExecuteSetWindowTop()
        {
            // 获取所有打开的窗口
            var windows = WindowEnumerator.FindAll();
            // 获取第三个窗口 在这里要根据实际情况来微调一下 最好加上粉色的边框提示一下 窗口已经指定
            var exWindow = windows[1];

            ToggleWindowTopMost(exWindow.Hwnd);
        }

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOSIZE = 0x0001;

        public void ToggleWindowTopMost(IntPtr windowHandle)
        {
            IntPtr currentWindowInsertAfter = GetWindowInsertAfter(windowHandle);
            SetWindowPos(windowHandle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
        }

        private static IntPtr GetWindowInsertAfter(IntPtr windowHandle)
        {
            return GetWindowLongPtr(windowHandle, GWL_HWNDPREV);
        }

        private const int GWL_HWNDPREV = -8;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);


    }
}
