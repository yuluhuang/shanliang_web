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
        //每次获取15条
        if (flag.Equals("getNotes"))
        {
            string userid = "c4ca4238a0b923820dcc509a6f75849b"; 
            int page = Convert.ToInt32(getandpost(context, "count"));
            int row = 12;
            //获得笔记
            string aa =coremanage.getNoteManage().getNoteList(userid,page,row);

            context.Response.Write("[{\"noteSearch\":[{" + aa + "}]}]");

        }
        else if (flag.Equals("getNoteByNoteId")) {
            int noteid = Convert.ToInt32(getandpost(context, "noteId"));
            string aa = coremanage.getNoteManage().getNotesByNoteId(noteid);
            context.Response.Write("[{" + aa +"}]");
        }

    }
}