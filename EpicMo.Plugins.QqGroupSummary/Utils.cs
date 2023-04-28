using System.Diagnostics;
using System.Security.Cryptography;

namespace EpicMo.Plugins.QqGroupSummary;

public static class Utils
{
    public static string TextGainCenter(string left, string right, string text)
    {
        //判断是否为null或者是empty
        if (string.IsNullOrEmpty(left))
            return "";
        if (string.IsNullOrEmpty(right))
            return "";
        if (string.IsNullOrEmpty(text))
            return "";

        var Lindex = text.IndexOf(left); //搜索left的位置

        if (Lindex == -1) //判断是否找到left
            return "";
        //abcd a d
        Lindex = Lindex + left.Length; //取出left右边文本起始位置

        var Rindex = text.IndexOf(right, Lindex); //从left的右边开始寻找right

        if (Rindex == -1) //判断是否找到right
            return "";

        return text.Substring(Lindex, Rindex - Lindex); //返回查找到的文本
    }

    /// <summary>
    ///     获取视频时长
    /// </summary>
    /// <param name="sourceFile">视频地址</param>
    /// <param name="ffmpegfile">ffmpeg存放文件夹地址</param>
    /// <returns></returns>
    public static string GetVideoDuration(string sourceFile, string ffmpegfile)
    {
        using (var ffmpeg = new Process())
        {
            string duration;
            string result;
            StreamReader errorreader;
            ffmpeg.StartInfo.UseShellExecute = false;
            //ffmpeg.StartInfo.ErrorDialog = false;
            ffmpeg.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            ffmpeg.StartInfo.RedirectStandardError = true;
            ffmpeg.StartInfo.FileName = ffmpegfile;
            ffmpeg.StartInfo.Arguments = "-i " + sourceFile;
            ffmpeg.StartInfo.CreateNoWindow = true; // 不显示程序窗口
            ffmpeg.Start();
            errorreader = ffmpeg.StandardError;
            ffmpeg.WaitForExit();
            result = errorreader.ReadToEnd();
            duration = result.Substring(result.IndexOf("Duration: ") + "Duration: ".Length, "00:00:00".Length);
            return duration;
        }
    }

    /// <summary>
    ///     导出封面图
    /// </summary>
    /// <param name="ffmpegFileName">FFmpeg.exe路径</param>
    /// <param name="videoFileName">视频文件路径</param>
    /// <returns>封面图</returns>
    public static string GetVideoFace(string ffmpegFileName, string videoFileName)
    {
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(videoFileName);
        var baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "video_tmp");
        if (!Directory.Exists(baseDirectory)) Directory.CreateDirectory(baseDirectory);
        var thumbFileName = Path.Combine(baseDirectory, fileNameWithoutExtension + ".jpg");
        var processStartInfo = new ProcessStartInfo(ffmpegFileName);
        processStartInfo.UseShellExecute = false;
        processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        processStartInfo.CreateNoWindow = true;
        processStartInfo.ErrorDialog = false;
        processStartInfo.RedirectStandardError = true;
        processStartInfo.Arguments = string.Format("-i \"{0}\" -y -f image2 -frames 30 \"{1}\"", new object[]
        {
            videoFileName,
            thumbFileName
        }); // 第30帧
        Process.Start(processStartInfo)!.WaitForExit(500);
        if (File.Exists(thumbFileName)) return thumbFileName;
        return "";
    }

    public static void Log(string msg)
    {
        File.AppendAllText(Directory.GetCurrentDirectory() + "\\log.log", msg + "\n");
    }

    public static void Success(string msg)
    {
        File.AppendAllText(Directory.GetCurrentDirectory() + "\\success.log", msg + "\n");
    }

    public static void Error(string msg)
    {
        File.AppendAllText(Directory.GetCurrentDirectory() + "\\error.log", msg + "\n");
    }

    public static string GetPictureCs(string sourceFile)
    {
        var pro = new Process();
        pro.StartInfo.FileName = "cmd.exe";
        pro.StartInfo.UseShellExecute = false;
        pro.StartInfo.RedirectStandardError = true;
        pro.StartInfo.RedirectStandardInput = true;
        pro.StartInfo.RedirectStandardOutput = true;
        pro.StartInfo.CreateNoWindow = true;
        //pro.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        pro.Start();
        pro.StandardInput.WriteLine("node index.js --path=\"" + sourceFile + "\"");
        pro.StandardInput.WriteLine("exit");
        pro.StandardInput.AutoFlush = true;
        //获取cmd窗口的输出信息
        var output = pro.StandardOutput.ReadToEnd();
        pro.WaitForExit(); //等待程序执行完退出进程
        pro.Close();
        return output.Split("\r\n")[4].Split("\n")[0];
    }


    public static string MD5Value(string filepath)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] md5ch;
        using (var fs = File.OpenRead(filepath))
        {
            md5ch = md5.ComputeHash(fs);
        }

        md5.Clear();
        var strMd5 = "";
        for (var i = 0; i < md5ch.Length; i++) strMd5 += md5ch[i].ToString("x").PadLeft(2, '0');

        return strMd5;
    }

    public static string GetUnixTime(DateTime dateTime)
    {
        return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds().ToString();
    }
}