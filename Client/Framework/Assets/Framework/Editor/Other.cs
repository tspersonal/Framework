using System.Diagnostics;
using System.IO;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace Assets.Tools.Other.Editor
{
    public class Other
    {
        /// <summary>
        /// 
        /// </summary>
        [MenuItem("Tools/Other/Set Unity AndroidManifest")]
        static void SetUnityAndroidManifest()
        {
            //获取当前进程的完整路径，包含文件名(进程名)。
            //result: X:\xxx\xxx\xxx.exe(.exe文件所在的目录 +.exe文件名)
            //string str = this.GetType().Assembly.Location;

            //获取新的 Process 组件并将其与当前活动的进程关联的主模块的完整路径，包含文件名(进程名)。
            //result: X:\xxx\xxx\xxx.exe(.exe文件所在的目录 +.exe文件名)
            string str0 = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

            //获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。
            //result: X:\xxx\xxx(.exe文件所在的目录)
            //string str2 = System.Environment.CurrentDirectory;

            //获取当前 Thread 的当前应用程序域的基目录，它由程序集冲突解决程序用来探测程序集。
            //result: X:\xxx\xxx\ (.exe文件所在的目录 + "\")
            //string str3 = System.AppDomain.CurrentDomain.BaseDirectory;

            //获取和设置包含该应用程序的目录的名称。(推荐)
            //result: X:\xxx\xxx\ (.exe文件所在的目录 + "\")
            //string str4 = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            //获取启动了应用程序的可执行文件的路径，不包括可执行文件的名称。
            //result: X:\xxx\xxx(.exe文件所在的目录)
            //string str = System.Windows.Forms.Application.StartupPath;

            //获取启动了应用程序的可执行文件的路径，包括可执行文件的名称。
            //result: X:\xxx\xxx\xxx.exe(.exe文件所在的目录 +.exe文件名)
            //string str = System.Windows.Forms.Application.ExecutablePath;

            //获取应用程序的当前工作目录(不可靠)。
            //result: X:\xxx\xxx(.exe文件所在的目录)
            //string str5 = System.IO.Directory.GetCurrentDirectory();


            //使用str1，获取unity安装的路径
            string str1 = str0.Replace("Unity.exe", "Data\\PlaybackEngines\\AndroidPlayer\\Apk\\AndroidManifest.XML");
            string str2 = str0.Replace("Unity.exe", "Data\\PlaybackEngines\\AndroidPlayer\\Apk\\AndroidManifest_Target.XML");

            FileInfo file = new FileInfo(str2);
            if (File.Exists(str1))
            {
                File.Delete(str1);
            }
            file.CopyTo(str1);

            Debug.Log(str2 + "===>>>" + str1);

            //FileStream file = new FileStream(str, FileMode.Truncate, FileAccess.ReadWrite);//清空文件内容
            //file.Close();
            //file = new FileStream(str, FileMode.Append, FileAccess.Write);//重新写入文件
            //StreamWriter sw = new StreamWriter(file);
            //sw.Write(str);
            //sw.Close();

        }

        /// <summary>
        /// 
        /// </summary>
        [MenuItem("Tools/Other/ReSet Unity AndroidManifest")]
        static void ReSetUnityAndroidManifest()
        {
            string str0 = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            Debug.Log(str0);

            //使用str1，获取unity安装的路径
            string str1 = str0.Replace("Unity.exe", "Data\\PlaybackEngines\\AndroidPlayer\\Apk\\AndroidManifest.XML");
            string str2 = str0.Replace("Unity.exe", "Data\\PlaybackEngines\\AndroidPlayer\\Apk\\AndroidManifest_Default.XML");

            FileInfo file = new FileInfo(str2);
            if (File.Exists(str1))
            {
                File.Delete(str1);
            }
            file.CopyTo(str1);


            Debug.Log(str2 + "===>>>" + str1);
        }

        /// <summary>
        /// 在文件夹中显示
        /// </summary>
        [MenuItem("Tools/Other/Show AndroidManifest InExplorer")]
        static void ShowInExplorer()
        {
            string str0 = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            Debug.Log(str0);

            //使用str1，获取unity安装的路径
            string fileName = str0.Replace("Unity.exe", "Data\\PlaybackEngines\\AndroidPlayer\\Apk\\AndroidManifest.XML");

            if (File.Exists(fileName))
            {
                Process.Start(@"explorer.exe", "/select,\"" + fileName + "\"");
            }

            Debug.Log("Show In Explorer===>>>" + fileName);
        }
    }
}