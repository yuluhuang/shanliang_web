<%@ WebHandler Language="C#" Class="testjson" %>

using System;
using System.Web;

public class testjson : AbstractHandler, IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string flag = getandpost(context, "flag");

        string sID = context.Session.SessionID;
        if (sID!= null)
        {
            if (flag.Equals("person"))
            {
                string s = "{\"person\":{\"address\":\"gd\",\"id\":1001,\"name\":\"jack\"}}";
                context.Response.Write(s);
            }
            else if (flag.Equals("persons"))
            {
                string s = "{\"persons\":[{\"address\":\"gd\",\"id\":1001,\"name\":\"jack\"},{\"address\":\"gx\",\"id\":1002,\"name\":\"ylh\"}]}";
                context.Response.Write(s);
            }
            else if (flag.Equals("liststring"))
            {
                string s = "{\"liststring\":[\"1\",\"2\",\"3\"]}";
                context.Response.Write(s);
            }
            else if (flag.Equals("listmap"))
            {
                string s = "{\"listmap\":[{\"id\":1001,\"address\":\"gd\",\"name\":\"jack\"},{\"id\":1002,\"address\":\"gx\",\"name\":\"ylh\"}]}";
                context.Response.Write(s);
            }
            else if (flag.Equals("shequ"))
            {
                string s = "[{\"id\":1001,\"name\":\"jack\"},{\"id\":1002,\"name\":\"ylh\"}]";
                context.Response.Write(s);
            }
            
        }
        else
        {
            context.Response.Write("false");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}