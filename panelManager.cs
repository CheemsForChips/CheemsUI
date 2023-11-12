using System;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Drawing.Drawing2D;

public class RoundForm:Form{
    #region 轮盘显示效果的具体参数
    public bool isFormMaximized = true;//是否最大化
    public int[] defaultSize = new int[2] { 300, 600 };//默认form大小 最大化后大小设置失效        public bool isFormBorderActive = false;//是否显示边框
    public double formOpacity = 0.7f;//窗体透明度        
    public int selectedSegment = -1; // 当前选中的区块索引
    public int segmentCount = 8; // 区块数量
    public float segmentAngle; // 每个区块的角度
    static public bool isRouletteMax = false;//轮盘是否最大化 否则的话就是固定大小 给下面的参数赋值        
    static public int centerRoundUIRadius = 250;//轮盘的半径
    static public Color[] unselectSegmentColors = {Color.Red, Color.Orange, Color.Yellow, 
                                            Color.Green, Color.Blue, Color.Indigo, 
                                            Color.Violet, Color.Purple};
    static public Color selectedSegmentColor = Color.White;
    #endregion

    private static RoundForm roundFormInstance;
    public static RoundForm GetInstance(){
        if(roundFormInstance == null){
            roundFormInstance = new RoundForm();
        }
        return roundFormInstance;

    }
    
    //构造函数
    private RoundForm(){
        }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //求出来center位置
        Point center = new Point(ClientSize.Width / 2, ClientSize.Height / 2);
        //求出来半径
        int radius=Math.Min(ClientSize.Width, ClientSize.Height) / 2;
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
}
