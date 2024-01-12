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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            //UtilityTextShow.Text = button.Name;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            //UtilityTextShow.Text = "CheemsShow";
        }

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
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            switch (SectorSegement)
            {
                case 0:
                    Arc0.SectorFill=new SolidColorBrush(Color.FromArgb(0xE0, 0xDF, 0xC0, 0xFF));
                    break;
                case 1:
                    Arc1.SectorFill= new SolidColorBrush(Color.FromArgb(0xE0, 0xDF, 0xC0, 0xFF));
                    break;
                case 2:
                    Arc0.SectorFill = new SolidColorBrush(Color.FromArgb(0xE0, 0xDF, 0xC0, 0xFF));
                    break;
                case 3:
                    Arc1.SectorFill = new SolidColorBrush(Color.FromArgb(0xE0, 0xDF, 0xC0, 0xFF));
                    break;
                case 4:
                    Arc0.SectorFill = new SolidColorBrush(Color.FromArgb(0xE0, 0xDF, 0xC0, 0xFF));
                    break;
                case 5:
                    Arc1.SectorFill = new SolidColorBrush(Color.FromArgb(0xE0, 0xDF, 0xC0, 0xFF));
                    break;
                case 6:
                    Arc0.SectorFill = new SolidColorBrush(Color.FromArgb(0xE0, 0xDF, 0xC0, 0xFF));
                    break;
                case 7:
                    Arc1.SectorFill = new SolidColorBrush(Color.FromArgb(0xE0, 0xDF, 0xC0, 0xFF));
                    break;
            }
        }

    }
}