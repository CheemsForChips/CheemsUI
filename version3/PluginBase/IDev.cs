using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginBase
{
    public interface IDev
    {
        string PluginName { get; set; }   
        string PluginDescription { get; set; }
        bool isPluginToggle { get; set; }
        string Execute { get; set; }
    }
}
