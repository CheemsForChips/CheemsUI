using System.Drawing.Drawing2D;


public class RoundForm:Form{
    #region 窗体参数设置
    public bool isFormMaximized = true;//是否最大化
    public int[] defaultSize = new int[2] { 300, 600 };//默认form大小 最大化后大小设置失效        public bool isFormBorderActive = false;//是否显示边框
    public double formOpacity = 0.7f;//窗体透明度        
    public int selectedSegment = -1; // 当前选中的区块索引
    public int segmentCount = 8; // 区块数量
    public float segmentAngle;// 每个区块的角度
    static public bool isRouletteMax = false;//轮盘是否最大化 否则的话就是固定大小 给下面的参数赋值        
    static public int centerRoundUIRadius = 250;//轮盘的半径
    static public Color[] unselectSegmentColors = {Color.Red, Color.Orange, Color.Yellow, 
                                            Color.Green, Color.Blue, Color.Indigo, 
                                            Color.Violet, Color.Purple};
    static public Color selectedSegmentColor = Color.White;
    #endregion
    #region 单例模式 私有构造函数
    //构造函数
    private static RoundForm roundFormInstance=new RoundForm();
    public static RoundForm GetInstance(){
        if(roundFormInstance == null){
            roundFormInstance = new RoundForm();
        }
        return roundFormInstance;
    }
    private RoundForm()
    {
        Console.WriteLine("RoundFormInstance is created");
        this.Dock = DockStyle.Fill;//填充整个窗体
        this.FormBorderStyle = FormBorderStyle.None;         //设置窗体为无边框样式
        this.StartPosition = FormStartPosition.CenterScreen;//设置窗体居中显示
        this.ResumeLayout(false);
        this.Opacity = formOpacity;//设置不透明度 1为完全不透明 0为完全透明
        this.WindowState = FormWindowState.Maximized;//设置窗体最大化

        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.UserPaint, true);

    }
    #endregion
    #region 与鼠标输入相关
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        // 计算鼠标相对于控件中心的角度
        Point center = new Point(ClientSize.Width / 2, ClientSize.Height / 2);
        float dx = e.X - center.X;
        float dy = e.Y - center.Y;
        float angle = (float)(Math.Atan2(dy, dx) * 180 / Math.PI);
        if (angle < 0)
        {
            angle += 360;
        }
        // 根据角度计算当前选中的区块索引
        int newSegment = (int)(angle / RoundForm.GetInstance().segmentAngle);//roundFormInstance.segmentAngle
        if (newSegment != RoundForm.GetInstance().segmentAngle)
        {
            selectedSegment=newSegment;
            Invalidate(); // 重绘控件
        }
    }
    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);
        // 所有区块对应的共性操作
        EventManager.GetInstance().CheemsStringDelegate+=EventManager.showSelectedAction;
        EventManager.GetInstance().CheemsStringDelegate?.Invoke(selectedSegment);

        //不同的区块对应不同的操作
        switch(selectedSegment)
        {
            case 0:
                Console.WriteLine("0");
                break;
            case 1:
                Console.WriteLine("1");
                break;
            case 2:
                Console.WriteLine("2");
                break;
            case 3:
                Console.WriteLine("3");
                break;
            case 4:
                Console.WriteLine("4");
                break;
            case 5:
                Console.WriteLine("5");
                EventManager.GetInstance().CheemsExitApplicationDelegate+=EventManager.applicationExit;
                break;
            case 6:
                Console.WriteLine("6");
                break;
            case 7:
                Console.WriteLine("7");
                break;
            default:
                Console.WriteLine("default");
                break;
        }

        EventManager.GetInstance().CheemsExitApplicationDelegate?.Invoke();//委托不空就运行
    }
    #endregion 
    #region 窗体的具体效果实现
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //求出来center位置
        Point center = new Point(ClientSize.Width / 2, ClientSize.Height / 2);
        //求出来半径
        int radius=250;
        //绘制背景颜色 这个颜色固定是灰色
        using (Brush bgBrush = new SolidBrush(Color.LightGray))
        {
            g.FillPie(bgBrush, center.X - radius, center.Y - radius, radius * 2, radius * 2, 0, 360);
        }
        //每个圆环的应该占据的额角度
        segmentAngle = 360f / segmentCount;
        // 绘制每个区块
        for (int i = 0; i < segmentCount; i++)
        {
            float startAngle = i * segmentAngle;
            float endAngle = startAngle + segmentAngle;
            int innerRadius = radius / 2;//内圆半径是外圆半径的一半
            bool isSelected = (i == selectedSegment);
            //绘制每个区块的颜色
            Color segmentColor = isSelected ? selectedSegmentColor : unselectSegmentColors[i];
            Brush segmentBrush = new SolidBrush(segmentColor);
            // 绘制区块的路径
            GraphicsPath path = new GraphicsPath();
            // 添加外圆的扇形
            path.AddPie(center.X - radius, center.Y - radius, radius * 2, radius * 2, startAngle, segmentAngle);
            // 添加内圆的扇形
            path.AddPie(center.X - innerRadius, center.Y - innerRadius, innerRadius * 2, innerRadius * 2, startAngle, segmentAngle);
            // 使用填充模式，只填充外圆和内圆之间的部分
            g.FillPath(segmentBrush, path);
        }
    }
    #endregion
}
