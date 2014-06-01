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
        huodinfo hd = new huodinfo();
        string flag = getandpost(context, "flag");
        if (context.Session.SessionID != null &&Convert.ToInt32(context.Session["power"])>=2)
        {
             hd.yonghubianhao= context.Session["usernamehash"].ToString();
            //获取信息
            if (flag.Equals("inserttheme") || flag.Equals("updatetheme"))
            {
                try
                {
                    int a = 0;
                    hd.huodId =Convert.ToInt32(getandpost(context, "themeID"));
                    hd.huodName= getandpost(context, "themeName");
                    hd.huodContext= getandpost(context, "remark");
                    hd.leibie = getandpost(context, "category");

                    if (flag.Equals("inserttheme"))
                    {
                        //创建主题
                        a = coremanage.getHuoDongBLL().createThemeGetID(hd);
                    }
                    else
                    {
                        //更新作品
                        a = coremanage.getHuoDongBLL().updateTheme(hd);
                        a = hd.huodId;

                    }
                    context.Response.Write("[{\"id\":" + a + "}]");

                }
                catch (Exception ex)
                {
                    context.Response.Write(responseCode("0",context.Session["power"].ToString()));
                }
            }
            //获取themeinfo
            else if (flag.Equals("getthemeinfobyid"))
            {
                try
                {

                    string id = getandpost(context, "id");
                    string ss = coremanage.getHuoDongBLL().getThemeInfoById(id);
                    if (ss.Length != 0)
                    {
                        context.Response.Write("[{" + ss + "}]");
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
            else if (flag.Equals("cutpic"))
            {
                try
                {
                    string id = getandpost(context, "id");
                    if (getandpost(context, "x") != "")
                    {
                        int x = int.Parse(getandpost(context, "x"));
                        int y = int.Parse(getandpost(context, "y"));
                        int w = int.Parse(getandpost(context, "w"));
                        int h = int.Parse(getandpost(context, "h"));
                        string filename = getandpost(context, "filepath");//="uploads/z/文件名" 
                        string aa = filename.Substring(filename.LastIndexOf('/') + 1);//文件名+jpg

                        if (coremanage.getHuoDongBLL().updateThemeIcon(id, "uploads/z/" + aa) == 1)
                        {
                            string filepath = Server.MapPath("~/") + filename;
                            Crop(filepath, w, h, x, y, aa);
                            context.Response.Write(responseCode("1", context.Session["power"].ToString()));
                        }
                        else
                        {
                            context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                        }

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