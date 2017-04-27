
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace CdmCliComNs
{
    [Guid("6E8ADBC6-2E94-4B6C-8849-5A55359E863B")]
    public interface ICDMCCom
    {
        [DispId(1)]
        string RestHttpClientGet(string host, string method, string param);
        [DispId(2)]
        string SendRestHttpClientRequest(string host, string method, string param);
    }

    [Guid("7B634074-B8AB-4A14-98D6-1492BE90804B")]

    [ClassInterface(ClassInterfaceType.None)]

    [ProgId("CdmCliComNs.CDMCCom")]

    public class CDMCCom : ICDMCCom
    {
        public CDMCCom()
        {

        }
        ~CDMCCom()
        {

        }
       
        public string SendRestHttpClientRequest(string host, string method, string param)
        {
            var url = string.Format("http://{0}/{1}", host, method);
            var srcString = string.Empty;
            try
            {
                var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
                using (var http = new HttpClient(handler))
                {
                    var content = new StringContent(param);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = http.PostAsync(url, content).Result;
                    srcString = response.Content.ReadAsStringAsync().Result;
                    return srcString;
                    //  return (response.IsSuccessStatusCode ? "000000" : ("000002" + response.StatusCode)) + srcString;
                }
            }
            catch (Exception ex)
            {
                return "000001服务url错误," + ex.Message;
            }
        }
        public string RestHttpClientGet(string host, string method, string param)
        {
            var url = string.Format("http://{0}/{2}/{1}", host, param, method);
            var srcString = string.Empty;
            try
            {
                var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };

                using (var restget = new HttpClient(handler))
                {
                    var response = restget.GetAsync(url).Result;
                    srcString = response.Content.ReadAsStringAsync().Result;
                    return srcString;
                    //   return (response.IsSuccessStatusCode ? "000000" : ("000002" + response.StatusCode)) + srcString;
                }
            }
            catch (Exception ex)
            {
                return "000001服务url错误," + ex.Message;
            }
        }
    }
}
