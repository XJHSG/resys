using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// FileHelper 的摘要说明
/// </summary>
public class FileHelper
{
    public FileHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public static string strFile = "";
    public static void SetFileName(string FileName)
    {
        strFile += FileName;
    }
    public static string GetFileName()
    {
        return strFile;
    }
    public static void CleanFileName()
    {
        strFile = "";
    }

    public static int TypeOfFile(String Extension) {
        int result = 0;
        string[] photos = ".jpg,.jpeg,.png,.gif,.bmp".Split(',');
        string[] videos = ".mp4,.mov,.flv,.3gp,.3g2,.ogv,.webm".Split(',');
        string[] musics = ".mp3,.aac,.m4a,.ogg,.wav".Split(',');
        string[] files = ".doc,.docx,.xls,.xlsx,.ppt,.pptx,.pdf,.txt".Split(',');
        string[] comps = ".zip,.rar,.7z,.iso".Split(',');
        string[] flvs = ".flv".Split(',');

        foreach(string a in photos)
        {
            if (a == Extension)
            {
                result = 1;
                break;
            }
               
        }
        foreach (string a in videos)
        {
            if (a == Extension)
            {
                result = 2;
                break;
            }

        }
        foreach (string a in musics)
        {
            if (a == Extension)
            {
                result = 3;
                break;
            }

        }
        foreach (string a in files)
        {
            if (a == Extension)
            {
                result = 4;
                break;
            }

        }
        foreach (string a in comps)
        {
            if (a == Extension)
            {
                result = 5;
                break;
            }

        }
        foreach (string a in flvs)
        {
            if (a == Extension)
            {
                result = 6;
                break;
            }

        }


        return result;
    }
}