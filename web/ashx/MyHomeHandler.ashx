<%@ WebHandler Language="C#" Class="LoginHandler" %>

using System;
using System.Web;
using System.Web.SessionState;
using UN.Common;
using UN.BLL;
using UN.Model;

public class LoginHandler : AbstractHandler, IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        CoreManager coremanage = new CoreManager();//实例化BLL总管
        string flag = getandpost(context, "flag");
        if (context.Session.SessionID != null && Convert.ToInt32(context.Session["power"]) >= 2)
        {  
            string userId = context.Session["usernamehash"].ToString();
            //myhome用户模型
            if (flag.Equals("myhome"))
            {
                string addAll = "";
                try
                {
                    string username = coremanage.getUserManage().userInfo(userId);// 用户信息user:[{...}]
                    string xx = username.Substring(0, username.LastIndexOf("}"));
                    string huodong = coremanage.getHuoDongBLL().getHuodongById(userId, "");//用户的活动集合活动:[{...}]
                    if (huodong != "")
                    {
                        addAll = "[{" + xx + "," + huodong + "}]}]";
                    }
                    else
                    {
                        addAll = "[{" + username + "}]";
                    }
                    context.Response.Write(addAll);
                }
                catch
                {
                    context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                }
            }
            else if (flag.Equals("zuopinfo"))
            {
                try
                {
                    string themeId = getandpost(context, "id");
                    string zuopin = coremanage.getZuopinBLL().getZuopinById(themeId) ;
                    if (zuopin != "")
                    {
                        context.Response.Write("[{" + zuopin + "}]");
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
            //删除活动
            else if (flag.Equals("deleteTheme"))
            {
                huodinfo huod = new huodinfo();
                huod.huodId = Convert.ToInt32(getandpost(context, "id"));
                int a = coremanage.getHuoDongBLL().deletehuodong(huod);
                if (a == 1)
                {
                    context.Response.Write(responseCode("1", context.Session["power"].ToString()));
                }
                else {
                    context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                }
            }

            //删除作品
            else if (flag.Equals("deleteTask"))
            {
                zuopinfo zp = new zuopinfo();
                zp.zuopId = Convert.ToInt32(getandpost(context, "id"));

                int a = coremanage.getZuopinBLL().delectzuopin(zp);
                if (a == 1)
                {
                    context.Response.Write(responseCode("1", context.Session["power"].ToString()));
                }
                else
                {
                    context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                }
            }
            else if (flag.Equals("motto"))
            {
                string motto = getandpost(context, "motto");
                int a = coremanage.getUserManage().updateMotto(motto, userId);
                if (a == 1)
                {
                    context.Response.Write(responseCode("1", context.Session["power"].ToString()));
                }
                else
                {
                    context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                }
            }
        }
        else
        {
        }

    }
}