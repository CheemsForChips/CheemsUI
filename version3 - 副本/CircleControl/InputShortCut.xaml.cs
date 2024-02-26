using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Panuon.WPF.UI;

namespace CircleControl
{
    /// <summary>
    /// InputShortCut.xaml 的交互逻辑
    /// </summary>
    public partial class InputShortCut : WindowX
    {
        public pluginButton pluginButton { get; set; }
        public delegate void SendMessage(string value, pluginButton pluginButton);//入口参数
        public SendMessage sendMessage;//返回值
        public InputShortCut()
        {

            InitializeComponent();
            if (pluginButton != null)
            {
                PathTextBox.Text = pluginButton.shortCutPath;
            }
        }
        private void shortCutPath(object sender, RoutedEventArgs e)
        {
            
            string textBoxString=PathTextBox.Text;
            if (textBoxString != null)
            {
                if(textBoxString == "")
                {
                    System.Windows.Forms.MessageBox.Show("输入的shortCut路径为空");
                }
                else
                {
                    string path = "start "+"\"快捷打开的窗口\" "+textBoxString;
                    //关闭窗口
                    sendMessage(path,pluginButton);
                    this.Close();
                }
                
            }
        }
    }
}
