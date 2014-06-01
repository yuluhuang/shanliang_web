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
        //context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        CoreManager coremanage = new CoreManager();//实例化BLL总管
        userinfo user = new userinfo();
        string flag = getandpost(context, "flag");
        if (flag.Equals("login"))
        {

            try
            {
                user.yonghubianhao = getandpost(context, "username");//用户编号即用户名
                user.mima = getandpost(context, "password");
                string[] aa = coremanage.getUserManage().loginManager(user).Split(','); ;//domainManage.getUserManage()【总管】返回userManageBLL对象【小二】,后调用userManageBLL对象的loginManager方法【沏茶】
                if (aa != null)
                {
                    context.Session["username"] = user.yonghubianhao;
                    context.Session["usernamehash"] = aa[0];
                    context.Session["power"] = aa[1]; ;
                    context.Response.Write("[{\"flag\":" + 1 + ",\"power\":" + context.Session["power"].ToString() + ",\"userName\":\"" + user.yonghubianhao + "\"}]");
                }
                else
                {
                    context.Response.Write(responseCode("0", "0"));//登陆失败
                }
            }
            catch
            {
                context.Response.Write(responseCode("0", "0"));//登陆失败
            }
        }
        else if (flag.Equals("register"))
        {
            if (false)
            {
                user.yonghubianhao = getandpost(context, "username");//用户编号即用户名
                user.mima = getandpost(context, "password");
                user.Email = getandpost(context, "email");
                if (coremanage.getUserManage().registerManage(user))
                {
                    context.Response.Write(responseCode("1", "1"));
                }
                else
                {
                    context.Response.Write(responseCode("0", "1"));
                }
            }
        }
        else if (flag.Equals("islogin"))
        {
            if (context.Session["username"] != null)
            {

                context.Response.Write("[{\"flag\":" + 1 + ",\"power\":" + context.Session["power"].ToString() + ",\"userName\":\"" + context.Session["username"].ToString() + "\"}]");
            }
            else
            {
                context.Response.Write(responseCode("0", "0"));
            }
        }
        else if (flag.Equals("logout"))
        {
            context.Session.RemoveAll();
            context.Response.Write(responseCode("1", "0"));
        }
        else if (flag.Equals("search"))
        {
            string key = getandpost(context, "key");
            string s1 = coremanage.getZuopinBLL().getTasksByKey(key);
            string s2 = coremanage.getNoteManage().getNotesByKey(key);
            if (s1 == null)
            {
                context.Response.Write("[{\"search\":[{" + s2 + "}]}]");
            }
            else
            {
                context.Response.Write("[{\"search\":[{" + s1 + "," + s2 + "}]}]");
            }
        }
    }
}