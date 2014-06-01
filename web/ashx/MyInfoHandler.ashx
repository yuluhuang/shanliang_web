<%@ WebHandler Language="C#" Class="MyInfoHandler" %>

using System;
using System.Web;
using System.Web.SessionState;
using UN.Common;
using UN.BLL;
using UN.Model;

public class MyInfoHandler : AbstractHandler, IHttpHandler, IRequiresSessionState
{

   public  void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        CoreManager coremanage = new CoreManager();//实例化BLL总管
        userinfo user = new userinfo();
        string flag = getandpost(context, "flag");
        if (context.Session.SessionID != null && Convert.ToInt32(context.Session["power"]) >= 2)
        {
            string userId = context.Session["usernamehash"].ToString();
            //获取信息
            if (flag.Equals("getinfo"))
            {
                try
                {
                  string info= coremanage.getXiaoxiBLL().selectxiaoxiLine(userId);
                  if (info!= "")
                  {
                      context.Response.Write("[{"+info+"}]");
                  }
                  else {
                      context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                  }
                }
                catch (Exception ex)
                {
                    context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                }
            }
            //设置信息
            else if (flag.Equals("setinfo"))
            {
                try
                {
                    xiaoxinfo info = new xiaoxinfo();
                    info.Id =Int32.Parse(getandpost(context, "id"));
                    info.yidu = Boolean.Parse(getandpost(context, "read"));
                    if (coremanage.getXiaoxiBLL().updateYiDuXiaoXi(info) == 1)
                    {
                        context.Response.Write(responseCode("1", context.Session["power"].ToString()));
                    }
                    else {
                        context.Response.Write(responseCode("0", context.Session["power"].ToString()));
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
            context.Response.Write(responseCode("0", "0"));
        }
    }

}