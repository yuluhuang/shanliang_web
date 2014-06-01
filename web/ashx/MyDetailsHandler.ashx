<%@ WebHandler Language="C#" Class="MyDetailsHandler" %>

using System;
using System.Web;
using System.Web.SessionState;
using UN.Common;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using UN.BLL;
using UN.Model;

public class MyDetailsHandler : AbstractHandler, IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        CoreManager coremanage = new CoreManager();//实例化BLL总管
        userinfo user = new userinfo();
        string flag = getandpost(context, "flag");
        if (context.Session.SessionID != null && Convert.ToInt32(context.Session["power"]) >= 2)
        {
            string userId = context.Session["usernamehash"].ToString();
            //修改基本信息
            if (flag.Equals("modifyinfo"))
            {
                try
                {
                    user.yonghubianhao = userId;
                    user.name = getandpost(context, "name");
                    user.Email = getandpost(context, "email");
                    user.jianjie = getandpost(context, "introduction");
                    if (coremanage.getUserManage().modifyInfo(user) == 1)
                    {
                        context.Response.Write(responseCode("1", context.Session["power"].ToString()));
                    }
                    else
                    {
                        context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    context.Response.Write("false");
                }
            }
            //获取用户信息
            else if (flag.Equals("getuserinfo"))
            {
                try
                {
                    context.Response.Write("[{" + coremanage.getUserManage().userInfo(userId) + "}]");
                }
                catch (Exception ex)
                {
                    context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                }

            }
            //修改密码
            else if (flag.Equals("updatepassword"))
            {
                try
                {
                    string password = getandpost(context, "password");
                    if (coremanage.getUserManage().updatePassword(userId, password) == 1)
                    {
                        context.Response.Write(responseCode("1", context.Session["power"].ToString()));
                    }
                    else
                    {
                        context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                }

            }
            else if (flag.Equals("cutpic"))
            {
                try
                {
                    if (getandpost(context, "x") != "")
                    {
                        int x = int.Parse(getandpost(context, "x"));
                        int y = int.Parse(getandpost(context, "y"));
                        int w = int.Parse(getandpost(context, "w"));
                        int h = int.Parse(getandpost(context, "h"));
                        string filename = getandpost(context, "filepath");//="uploads/z/文件名" 
                        string aa =filename.Substring(filename.LastIndexOf('/') + 1);//文件名+jpg

                        if (coremanage.getUserManage().setIcon(userId, "uploads/z/"+ aa) == 1)
                        {
                            string filepath = Server.MapPath("~/") + filename;
                            Crop(filepath, w, h, x, y, aa);
                        
                            context.Response.Write(responseCode("1",context.Session["power"].ToString()));
                        }
                        else
                        {
                            context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                }
            }
        }
        else
        {
            context.Response.Write(responseCode("0", context.Session["power"].ToString()));
        }
    }

}