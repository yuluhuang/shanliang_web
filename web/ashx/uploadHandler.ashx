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
        zuopinfo zp = new zuopinfo();
        string flag = getandpost(context, "flag");
        if (context.Session.SessionID != null && Convert.ToInt32(context.Session["power"]) >= 2)
        {
            string userId = context.Session["usernamehash"].ToString();
            //获取信息
            if (flag.Equals("inserttask") || flag.Equals("updatetask"))
            {
                try
                {
                    int a = 0;
                    zp.zuopName = getandpost(context, "taskName");
                    zp.zuopbeizhu = getandpost(context, "remark");
                    zp.leibie = getandpost(context, "category");
                    zp.shijain = getandpost(context, "time");
                    zp.huodId = Convert.ToInt32(getandpost(context, "themeID"));
                    zp.zuopId = Convert.ToInt32(getandpost(context, "taskID"));
                    //创建主题
                    if (zp.huodId == 0)
                    {
                        huodinfo theme = new huodinfo();
                        theme.huodName = getandpost(context, "themeName");
                        theme.yonghubianhao = userId;
                        zp.huodId = coremanage.getHuoDongBLL().createThemeGetID(theme);
                    } 
                    if (flag.Equals("inserttask"))
                    {
                        //创建作品
                        a = coremanage.getZuopinBLL().insertzuop(zp);
                    }
                    else
                    {
                        //更新作品
                        a = coremanage.getZuopinBLL().updatetask(zp);

                    }
                    context.Response.Write("[{\"id\":" + a + "}]");

                }
                catch (Exception ex)
                {
                    context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                }
            }
            //获取items
            else if (flag.Equals("getItems"))
            {
                try
                {

                    string id = getandpost(context, "id");
                    string ss = coremanage.getTiaoMuBLL().getItems(id);
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
            //删除条目
            else if (flag.Equals("delItem"))
            {
                try
                {
                    string id = getandpost(context, "id");
                    if (coremanage.getTiaoMuBLL().delete_tm(id) == 1)
                    {
                        context.Response.Write(responseCode("1", context.Session["power"].ToString()));
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
            //提交修改
            else if (flag.Equals("modifyItem"))
            {
                try
                {
                    string id = getandpost(context, "id");
                    string title = getandpost(context, "title");
                    string remark = getandpost(context, "remark");

                    //更新item介绍
                    if (coremanage.getTiaoMuBLL().updateItem(id, title, remark) == 1)
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

                        if (coremanage.getZuopinBLL().updateTaskIcon(id, "uploads/z/" + aa) == 1)
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
            //获得用户创建的所有theme
            else if (flag.Equals("selecttheme"))
            {
                try
                {
                    string key = getandpost(context, "key");

                    string ss = coremanage.getHuoDongBLL().getHuodongByKey(userId, key);
                    context.Response.Write("[{" + ss + "}]");
                }
                catch
                {
                    context.Response.Write(responseCode("0", context.Session["power"].ToString()));
                }
            }
            else if (flag.Equals("gettaskinfobyid"))
            {
                try
                {
                    string id = getandpost(context, "id");
                    string ss = coremanage.getZuopinBLL().getTaskInfoById(id);
                    context.Response.Write("[{" + ss + "}]");
                }
                catch (Exception ex)
                {
                    context.Response.Write(responseCode("0", context.Session["power"].ToString()));

                }
            }
        }
        else
        {
            context.Response.Write(responseCode("0","0"));
        }
    }
}