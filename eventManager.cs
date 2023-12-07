public class EventManager{
    //单例模式 现阶段觉得没什么用 不需要单例模式
    private static EventManager eventManagerInstance=new EventManager();
    private EventManager(){
        //构造函数
        Console.WriteLine("EventManagerInstance is created");
    }
    public static EventManager GetInstance(){
        if(eventManagerInstance == null){
            eventManagerInstance = new EventManager();
        }
        return eventManagerInstance;
    }
    #region 传入选择的区块编号的委托
    public delegate void SelectedToString(int index);
    public SelectedToString CheemsStringDelegate  = null;
    public static void showSelectedAction(int index){
        Label textLabel = new Label();
        textLabel.Text = index.ToString();
        textLabel.Font = new Font("微软雅黑", 20, FontStyle.Bold);

        textLabel.AutoSize = true;
        textLabel.Location = new Point(0, 0);
        textLabel.BackColor = Color.Transparent;
        textLabel.ForeColor = Color.White;
        textLabel.Parent = RoundForm.GetInstance();
        textLabel.BringToFront();
        textLabel.Show();
        
    }
    #endregion

    #region 退出程序的委托
    public delegate void ExitApplication();
    public ExitApplication CheemsExitApplicationDelegate = null;
    public static void applicationExit(){
        Application.Exit();
    }
    #endregion
}
