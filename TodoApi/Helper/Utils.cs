
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace TodoApi.Helper
{
    public class Utils
    {
        public static DateTime convertDateFromString(string s)
        {
            return DateTime.ParseExact(s, "yyyyMMdd", CultureInfo.InvariantCulture);
        }

        public static DateTime convertDateFromTimeString(string s)
        {
            return DateTime.ParseExact(s, "HH:mm:ss", CultureInfo.InvariantCulture);
        }
        
        public static string GetJSONofHeader(HttpRequest request){
            string sHeader=null;
            var sRequest = request;
            var rH= request.Headers;
            Dictionary<string, string> dD=new Dictionary<string,string>();
            // sHeader =  $"Method=={sRequest.Method};;";
            dD.Add("Method",sRequest.Method);
            // sHeader += $"Protocol=={sRequest.Protocol};;";
            dD.Add("Protocol",sRequest.Protocol);
            foreach(var sHead in rH)
            {
            //    sHeader+=$"{sHead.Key}=={sHead.Value};;";
               dD.Add(sHead.Key,sHead.Value);
            }
            // Console.WriteLine( JsonConvert.SerializeObject(dD));
            
// Method==GET 
// Protocol==HTTP/1.1 
// Connection==keep-alive
// Accept==*/*
// Accept-Encoding==gzip, deflate, br
// Accept-Language==en-US,en;q=0.9
// Host==localhost:5006
// Referer==http://localhost:5006/
// User-Agent==Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Safari/537.36
// X-Requested-With==XMLHttpRequest

            sHeader = JsonConvert.SerializeObject(dD);  

            var obj = JsonConvert.DeserializeObject<Dictionary<string,string>>(sHeader);
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\Names.txt");
// string[] files = File.ReadAllLines(path);
            return sHeader;
        }
    }
}
