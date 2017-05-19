
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using Common;
using Ionic.Zip;
using Newtonsoft.Json;


namespace YunYiCdm
{
   // [Guid("7B634074-B8AB-4A14-98D6-1492BE90804B")]
  //  [Guid("758B226E-E824-49B5-B688-55DC1F319080")]
    [Guid("4C4FEFB2-305D-427A-8EA5-FBA7D7CEBB82")]

    [ClassInterface(ClassInterfaceType.None)]

    [ProgId("YunYiCdm.YunYiCdm")]

    public class YunYiCdm : IYunYiCdm
    {
        public string SendRestHttpClientRequest(string host, string method, string param)
        {
            if (param.Length > 1024 * 1024 * 4.2)
            {
                return JsonConvert.SerializeObject(new ResultModel { StatusCode = "000001", Result = "图片文件大小不能超过3M ！" });
            }
            var url = string.Format("http://{0}/{1}", host, method);
            try
            {
                var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
                using (var http = new HttpClient(handler))
                {
                    var content = new StringContent(param);
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = http.PostAsync(url, content).Result;
                    string srcString = response.Content.ReadAsStringAsync().Result;
                    return srcString;
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new ResultModel { StatusCode = "000001", Result = ex.Message });
            }
        }

        public string ByteToFile(byte[] bytesFile, string absoluteFilePath)
        {
            var ret = string.Empty;
            try
            {
               
                var tempfile = Path.GetTempFileName();
                File.WriteAllBytes(tempfile, bytesFile);
                using (var zip = new ZipFile(tempfile))
                {
                    if (!Directory.Exists(absoluteFilePath))
                    {
                        Directory.CreateDirectory(absoluteFilePath);
                    }
                    else
                    {
                        var p = Directory.GetFiles(absoluteFilePath);
                        foreach (string s in p)
                        {
                            File.Delete(s);
                        }
                    }
                    zip.ExtractAll(absoluteFilePath);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ret+ex.Message;
            }
        }

        public string JsonserializeEx(string countyCode,string userName,string password,string fileName,int kind ,int id, string absoluteFileName)
        {
            return JsonConvert.SerializeObject(new UploadPicture
            {
                CountyCode=countyCode,
                FileName=fileName,
                Id=id,
                Password=password,
                Kind=kind,
                UserName=userName,
                FileContent=File.ReadAllBytes(absoluteFileName)
            });
        }

        public string JsonserializeForCobject(object cObject)
        {
            
           return JsonConvert.SerializeObject(cObject);
        }

        public string ResultToFile(string result, string absoluteFilePath)
        {
          
            var ret = string.Empty;
            try
            {
                var model = JsonConvert.DeserializeObject<ResultModel>(result);
                var tempfile = Path.GetTempFileName();
                File.WriteAllBytes(tempfile,model.BussinessModel.zipFile );
                using (var zip = new ZipFile(tempfile))
                {
                    if (!Directory.Exists(absoluteFilePath))
                    {
                        Directory.CreateDirectory(absoluteFilePath);
                    }
                    else
                    {
                        var p = Directory.GetFiles(absoluteFilePath);
                        foreach (string s in p)
                        {
                            File.Delete(s);
                        }
                    }
                    zip.ExtractAll(absoluteFilePath);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ret + ex.Message;
            }
        }

        public string RestHttpClientGet(string host, string method, string param)
        {
            var url = string.Format("http://{0}/{2}/{1}", host, param, method);
            try
            {
                var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };

                using (var restget = new HttpClient(handler))
                {
                    var response = restget.GetAsync(url).Result;
                    string srcString = response.Content.ReadAsStringAsync().Result;
                    return srcString;
                }
            }
            catch (Exception ex)
            {
                return "000001服务url错误," + ex.Message;
            }
        }
    }
}
