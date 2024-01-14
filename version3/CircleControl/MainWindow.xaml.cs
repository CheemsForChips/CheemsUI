using System.Configuration;
using System.Text;
using System.Drawing.Drawing2D;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CircleControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

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
                UtilityTextShow.Text = newSegment.ToString();
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

            switch(SectorSegement)
            {
                case 0:
                    UtilityModel.GetUtilityModelInstance().ExecuteCommand("taskmgr");
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    UtilityModel.GetUtilityModelInstance().ExitCircleControl();
                    break;
                case 6:
                    break;
                case 7:
                    break;
            }

        }

    }
}