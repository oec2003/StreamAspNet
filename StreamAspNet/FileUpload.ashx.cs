using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using EntApp.Common;
using System.IO;
using EntApp.Common.Extension;
using Newtonsoft;
using Newtonsoft.Json;
using StreamAspNet.Common;

namespace StreamAspNet
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class FileUpload : IHttpHandler
    {
        HttpRequest _request;
        HttpResponse _response;
        HttpServerUtility _server;
        
        FileHelper _fileHelper = new FileHelper();

        private string _tokenPath = "upload/tokens/";            //令牌保存目录
	    private string _filePath = "upload/files/";              //上传文件保存目录

        public void ProcessRequest(HttpContext context)
        {
            _request = context.Request;
            _response = context.Response;
            _server = context.Server;

            string method = _request["Method"].ToString();

            MethodInfo methodInfo = GetType().GetMethod(method);
            methodInfo.Invoke(this, null);
        }

        public void upload()
        {
            string token = _request.QueryString["token"];
            UploadToken uploadToken = GetTokenInfo(token);

            if(uploadToken!=null && uploadToken.size>uploadToken.upsize)
            {
                Stream stream = _request.InputStream;
                if(stream!=null && stream.Length>0)
                {
                    _fileHelper.FileName = uploadToken.name;
                    _fileHelper.FilePath = _server.MapPath(_filePath);
                    _fileHelper.WriteFile(stream);

                    uploadToken.upsize += stream.Length;
                    if(uploadToken.size>uploadToken.upsize)
                    {
                        SetTokenInfo(token, uploadToken);
                    }
                    else
                    {
                        //上传完成后删除令牌信息
                        DelTokenInfo(token);
                    }
                }
            }
            UploadResult ur = new UploadResult();
            ur.message = "";
            ur.start = uploadToken.upsize;
            ur.success = true;

            string result= JsonHelper.SerializeObject(ur);
            _response.Write(result);
        }

        /// <summary>
        /// 获取令牌
        /// </summary>
        public void tk()
        {
            UploadToken uploadToken = new UploadToken();
            
            string name = _request.QueryString["name"];
            string size = _request.QueryString["size"];
            string ext=name.Substring(name.LastIndexOf('.'));
            string token = SimpleEncryptor.MD5(name + size);
            uploadToken.name = name;
            uploadToken.size = size.ToInt(0);
            uploadToken.token = token;

            if (!File.Exists(_server.MapPath(_tokenPath+token+".token")))
            {
                string modified = _request.QueryString["modified"];

                uploadToken.filePath = "";
                uploadToken.modified = modified;

                SetTokenInfo(token, uploadToken);
            }

            TokenResult tokenResult = new TokenResult();
            tokenResult.message = "";
            tokenResult.token = token;
            tokenResult.success = true;

            string result = JsonHelper.SerializeObject(tokenResult); ;

            _response.Write(result);
        }

        private void SetTokenInfo(string token,UploadToken uploadToken)
        {
            _fileHelper.FileName = token + ".token";
            _fileHelper.FilePath = _server.MapPath(_tokenPath);
            _fileHelper.WriteFile(JsonHelper.SerializeObject(uploadToken));
        }

        private UploadToken GetTokenInfo(string token)
        {
            string tokenPath = _tokenPath + token + ".token";
            if(File.Exists(_server.MapPath(tokenPath)))
            {
                _fileHelper.FileName = token + ".token";
                _fileHelper.FilePath = _server.MapPath(_tokenPath);
                UploadToken uploadToken = JsonHelper.DeserializeJsonToObject<UploadToken>(_fileHelper.ReadFile());

                return uploadToken;
            }

            return null;
        }

        private void DelTokenInfo(string token)
        {
            string tokenPath = _tokenPath + token + ".token";
            if (File.Exists(_server.MapPath(tokenPath)))
            {
                _fileHelper.FileName = token + ".token";
                _fileHelper.FilePath = _server.MapPath(_tokenPath);
                _fileHelper.DeleteFile();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}