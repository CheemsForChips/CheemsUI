public class mouseInput:Control{
    //获取到控件的实例
    private RoundForm roundFormInstance=RoundForm.GetInstance();
    private EventManager eventManagerInstance = EventManager.GetInstance();
    private static mouseInput mouseInputInstance;
    private mouseInput(){
        Console.WriteLine("mouseInputInstance is created");
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.UserPaint, true);
    } 
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
        int newSegment = (int)(angle / roundFormInstance.segmentAngle);
        if (newSegment != roundFormInstance.segmentAngle)
        {
            eventManagerInstance.CheemsStringDelegate(newSegment);
            Invalidate(); // 重绘控件
        }
    }
}
