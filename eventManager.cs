using System.Configuration;

public class EventManager{
    //单例模式 现阶段觉得没什么用 不需要单例模式
    private static EventManager eventManagerInstance;
    private EventManager(){
        //构造函数
        Console.WriteLine("EventManager is created");
        CheemsStringDelegate += showSelectedAction;
    }
    public static EventManager GetInstance(){
        if(eventManagerInstance == null){
            eventManagerInstance = new EventManager();
        }
        return eventManagerInstance;
    }

    // 事件
    public delegate void SelectedToString(int index);
    public SelectedToString CheemsStringDelegate  = null;
    //方法
    public static void showSelectedAction(int index){
        Console.WriteLine("Selected segment: {0}", index);
    }

}
