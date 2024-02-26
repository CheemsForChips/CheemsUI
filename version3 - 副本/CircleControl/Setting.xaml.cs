using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Panuon.WPF.UI;
using PluginBase;
using System.Configuration;
using GongSolutions.Wpf.DragDrop;

namespace CircleControl
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : WindowX
    {
        public delegate void SendMessage(ObservableCollection<pluginButton> pluginButton);
        public SendMessage sendMessage;
        public Setting()
        {
            InitializeComponent();
        }
        //用来给轮盘提供插件列表
        public ObservableCollection<pluginButton> pluginButtonListBox = new ObservableCollection<pluginButton>();

        private void settingWindowLoad(object sender, RoutedEventArgs e)
        {
            //初始化插件列表
            initializeListBox();
            //初始化左侧的markdown文本信息
            utilityViewMarkdown.Markdown=File.ReadAllText(@"./Resources/Document/utilityViewMarkdown.md");
            informationViewMarkDown.Markdown = File.ReadAllText(@"./Resources/Document/About.md");
        }
        private void initializeListBox()
        {
            //初始化插件列表
            mListBox.Items.Clear();
            pluginButtonListBox.Add(new pluginButton { pluginName = "一键休眠", pluginDescription = "选中该区块即可使电脑进入休眠模式", headerIcon = "\ue677" ,shortCutPath="shutdown -h"});
            //pluginButtonListBox.Add(new pluginButton { pluginName = "划词翻译", pluginDescription = "选词翻译", headerIcon = "\ue68a" });
            pluginButtonListBox.Add(new pluginButton { pluginName = "任务管理器", pluginDescription = "打开任务管理器", headerIcon = "\ue695" ,shortCutPath= "taskmgr" });
            pluginButtonListBox.Add(new pluginButton { pluginName = "窗口置顶", pluginDescription = "将窗口选中的窗口始终置顶", headerIcon = "\ue6a3" });
            //pluginButtonListBox.Add(new pluginButton { pluginName = "浏览器搜索", pluginDescription = "选中词句之后，调用该区块功能实现快捷浏览器搜索", headerIcon = "\ue798" });
            pluginButtonListBox.Add(new pluginButton { pluginName = "文件夹打开", pluginDescription = "点击该区块即可打开文件夹", headerIcon = "\ue610" ,shortCutPath="explorer Desktop"});
            pluginButtonListBox.Add(new pluginButton { pluginName = " 计算器", pluginDescription = " 计算器", headerIcon = "\ue79b" ,shortCutPath="calc"});
            pluginButtonListBox.Add(new pluginButton { pluginName = "shortCut0", pluginDescription = "输入快捷方式的路径", headerIcon = "\ue60a" });
            pluginButtonListBox.Add(new pluginButton { pluginName = "shortCut1", pluginDescription = "输入快捷方式的路径", headerIcon = "\ue60a" });
            pluginButtonListBox.Add(new pluginButton { pluginName = "shortCut2", pluginDescription = "输入快捷方式的路径", headerIcon = "\ue60a" });
            pluginButtonListBox.Add(new pluginButton { pluginName = "shortCut3", pluginDescription = "输入快捷方式的路径", headerIcon = "\ue60a" });
            pluginButtonListBox.Add(new pluginButton { pluginName = "shortCut4", pluginDescription = "输入快捷方式的路径", headerIcon = "\ue60a" });
            pluginButtonListBox.Add(new pluginButton { pluginName = "shortCut5", pluginDescription = "输入快捷方式的路径", headerIcon = "\ue60a" });
            pluginButtonListBox.Add(new pluginButton { pluginName = "shortCut6", pluginDescription = "输入快捷方式的路径", headerIcon = "\ue60a" });
            pluginButtonListBox.Add(new pluginButton { pluginName = "shortCut7", pluginDescription = "输入快捷方式的路径", headerIcon = "\ue60a" });
            mListBox.ItemsSource = pluginButtonListBox;
        }

        private void listboxButtonClick(object sender, RoutedEventArgs e)
        {
            //将按钮的内容传递给右侧的文本框
            var itemButton = sender as System.Windows.Controls.Button;
            foreach(var obj in pluginButtonListBox)
            {
                if (itemButton.Content == obj.pluginName)
                {
                    showPluginUtility.Text = obj.pluginDescription+" path:"+obj.shortCutPath;
                }
            }
        }
        private void shortCutClick(object sender, RoutedEventArgs e)
        {
            var itemButton = sender as System.Windows.Controls.Button;
            foreach(var obj in pluginButtonListBox)
            {
                if (itemButton.Content == obj.pluginName)
                {
                    //弹窗 用来输入 命令行
                    InputShortCut inputShortCut = new InputShortCut();
                    inputShortCut.pluginButton = obj;
                    inputShortCut.sendMessage += Recevie;
                    inputShortCut.ShowDialog();
                }
            }

        }
        private void exitUtilityWindow(object sender, RoutedEventArgs e)
        {
            sendMessage(pluginButtonListBox);
            this.Close();
        }
        public void Recevie(string value,pluginButton pluginButton)
        {
            pluginButton.shortCutPath = value;
        }
    }
    public class pluginButton
    {
        public string pluginName { get; set; }
        public string pluginDescription { get; set; }
        public string headerIcon { get; set;}
        public string shortCutPath { get; set; }
    }
}
