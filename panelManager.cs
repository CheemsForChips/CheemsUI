using System;
using System.Drawing;
using Microsoft.VisualBasic;
using System.Drawing.Drawing2D;

public class RoundForm:Form{
    #region 参数设置
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

        //启用输入系统inputManager
        this.Controls.Add(mouseInput.GetInstance());
    }
    #endregion
    protected override void OnMouseMove(MouseEventArgs e)
    {
        Console.WriteLine("OnMouseMove is called");
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
            EventManager.GetInstance().CheemsStringDelegate(newSegment);
            Invalidate(); // 重绘控件
        }
    }
    
}
