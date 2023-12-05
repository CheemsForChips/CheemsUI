public class mouseInput:Control{
    #region 单例模式 私有构造函数
    private static mouseInput mouseInputInstance=new mouseInput();
    private mouseInput(){
        Console.WriteLine("mouseInputInstance is created");
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.UserPaint, true);
    }
    public static mouseInput GetInstance(){
        if(mouseInputInstance == null){
            mouseInputInstance = new mouseInput();
        }
        return mouseInputInstance;
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
