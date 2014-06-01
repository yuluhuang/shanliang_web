<%@ WebHandler Language="C#" Class="playerHandler" %>

using System;
using System.Web;
using System.Web.SessionState;
using UN.Common;
using UN.BLL;
using UN.Model;

public class playerHandler : AbstractHandler, IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        CoreManager coremanage = new CoreManager();//实例化BLL总管
        zuopinfo zp = new zuopinfo();
        string flag = getandpost(context, "flag");
        if (context.Session.SessionID != null && Convert.ToInt32(context.Session["power"]) >= 2)
        {
            string userId = context.Session["usernamehash"].ToString();
            //获取task
            if (flag.Equals("getplayer"))
            {
                try
                {
                    string taskid = getandpost(context, "id");
                    string ss=  coremanage.getTiaoMuBLL().getItems(taskid);
                    if (ss != "")
                    {
                        context.Response.Write("[{" + ss + "}]");
                    }
                    else {
                        context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                    
                    }

                }
                catch (Exception ex)
                {
                    context.Response.Write(responseCode("1", context.Session["power"].ToString()));
                }
            }
        }
        else
        {
            context.Response.Write(responseCode("0","0"));
        }
    }

}