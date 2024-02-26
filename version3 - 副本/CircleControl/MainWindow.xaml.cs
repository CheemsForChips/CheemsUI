using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Walterlv.WindowDetector;
using NHotkey.Wpf;
using Color = System.Windows.Media.Color;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Point = System.Windows.Point;
using System.Reflection.Metadata;
using NHotkey;
using Gma.System.MouseKeyHook;
using PluginBase;
using System.Collections.ObjectModel;



namespace CircleControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<pluginButton> pluginButtonInMainWindow=new ObservableCollection<pluginButton>();

        public void receiveMessage(ObservableCollection<pluginButton> pluginButtons)
        {
            pluginButtonInMainWindow= pluginButtons;
        }
        public static int SectorSegement;
        // a r g b先是透明通道 然后才是rgb注意区分
        public static SolidColorBrush[] UnselectedSectorColor = { new SolidColorBrush(Color.FromArgb(0xFF,0x1E, 0x96, 0xFC)),
                                                                    new SolidColorBrush(Color.FromArgb(0xFF, 0x60, 0xB6, 0xFB )),
                                                                    new SolidColorBrush(Color.FromArgb(0xFF, 0xA2, 0xD6, 0xF9)),
                                                                    new SolidColorBrush(Color.FromArgb(0xFF, 0xCF, 0xE5, 0x7D)),
                                                                    new SolidColorBrush(Color.FromArgb(0xFF, 0xE6, 0xEC, 0x3F)),
                                                                     new SolidColorBrush(Color.FromArgb(0xFF, 0xFC, 0xF3, 0x00)),
                                                                    new SolidColorBrush(Color.FromArgb(0xFF, 0x0D, 0x45, 0xD5)),
                                                                    new SolidColorBrush(Color.FromArgb(0xFF, 0x16, 0x6E, 0xE9))
                                                                    };
        public static SolidColorBrush SelectedSectorColor= new SolidColorBrush(Color.FromArgb(0xFF, 0xE0, 0xDF, 0xC0));

        // winform下的系统托盘控件
        private NotifyIcon _notifyIcon = null;
        private ContextMenuStrip __ContextMenuStrip = null;

        private IKeyboardMouseEvents myMouseHook;
        public MainWindow()
        {

            InitializeComponent();
            this.DataContext = this;
            //一启动就最小化至托盘
            InitialTray();
            //hotkey注册
            HotkeyManager.Current.AddOrReplace("Increment", Key.Space, ModifierKeys.Alt, OnIncrement);
            //mousehook注册
            myMouseHook = Hook.GlobalEvents();
            myMouseHook.MouseDoubleClick += MyMouseHook_MouseDoubleClick;


            pluginButtonInMainWindow.Add(new pluginButton { pluginName = "一键休眠", pluginDescription = "选中该区块即可使电脑进入休眠模式", headerIcon = "\ue677", shortCutPath = "shutdown -h" });
            //pluginButtonListBox.Add(new pluginButton { pluginName = "划词翻译", pluginDescription = "选词翻译", headerIcon = "\ue68a" });
            pluginButtonInMainWindow.Add(new pluginButton { pluginName = "任务管理器", pluginDescription = "打开任务管理器", headerIcon = "\ue695", shortCutPath = "taskmgr" });
            pluginButtonInMainWindow.Add(new pluginButton { pluginName = "窗口置顶", pluginDescription = "将窗口选中的窗口始终置顶", headerIcon = "\ue6a3" });
            //pluginButtonListBox.Add(new pluginButton { pluginName = "浏览器搜索", pluginDescription = "选中词句之后，调用该区块功能实现快捷浏览器搜索", headerIcon = "\ue798" });
            pluginButtonInMainWindow.Add(new pluginButton { pluginName = "文件夹打开", pluginDescription = "点击该区块即可打开文件夹", headerIcon = "\ue610", shortCutPath = "explorer Desktop" });
            pluginButtonInMainWindow.Add(new pluginButton { pluginName = " 计算器", pluginDescription = " 计算器", headerIcon = "\ue79b", shortCutPath = "calc" });
            pluginButtonInMainWindow.Add(new pluginButton { pluginName = "shortCut0", pluginDescription = "输入快捷方式的路径", headerIcon = "\ue60a" });
            pluginButtonInMainWindow.Add(new pluginButton { pluginName = "shortCut1", pluginDescription = "输入快捷方式的路径", headerIcon = "\ue60a" });
            pluginButtonInMainWindow.Add(new pluginButton { pluginName = "shortCut2", pluginDescription = "输入快捷方式的路径", headerIcon = "\ue60a" });
        }
        //鼠标双击事件
        // https://github.com/gmamaladze/globalmousekeyhook
        private void MyMouseHook_MouseDoubleClick(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
           if(e.Button==MouseButtons.Middle)
            {
                //双击鼠标中键 显示主窗体
                this.Visibility = Visibility.Visible;
                //激活主窗体
                this.Activate();
            }
        }
        //hotkey触发事件
        private void OnIncrement(object sender, HotkeyEventArgs e)
        {
            //双击鼠标中键 显示主窗体
            this.Visibility = Visibility.Visible;
            //激活主窗体
            this.Activate();
        }

        #region 最小化系统托盘
        private void InitialTray()
        {
            //右键菜单设置
            __ContextMenuStrip = new ContextMenuStrip();
            __ContextMenuStrip.Items.Add("设置");
            __ContextMenuStrip.Items.Add("退出");
            __ContextMenuStrip.ItemClicked += (sender, e) =>
            {
                if (e.ClickedItem.Text == "设置")
                {
                    Setting setting = new Setting();
                    setting.sendMessage += receiveMessage;
                    setting.ShowDialog();
                }
                else if (e.ClickedItem.Text == "退出")
                {
                    //关闭该程序所有的窗口
                    System.Windows.Application.Current.Shutdown();
                }
            };
            //隐藏主窗体
            this.Visibility = Visibility.Hidden;
            //设置托盘的各个属性
            _notifyIcon = new NotifyIcon();
            _notifyIcon.ContextMenuStrip = __ContextMenuStrip;//右键菜单关联到托盘控件
            _notifyIcon.BalloonTipText = "CheemsUI运行中...";//托盘气泡显示内容
            _notifyIcon.Text = "CheemsUI";
            _notifyIcon.Visible = true;//托盘按钮是否可见
            _notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            _notifyIcon.ShowBalloonTip(1000);//托盘气泡显示时间
            _notifyIcon.MouseClick+=notifyIcon_MouseClick;
            //窗体状态改变时触发
            this.StateChanged += MainWindow_StateChanged;
        }
        #endregion

        #region 窗口状态改变
        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        #region 托盘图标鼠标单击事件
        private void notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //鼠标左键的点击事件
            if (e.Button == MouseButtons.Left)
            {
                if (this.Visibility == Visibility.Visible)
                {
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.Visibility = Visibility.Visible;
                    this.Activate();
                }
            }
        }
        #endregion

        /// <summary>
        /// 旋转鼠标标记控件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
           
            base.OnMouseMove(e);

            Point center = new Point(ActualWidth / 2, ActualHeight / 2);
            double dx = e.GetPosition(this).X - center.X;
            double dy = e.GetPosition(this).Y - center.Y;
            double angle = (Math.Atan2(dy, dx) * 180 / Math.PI + 360) % 360;

            int newSegment = (int)(angle / 45);
            {
                SectorSegement = newSegment;
                UtilityTextShow.Text = pluginButtonInMainWindow[SectorSegement].pluginName.ToString();
            }

            for(int i = 0; i < 8; i++)
            {
                bool isSelect = (i == SectorSegement);
                SolidColorBrush solidColorBrush = isSelect ? SelectedSectorColor : UnselectedSectorColor[i];
                switch (i)
                {
                    case 0:Arc0.SectorFill = solidColorBrush; break;
                    case 1: Arc1.SectorFill = solidColorBrush;break;
                    case 2: Arc2.SectorFill = solidColorBrush;break;
                    case 3: Arc3.SectorFill = solidColorBrush;break;
                    case 4: Arc4.SectorFill = solidColorBrush;break;
                    case 5: Arc5.SectorFill = solidColorBrush;break;
                    case 6: Arc6.SectorFill = solidColorBrush;break;
                    case 7 : Arc7.SectorFill = solidColorBrush;break;
                }
            }
        }

        /// <summary>
        /// 点击鼠标 触发控件的事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            ////每次鼠标点击 都将会重新加载一下插件 后期用来实现实时插件信息的更改
            //PluginOperate pluginOperate = PluginOperate.GetInstance();
            //var exeCommand = pluginOperate.Pluginlist[SectorSegement].Execute;
            //pluginOperate.LoadPlugin();
            //pluginOperate.Execute(exeCommand,SectorSegement);


            if (pluginButtonInMainWindow[SectorSegement].pluginName=="窗口置顶")
            {
                UtilityModel.GetUtilityModelInstance().ExecuteSetWindowTop();
            }
            else
            {
                UtilityModel.GetUtilityModelInstance().ExecuteCommand(pluginButtonInMainWindow[SectorSegement].shortCutPath);
            }
            this.Visibility = Visibility.Hidden;
        }

    }
}