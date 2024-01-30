using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PluginInfo
{
    public class PluginManager
    {
        //单例模式
        private PluginManager() { }
        private static PluginManager pluginManagerInstance = new PluginManager();
        public static PluginManager GetPluginManagerInstance()
        {
            if (pluginManagerInstance == null)
            {
                pluginManagerInstance = new PluginManager();
            }
            return pluginManagerInstance;
        }
        public static PluginManager GetPluginManager()
        {
               return pluginManagerInstance;
        }
        private PluginA GetPluginA()
        {
            return new PluginA();
        }
        private PluginB GetPluginB()
        {
            return new PluginB();
        }

    }
    class PluginA : IPluginDescription
    {
        public string Show()
        {
            Console.WriteLine("PluginA");
            return "PluginA";
        }
    }
    class PluginB : IPluginDescription
    {
        public string Show()
        {
            
            Console.WriteLine("PluginB");
            return "PluginB";
        }
    }

}
