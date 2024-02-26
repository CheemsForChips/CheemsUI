using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace PluginBase
{
    public class PluginOperate
    {
        //单例模式 使得插件操作类只有一个实例
        private static PluginOperate instance=new PluginOperate(@"./Plugins");
        private PluginOperate() { }
        public static PluginOperate GetInstance()
        {
            return instance;
        }




        public string PluginPath { get; set; } = @"./Plugins"; //默认插件路径
        //用来存储插件的列表
        public  List<ModPlugin> Pluginlist=new List<ModPlugin>();
        //构造函数 给path赋值
        public PluginOperate(string path)
        {
            PluginPath = path;
        }
        /// <summary>
        /// 加载插件到列表中
        /// </summary>
        /// <returns>加载是否成功</returns>
        public bool LoadPlugin()
        {
            DirectoryInfo dir = new DirectoryInfo(PluginPath);
            if (!dir.Exists)
            {
                return false;
            }
            else
            {
                FileInfo[] files = dir.GetFiles("*.dll");
                foreach (FileInfo file in files)
                {
                    try
                    {
                        //加载程序集
                        System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(file.FullName);
                        //遍历程序集中的每一个类型
                        foreach (Type t in assembly.GetTypes())
                        {
                            //判断类型是否实现了IPlugin接口
                            if (t.GetInterface("IDev") != null)
                            {
                                //创建插件实例
                                IDev plugin = (IDev)Activator.CreateInstance(t);
                                //将插件实例添加到列表中
                                Pluginlist.Add(new ModPlugin(plugin));
                                return true;
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                return true;
            }
        }

        public void Execute(string cmd,int num)
        {
           
        }

        /// <summary>
        /// 命令行执行 调出任务管理器功能
        /// </summary>
        /// <param name="command"></param>
        public void ExecuteCommand(string command)
        {
            var processInfo = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = $"/C {command}",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            using (var process = new Process())
            {
                process.StartInfo = processInfo;
                process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                process.Start();
                process.BeginOutputReadLine();
                process.WaitForExit();
            }
        }
    }
}
