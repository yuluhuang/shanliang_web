<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using UN.Model;
using UN.BLL;
using UN.DAL;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Collections.Generic;

public class Handler : AbstractHandler, IHttpHandler, IRequiresSessionState//获取session需要IRequiresSessionState
{
    //DB_DLL db = new DB_DLL();
    DataBase db = new DataBase();
    CoreManager BLL = CoreManager.getInstance();


    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string flag = getandpost(context, "flag");
        //if (context.Session["isloginid"] != null)
        if (context.Session.SessionID!=null)
        {
            //添加作品
            if (flag.Equals("zpsc_1"))
            {
                zuopinfo zuop = new zuopinfo(); int ok = 1;
                zuop.zuopName = getandpost(context, "zp_title");
                zuop.zuopbeizhu = getandpost(context, "zp_context");

                string huodname = getandpost(context, "id_huod");//获得活动名

                ok = 0;// BLL.getHuoDongBLL().ishuodong(huodname);//通过活动名判断是否存在活动 返回活动ID
                if (ok > 0)//if存在活动
                {
                    zuop.huodId = ok;
                    zuop.leibie = getandpost(context, "leibie");
                    string tuanti_s = getandpost(context, "tuanti_s");
                    string tuanti_t = getandpost(context, "tuanti_t");
                    string[] tuanti_s_id = tuanti_s.Split(',');
                    string[] tuanti_t_id = tuanti_t.Split(',');

                    int b = BLL.getZuopinBLL().insertzuop(zuop);//返回作品编号
                   // this.BLL.getZuopinBLL().insertzuop2(b, tuanti_s_id, tuanti_t_id);
                    //this.BLL.getPinFenBLL().getinsertpinfen(b);
                    context.Response.Write("true," + b);
                }
                else
                {
                    context.Response.Write("false");
                }
            }

            if (flag.Equals("zpsc_3"))//更新tm
            {
                tiaominfo tm = new tiaominfo();
                tm.tmId = Convert.ToInt32(getandpost(context, "tm_id"));
                tm.zhuti = getandpost(context, "tm_title");
                tm.jieshao = getandpost(context, "tm_context");
                //int a = BLL.getTiaoMuBLL().inserttm(tm);
                //if (a == 1)
                //    context.Response.Write("true");
            }
            if (flag.Equals("tuozhuai"))
            {
                tiaominfo tm = new tiaominfo();
                tm.zuopId = Convert.ToInt32(getandpost(context, "id_zuop"));
                string[] tuoz = getandpost(context, "string").Split(',');
                BLL.getTiaoMuBLL().updatetmPaixuline(tm, tuoz);//拖拽 更新排序列
            }

            if (flag.Equals("delete_tm"))//tm
            {
                tiaominfo tm = new tiaominfo();
                tm.tmId = Convert.ToInt32(getandpost(context, "tm_id"));
                //int a = BLL.getTiaoMuBLL().delete_tm(tm);
                //if (a > 0)
                //    context.Response.Write("true");
            }

            if (flag.Equals("upload_tm"))//是否可下载
            {
                string tm_id = getandpost(context, "tm_id");
                string b = context.Request.QueryString["b"].ToString();

                string sql = string.Format("update 作品条目表 set 下载={0} where 条目ID={1}", b, tm_id);
               // int a = db.getcomm(sql);
            }
            if (flag.Equals("editzuop_zx"))///
            {
                zuopinfo zuop = new zuopinfo(); int ok = 1;
                zuop.zuopId = Convert.ToInt32(getandpost(context, "id_zuop"));
                zuop.zuopName = getandpost(context, "zp_title");
                zuop.zuopbeizhu = getandpost(context, "zp_context");
                string huodname = getandpost(context, "id_huod");//获得活动名

                ok = 0;// BLL.getHuoDongBLL().ishuodong(huodname);//通过活动名判断是否存在活动 返回活动ID
                if (ok > 0)//if存在活动
                {
                    zuop.huodId = ok;
                    zuop.leibie = getandpost(context, "leibie");
                    int b = BLL.getZuopinBLL().getupdatezuop(zuop);
                    if (b > 0)
                        context.Response.Write("true");
                }
                else
                {
                    context.Response.Write("false");
                }
            }
            if (flag.Equals("edithuod_zx"))///编辑活动,更新活动表
            {
                huodinfo hd = new huodinfo();
                hd.huodName = getandpost(context, "addhuod_name");
                hd.huodContext = getandpost(context, "addhuod_context");
               // hd.startTime = DateTime.Parse(getandpost(context, "startTime"));
               // hd.endTime = DateTime.Parse(getandpost(context, "endTime"));
                hd.huodId = Convert.ToInt32(getandpost(context, "huod_id"));
                //if (BLL.getLoginService(context.Session).islogon())
                //{
                //    hd.yonghubianhao = context.Session["isloginid"].ToString();
                //    int a = BLL.getHuoDongBLL().getgenxinhuodong(hd);
                //    if (a > 0)
                //        context.Response.Write("true");
                //}
            }

            if (flag.Equals("addhuod_zx"))
            {
                huodinfo hd = new huodinfo();
                hd.huodName = getandpost(context, "addhuod_name");
                hd.huodContext = getandpost(context, "addhuod_context");
               // hd.startTime = DateTime.Parse(getandpost(context, "startTime"));
                //hd.endTime = DateTime.Parse(getandpost(context, "endTime"));
                hd.leibie = getandpost(context, "leibieid");
                int a = 0;
                hd.yonghubianhao = context.Session["isloginid"].ToString();
                a = 0;// BLL.getHuoDongBLL().gethuodong(hd);

                if (a > 0)
                    context.Response.Write(a);
            }

            if (flag.Equals("delete_huod"))
            {
                huodinfo huod = new huodinfo();
                huod.huodId = Convert.ToInt32(getandpost(context, "huod_id"));
                int a = BLL.getHuoDongBLL().deletehuodong(huod);
                if (a == 1)
                {
                    context.Response.Write("true");
                }

            }
            if (flag.Equals("delete_zuop"))
            {
                zuopinfo zp = new zuopinfo();
                zp.zuopId = Convert.ToInt32(getandpost(context, "zuop_id"));
                //LoginManager logservice = BLL.getLoginService(context.Session);
                //int a = BLL.getZuopinBLL().delectzuopin(zp);//
                //if (a == 1)
                //{
                //    context.Response.Write("true");
                //}
            }
            if (flag.Equals("qianmmd"))
            {
                string qianmmd_text = getandpost(context, "qianmmd_text");
                string sql = string.Format("update 用户表 set 签名档='{0}' where 用户编号='{1}'", qianmmd_text, context.Session["isloginid"].ToString());
                //int a = db.getcomm(sql);
                //if (a >= 1)
                //{
                //    context.Response.Write("true");
                //}
            }
            if (flag.Equals("setXiaoXi"))//更新已读消息
            {
                xiaoxinfo xiaoxi = new xiaoxinfo();
                bool a = false;
                xiaoxi.Id = Convert.ToInt32(getandpost(context, "xiaoxi_id"));
                if (context.Request.Form["yidu"].ToString() == "1") a = true;
                xiaoxi.yidu = a;
                BLL.getXiaoxiBLL().updateYiDuXiaoXi(xiaoxi);
            }
            if (flag.Equals("xiaoxiLine"))// 筛选所有消息
            {
                xiaoxinfo xiaoxi = new xiaoxinfo();
                xiaoxi.yonghubianhao = "1";
               // DataTable dt = BLL.getXiaoxiBLL().selectxiaoxiLine(xiaoxi);
                context.Response.Write(1);
            }

            if (flag.Equals("xiugaimima"))
            {
                xiugaimima(context);
                // if (BLL.getLoginService(context.Session).islogon())
                //{  }
            }

            if (flag.Equals("updatejianjie"))
            {
                updatejianjie(context);
            }
            if (flag.Equals("sczx_insert_sc"))
            {
                //LoginManager logservice = BLL.getLoginService(context.Session);
                shoucinfo shouc = new shoucinfo();
                shouc.yonghubianhao = context.Session["isloginid"].ToString(); //logservice.getCurrentUserId();
                shouc.lujing = getandpost(context, "sczx_url");
                shouc.tmName = getandpost(context, "sczx_biaoti");
                shouc.miaoshu = getandpost(context, "sczx_miaoshu");
              //  shouc.shijian = DateTime.Now;
                BLL.getShouChangBLL().insertCollect(shouc);
            }

            if (flag.Equals("update_sc"))
            {
                shoucinfo sc = new shoucinfo();
                sc.shoucId = Convert.ToInt32(getandpost(context, "sc_id"));
                sc.tmName = getandpost(context, "sc_title");
                sc.miaoshu = getandpost(context, "sc_miaoshu");
                sc.lujing = getandpost(context, "sc_URL");
                if (BLL.getShouChangBLL().updateshouchang(sc) == 1)
                {
                    context.Response.Write("true");
                }
            }
            if (flag.Equals("delete_sc"))
            {
                string sc_id = getandpost(context, "sc_id");
                string sql = string.Format("delete from  收藏表  where 收藏ID={0}", sc_id);
                //int a = db.getcomm(sql);
                //if (a == 1)
                //{
                //    context.Response.Write("删除成功");
                //}
            }
            //收藏
            //if (flag.Equals("soucang"))
            //{
            //    shoucinfo sc = new shoucinfo();
            //    string type = getandpost(context, "type");
            //    sc.yonghubianhao = context.Session["isloginid"].ToString();
            //    // LoginManager logservice = BLL.getLoginService(context.Session);
            //    //sc.yonghubianhao = logservice.getCurrentUserId();//当前用户登录id
            //    if (sc.yonghubianhao == "")
            //    {
            //        context.Response.Write("请登录");
            //    }
            //    else
            //    {
            //        if (type.Equals("renwuid"))
            //        {
            //            sc.renwuId = getandpost(context, "duifangid");
            //            int yonghuid = bianhaoToid(sc.renwuId);
            //            sc.miaoshu = "待编辑";
            //            int a = BLL.getShouChangBLL().getshoucangrenwu(sc, yonghuid);//插入收藏表
            //            xiaoxinfo xx = new xiaoxinfo();
            //            xx.yonghubianhao = sc.renwuId;//用户编号
            //            string shoucangzheyonghubianhao = bianhaoToid(sc.yonghubianhao).ToString();//用户编号->用户id
            //            int b = BLL.getXiaoxiBLL().getshoucangxiaoxi(xx, shoucangzheyonghubianhao);//插入消息表
            //            if (a > 0)
            //            {
            //                context.Response.Write("true");
            //            }
            //        }
            //        else if (type.Equals("zuopinid"))
            //        {
            //            sc.zuopId = Convert.ToInt32(getandpost(context, "zuopid"));//被收藏作品id
            //            string[] zuopin = BLL.getZuopinBLL().getzuopinname(sc).Split(',');
            //            sc.tmName = zuopin[0];
            //            sc.miaoshu = zuopin[1];
            //            int a = BLL.getShouChangBLL().getshoucangzuopin(sc);//插入收藏表
            //            string[] xiaozucy = BLL.getZuopinBLL().getxiaozu(sc).Split(',');//将收到消息的用户编号
            //            int yonghuid = bianhaoToid(sc.yonghubianhao);
            //            xiaoxinfo xx = new xiaoxinfo();
            //            int b = BLL.getXiaoxiBLL().getshoucangxiaoxi(xx, sc, yonghuid, xiaozucy);//插入消息表
            //            if (a > 0)
            //            {
            //                context.Response.Write("true");
            //            }
            //        }
            //        else if (type.Equals("huodongid"))
            //        {
            //            sc.huodId = Convert.ToInt32(getandpost(context, "huodid"));
            //            string huodong = BLL.getHuoDongBLL().gethuodongname(sc);//得到活动名称
            //            sc.tmName = huodong;
            //            string huodongfaqiren = BLL.getHuoDongBLL().gethuodongfaqiren(sc);//活动发起人编号
            //            int yonghuid = bianhaoToid(huodongfaqiren);
            //            int a = BLL.getShouChangBLL().getshoucanghuodong(sc);//插入收藏表
            //            int b = BLL.getXiaoxiBLL().getshoucangxiaoxi(sc, yonghuid, huodongfaqiren);//插入消息表
            //            if (a > 0)
            //            {
            //                context.Response.Write("true");
            //            }
            //        }
            //    }
            //}
            
            //if (flag.Equals("add_pingwei"))
            //{
            //    pinfenbiaozhuninfo bz = new pinfenbiaozhuninfo();
            //    string[] addpingweiid = getandpost(context, "addpingweiid").Split(',');
            //    bz.zhanlanid = Convert.ToInt32(getandpost(context, "huodid"));
            //    for (int i = 0; i < addpingweiid.Length - 1; i++)
            //    {
            //        int b = BLL.getHuoDongBLL().getinsertpinfen(bz, bianhaoToid(addpingweiid[i]));
            //    }
            //    bz.bianzhun1 = getandpost(context, "xijie1");
            //    bz.bianzhun2 = getandpost(context, "xijie2");
            //    bz.bianzhun3 = getandpost(context, "xijie3");
            //    bz.bianzhun4 = getandpost(context, "xijie4");
            //    bz.bili1 = float.Parse(getandpost(context, "fenzhi1"));
            //    bz.bili2 = float.Parse(getandpost(context, "fenzhi2"));
            //    bz.bili3 = float.Parse(getandpost(context, "fenzhi3"));
            //    bz.bili4 = float.Parse(getandpost(context, "fenzhi4"));
            //    int a = BLL.getHuoDongBLL().getinsertbz(bz);
            //    if (a > 0)
            //    {
            //        context.Response.Write("true");
            //    }
            //    else
            //    {
            //        context.Response.Write("false");
            //    }
            //}
            if (flag.Equals("ishuodongming"))
            {
                int ok;
                string a = getandpost(context, "hd_name");
                ok = 0;// BLL.getHuoDongBLL().ishuodong(a);
                if (ok != 0)
                {
                    context.Response.Write("false");
                }
                else
                {
                    context.Response.Write("true");
                }
            }
        }
        else
        {
            context.Response.Write("请登录");
        }
    }

    public void updatejianjie(HttpContext context)
    {
        //LoginManager logservice = BLL.getLoginService(context.Session);
        //string yonghuid = logservice.getCurrentUserId();
        //string jianjie = getandpost(context, "jianjie");
        //if (logservice.updatejiianjie(yonghuid, jianjie))
        //{
        //    context.Response.Write("true");
        //}
        //else
        //{
        //    context.Response.Write("false");
        //}
    }

    public void xiugaimima(HttpContext context)
    {
        //LoginManager logservice = BLL.getLoginService(context.Session);
        //string yonghuid = logservice.getCurrentUserId();
        //string jiumima = getandpost(context, "jiumima");
        //string xinmima = getandpost(context, "xinmima");
        //if (logservice.updatePassword(yonghuid, jiumima, xinmima))
        //{
        //    context.Response.Write("true");
        //}
        //else
        //{
        //    context.Response.Write("false");
        //}
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}