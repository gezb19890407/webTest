using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;

namespace testWebApplication.plugInUnit.webPush
{
    public class HttpClientUtil
    {
        private static string xiazhi_tomcat_url = "http://goeasy.io/goeasy/";
        private static string xiazhi_server_url = xiazhi_tomcat_url;

        // REST @GET 方法，根据泛型自动转换成实体，支持List<T>
        public static T doGetMethodToObj<T>(string metodUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(xiazhi_server_url + metodUrl);
            request.Method = "get";
            request.ContentType = "application/json;charset=UTF-8";
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                response = (HttpWebResponse)e.Response;
                throw e;
            }
            string json = getResponseString(response);
            return JsonConvert.DeserializeObject<T>(json);
        }

        // 将 HttpWebResponse 返回结果转换成 string
        private static string getResponseString(HttpWebResponse response)
        {
            string json = null;
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8")))
            {
                json = reader.ReadToEnd();
            }
            return json;
        }

        // 获取异常信息
        private static string getRestErrorMessage(HttpWebResponse errorResponse)
        {
            string errorhtml = getResponseString(errorResponse);
            string errorkey = "spi.UnhandledException:";
            errorhtml = errorhtml.Substring(errorhtml.IndexOf(errorkey) + errorkey.Length);
            errorhtml = errorhtml.Substring(0, errorhtml.IndexOf("\n"));
            return errorhtml;
        }

        //对应soapui 勾选 query-parameters should be put in message body
        //REST @POST 方法
        public static T doPostMethodToObj<T>(string metodUrl, string jsonBody)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(xiazhi_server_url + metodUrl);
            request.Method = "Post";
            request.ContentType = "application/json;charset=UTF-8";
            var stream = request.GetRequestStream();
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(jsonBody);
                writer.Flush();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string json = getResponseString(response);
            return JsonConvert.DeserializeObject<T>(json);
        }

        // REST @POST 方法
        public static string doPostMethodToObj(string metodUrl, string jsonBody)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(xiazhi_server_url + metodUrl);
            request.Method = "Post";
            request.ContentType = "application/x-www-form-urlencoded";
            StringBuilder data = new StringBuilder();
            data.AppendFormat("appkey=BC-d1188086f6b944d1a76c8ce4088ad5f1&channel=notification&content=123456");
            byte[] byteData = UTF8Encoding.UTF8.GetBytes(data.ToString());
            request.ContentLength = byteData.Length;
            using (Stream postStream = request.GetRequestStream())
            {
                postStream.Write(byteData, 0, byteData.Length);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string json = getResponseString(response);
            return json;
        }

        // REST @PUT 方法
        public static string doPutMethod(string metodUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(xiazhi_server_url + metodUrl);
            request.Method = "put";
            request.ContentType = "application/json;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8")))
            {
                return reader.ReadToEnd();
            }
        }

        // REST @PUT 方法，带发送内容主体
        public static T doPutMethodToObj<T>(string metodUrl, string jsonBody)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(xiazhi_server_url + metodUrl);
            request.Method = "put";
            request.ContentType = "application/json;charset=UTF-8";
            var stream = request.GetRequestStream();
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(jsonBody);
                writer.Flush();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string json = getResponseString(response);
            return JsonConvert.DeserializeObject<T>(json);
        }

        // REST @DELETE 方法
        public static bool doDeleteMethod(string metodUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(xiazhi_server_url + metodUrl);
            request.Method = "delete";
            request.ContentType = "application/json;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8")))
            {
                string responseString = reader.ReadToEnd();
                if (responseString.Equals("1"))
                {
                    return true;
                }
                return false;
            }
        }
    }
}