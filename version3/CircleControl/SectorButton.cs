using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Brush = System.Windows.Media.Brush;
using Button = System.Windows.Controls.Button;

namespace CircleControl
{
    public class SectorButton : Button 
    {
        public static readonly DependencyProperty SectorFillProperty =
                   DependencyProperty.Register("SectorFill", typeof(Brush), typeof(SectorButton), new PropertyMetadata(null));

        public Brush SectorFill
        {
            get { return (Brush)GetValue(SectorFillProperty); }
            set { SetValue(SectorFillProperty, value); }
        }

        static SectorButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SectorButton), new FrameworkPropertyMetadata(typeof(SectorButton)));
        }
    }
}
