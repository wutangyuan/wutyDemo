// See https://aka.ms/new-console-template for more information


using System.Diagnostics;
using System.Text;

var cmdConsoleAppPath  = @$"E:\Code\wutyDemo\CmdConsoleApp\bin\Debug\net8.0\CmdConsoleApp.exe";
var pathLength = cmdConsoleAppPath.Length;



StringBuilder sb = new StringBuilder();

//32,767


var cmdFileName = "cmd.exe";
var processArgs1 = $"/k {cmdConsoleAppPath} ";

var cmdParmamterCount = 8188 - cmdFileName.Length - processArgs1.Length;
for (int i = 0; i < cmdParmamterCount; i++)
{
    sb.Append("1");
}
processArgs1 += sb.ToString();

using Process process = new Process();


// 2. 配置启动信息
process.StartInfo = new ProcessStartInfo
{
    FileName = cmdFileName,                 // 调用命令提示符
    Arguments = processArgs1,   // 执行命令参数
};
process.Start();



var count = 32763 - pathLength;

sb = new StringBuilder();
for (int i = 0; i < count; i++)
{
    sb.Append("1");
}

Process.Start(cmdConsoleAppPath, sb.ToString());