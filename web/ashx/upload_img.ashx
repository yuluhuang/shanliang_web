<%@ WebHandler Language="C#" Class="upload_img" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using UN.BLL;

public class upload_img :AbstractHandler, IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        CoreManager coremanage = new CoreManager();//实例化BLL总管
        context.Response.ContentType = "text/plain";
        try
        {
            if (context.Session.SessionID != null && Convert.ToInt32(context.Session["power"]) >= 2)
            {
                string userId = context.Session["usernamehash"].ToString();
                string type = "";
                string newname = "";
                string newpath = "";
                string sfile;

                string path = HttpContext.Current.Server.MapPath("~/uploads/");
                string oldpath = path + @"s/";//上传文件夹路径
                if (context.Request.Files.Count > 0)
                {

                    string tempFile = context.Request.PhysicalApplicationPath;

                    for (int j = 0; j < context.Request.Files.Count; j++)
                    {
                        HttpPostedFile uploadFile = context.Request.Files[j];
                        if (uploadFile.ContentLength > 0)
                        {
                            uploadFile.SaveAs(tempFile + "\\uploads\\s\\" + System.IO.Path.GetFileName(uploadFile.FileName));
                            sfile = uploadFile.FileName;//file文件名包括后缀
                            type = sfile.Substring(sfile.LastIndexOf(".")).ToLower();  //获得后缀名.doc

                            newname = Guid.NewGuid().ToString("N");//唯一文件名不包括后缀
                            newpath = path + @"z\" + newname;//路径不包括后缀
                            File.Move(oldpath + sfile, newpath + type);
                            string aa = @"uploads\z\" + newname + type;
                            aa = aa.Replace("\\", "/").Trim();
                            context.Response.Write(aa);
                            //if (coremanage.getUserManage().setIcon(userId, aa) == 1)
                            //{
                            //    context.Response.Write(aa);
                            //}
                            //else
                            //{
                            //    context.Response.Write("");
                            //}
                        }
                    }

                   // HttpContext.Current.Response.Write(" ");
                }
            }
        }
        catch
        {
            context.Response.Write("");
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