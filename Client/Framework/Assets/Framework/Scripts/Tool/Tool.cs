using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

public class Tool
{
    /// <summary>
    /// 字符串转为MD5码
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string CalculateMd5Hash(string input)
    {
        MD5 md5 = MD5.Create();
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);

        // step 2, convert byte array to hex string
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }

    /// <summary>
    /// 文件转为MD5码
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string GetMd5HashFromFile(string fileName)
    {
        try
        {
            FileStream file = new FileStream(fileName, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
        }
    }

    /// <summary>
    /// 解析URL 获得IP
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string GetServerIp(string url)
    {
        string str = url.Substring(0, 7);
        if (str == "192.168") return url;
        IPHostEntry ipHost = Dns.GetHostEntry(url);
        string serverIp = "";
        switch (ipHost.AddressList[0].AddressFamily)
        {
            case AddressFamily.InterNetwork:
                NetConnectServer.IsIpv6 = false;
                break;
            case AddressFamily.InterNetworkV6:
                NetConnectServer.IsIpv6 = true;
                break;

        }
        serverIp = ipHost.AddressList[0].ToString();
        return serverIp;
    }
}
