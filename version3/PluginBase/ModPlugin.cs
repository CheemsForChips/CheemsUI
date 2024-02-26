 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginBase
{
    public class ModPlugin
    {
        public string PluginName
        {
            get
            {
                return Plugin.PluginName;
            }
            set
            {
                Plugin.PluginName = value;
            }
        }

        public string PluginDescription
        {
            get
            {
                return Plugin.PluginDescription;
            }
            set
            {
                Plugin.PluginDescription = value;
            }
        }

        public bool isPluginToggle
        {
            get
            {
                return Plugin.isPluginToggle;
            }
            set
            {
                Plugin.isPluginToggle = value;
            }
        }
        public string Execute
        {
            get
            {
                return Plugin.Execute;
            }
            set
            {
                Plugin.Execute = value;
            }
        }

        public IDev Plugin { get; set; }
        public ModPlugin(IDev plugin)
        {
            Plugin = plugin;
        }
    }
}
