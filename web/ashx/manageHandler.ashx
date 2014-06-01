<%@ WebHandler Language="C#" Class="uploadHandler" %>

using System;
using System.Web;
using System.Web.SessionState;
using UN.Common;
using UN.BLL;
using UN.Model;

public class uploadHandler : AbstractHandler, IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        CoreManager coremanage = new CoreManager();//实例化BLL总管
        string flag = getandpost(context, "flag");
        int page = Convert.ToInt32(getandpost(context, "page"));
        int rows = Convert.ToInt32(getandpost(context, "rows"));
        if (context.Session.SessionID != null && Convert.ToInt32(context.Session["power"]) >= 2)
        {
            try
            {
                string userId = context.Session["usernamehash"].ToString();
                //获取信息
                if (flag.Equals("user"))
                {

                    string ss = coremanage.getUserManage().getUsersForManage(page, rows);
                    if (ss != "")
                    {
                        context.Response.Write(ss);
                    }
                    else
                    {

                        context.Response.Write(responseCode("0",context.Session["power"].ToString()));
                    }

                }
                //manage 获得所有theme
                else if (flag.Equals("theme"))
                {
                    string ss = coremanage.getHuoDongBLL().getAllTheme(page,rows);
                    if (ss != "")
                    {
                        context.Response.Write(ss);
                    }
                    else
                    {

                        context.Response.Write(responseCode("0",context.Session["power"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(responseCode("0", context.Session["power"].ToString()));
            }
        }
        else
        {
            context.Response.Write(responseCode("0", context.Session["power"].ToString()));
        }
    }
}