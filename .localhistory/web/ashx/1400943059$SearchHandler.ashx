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
        userinfo user = new userinfo();
        string flag = getandpost(context, "flag");

        if (flag.Equals("noteSearch"))
        {
            string key = getandpost(context, "key");

            string s2 = coremanage.getNoteManage().getNotesByKey(key);

            context.Response.Write("[{\"noteSearch\":[{" + s2 + "}]}]");

        }
    }
}