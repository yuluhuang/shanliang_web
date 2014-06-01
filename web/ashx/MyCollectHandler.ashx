<%@ WebHandler Language="C#" Class="MyCollectHandler" %>

using System;
using System.Web;
using System.Web.SessionState;
using UN.Common;
using UN.BLL;
using UN.Model;

public class MyCollectHandler : AbstractHandler, IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        CoreManager coremanage = new CoreManager();//实例化BLL总管
        string flag = getandpost(context, "flag");
        if (context.Session.SessionID != null && Convert.ToInt32(context.Session["power"]) >= 2)
        {
            //myhome用户模型
            if (flag.Equals("mycollect"))
            {
                try
                {
                    string userbianhao = context.Session["usernamehash"].ToString();
                    string collects = coremanage.getShouChangBLL().getCollectInfoList(userbianhao);

                    context.Response.Write("[{" + collects + "}]");

                }
                catch (Exception ex)
                {
                    context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                }
            }
            else if (flag.Equals("insertCollect"))
            {
                try
                {
                    shoucinfo sc = new shoucinfo();
                    sc.yonghubianhao = context.Session["usernamehash"].ToString();
                    sc.tmName = getandpost(context, "title");
                    sc.miaoshu = getandpost(context, "description");
                    sc.lujing = getandpost(context, "url");
                    sc.shijian = getandpost(context, "time");

                    context.Response.Write(coremanage.getShouChangBLL().insertCollect(sc) == 1 ? responseCode("1", context.Session["power"].ToString()) : responseCode("0", context.Session["power"].ToString()));
                }
                catch (Exception ex)
                {
                    responseCode("0", context.Session["power"].ToString());
                }

            }

            else if (flag.Equals("editCollect"))
            {
                try
                {
                    shoucinfo sc = new shoucinfo();
                    sc.shoucId = Convert.ToInt32(getandpost(context, "id"));
                    sc.tmName = getandpost(context, "title");
                    sc.miaoshu = getandpost(context, "description");
                    sc.lujing = getandpost(context, "url");
                    if (coremanage.getShouChangBLL().updateshouchang(sc) == 1)
                    {
                        context.Response.Write(responseCode("1", context.Session["power"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                }

            }
            else if (flag.Equals("deleteCollect"))
            {
                try
                {
                    string id = getandpost(context, "id");

                    if (coremanage.getShouChangBLL().deleteshouchang(id) == 1)
                    {
                        context.Response.Write(responseCode("1", context.Session["power"].ToString()));
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