using PluginBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SetOnTop
{
    public class Start : IDev
    {
        public string PluginName => "SetOnTop";

        public string PluginDescription => "选中窗口始终置顶";

        public bool isPluginToggle => true;

        string IDev.PluginName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IDev.PluginDescription { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool IDev.isPluginToggle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string IDev.Execute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }





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

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOSIZE = 0x0001;

        public void ToggleWindowTopMost(IntPtr windowHandle)
        {
            IntPtr currentWindowInsertAfter = GetWindowInsertAfter(windowHandle);

            if (currentWindowInsertAfter == HWND_NOTOPMOST)
            {
                SetWindowPos(windowHandle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
            }
            else
            {
                SetWindowPos(windowHandle, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
            }
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
