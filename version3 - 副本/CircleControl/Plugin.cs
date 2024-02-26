using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




namespace Plugin
{
    public interface IPlugin
    {
        string Show();
    }

    public class PluginA:IPlugin
    {
        public string Show()
        {
            MessageBox.Show("PluginA");
            return "PluginA";
        }
    }
    public class PluginB:IPlugin
    {
        public string Show()
        {
            MessageBox.Show("PluginB");
            return "PluginB";
        }
    }
}
