<%@ WebHandler Language="C#" Class="NoteHandler" %>

using System;
using System.Web;
using System.Web.SessionState;
using UN.Common;
using UN.BLL;
using UN.Model;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class NoteHandler : AbstractHandler, IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        CoreManager coremanage = new CoreManager();//实例化BLL总管
        noteinfo note = new noteinfo();
        string flag = getandpost(context, "flag");
        if (context.Session.SessionID != null && Convert.ToInt32(context.Session["power"]) >= 2)
        {
            string userId = context.Session["usernamehash"].ToString();
            if (flag.Equals("note"))
            {
                try
                {
                    note.noteUrl = getandpost(context, "url");
                    note.noteTitle =getandpost(context, "title");
                    note.noteContent = getandpost(context, "content");
                    note.noteTime = getandpost(context, "time");
                    note.noteTag =  getandpost(context, "tag");
                    note.ispublic = getandpost(context, "ispublic");
                    note.reprint = getandpost(context, "reprint");
                    /*
                    using (var reader = new StreamReader(context.Request.InputStream))
                    {
                        // This will equal to "charset = UTF-8 & param1 = val1 & param2 = val2 & param3 = val3 & param4 = val4"
                        string values = reader.ReadToEnd();
                        JObject jo = (JObject)JsonConvert.DeserializeObject(values);
                        note.noteTitle = jo["title"].ToString(); //getandpost(context, "title");
                        note.noteContent = jo["content"].ToString(); //getandpost(context, "content");
                        note.noteTime = jo["time"].ToString(); //getandpost(context, "time");
                        note.noteTag = jo["tag"].ToString();// getandpost(context, "tag");
                        note.noteUrl = jo["url"].ToString();// getandpost(context, "tag");

                    }
                    */
                    note.userId = userId;

                    //保存笔记
                    if (coremanage.getNoteManage().saveNote(note))
                    {
                        //bool a = true;
                        context.Response.Write(responseCode("1", context.Session["power"].ToString()));
                    }
                    else
                    {
                        //bool a = false;
                        context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                    }
                }
                catch (Exception ex)
                {

                }
            }
                
            //获取笔记集
            else if (flag.Equals("getnote"))
            {
                try
                {
                    string userid = userId;
                    //获得笔记
                    string aa = coremanage.getNoteManage().getALLNotes(userid);
                    if (aa != "")
                    {
                        context.Response.Write("[{" + aa + "}]");
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

            else if (flag.Equals("delnote"))
            {
                try
                {
                    string id = getandpost(context, "id");
                    if (coremanage.getNoteManage().delNote(id) > 0)
                    {
                        context.Response.Write(responseCode("1", context.Session["power"].ToString()));
                    }
                    else
                    {
                        context.Response.Write(responseCode("0",context.Session["power"].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    context.Response.Write(responseCode("0",context.Session["power"].ToString()));
                }
            }

        }
        else
        {
            context.Response.Write(responseCode("0","0"));
        }
    }

}