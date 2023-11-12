using System.Configuration;

public class EventManager{
    //单例模式 现阶段觉得没什么用 不需要单例模式
    private static EventManager eventManagerInstance;
    private EventManager(){
        //构造函数
        Console.WriteLine("EventManager is created");
    }
    public static EventManager GetInstance(){
        if(eventManagerInstance == null){
            eventManagerInstance = new EventManager();
        }
        return eventManagerInstance;
    }

    
    // 事件
    delegate void SelectionChangedEventHandler(int index);
    //方法
    public void showSelectedAction(int index){
        Console.WriteLine("Selected segment: {0}", index);
    }
    public void triggerHandler(){
        SelectionChangedEventHandler handler = new SelectionChangedEventHandler(showSelectedAction);
        handler(1);
    }
}
