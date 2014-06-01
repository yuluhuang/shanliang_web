using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UN.DAL;
using UN.Model;
using System.Web.SessionState;

namespace UN.BLL
{
    public class CoreManager
    { 
        ZuopintiaomuManager tmBll = new ZuopintiaomuManager();
        ShoucangManager shouchangBll = new ShoucangManager();
        XiaoxiManager xiaoxiBll = new XiaoxiManager();
        //替换
        //LoginManager loginservice = new LoginManager(); 
        userManageBLL umanager = new userManageBLL();

        ZhanlanManager huodBll = new ZhanlanManager();
        ZuopinManager zuopBill=new ZuopinManager();
        leibieManager leibieBill = new leibieManager();
        //pinglunManager pinlun = new pinglunManager();
       // pingfenManager pinfen = new pingfenManager();
        noteManager note = new noteManager();

        public noteManager getNoteManage() {
            return note;
        }


        private static CoreManager bs = null;
        public static CoreManager getInstance()
        {
            if (bs == null)
            {
                bs = new CoreManager();
            }
            return bs;

        }
        public ZuopintiaomuManager getTiaoMuBLL()
        {
            return tmBll;
        }
        public XiaoxiManager getXiaoxiBLL() {
            return xiaoxiBll;
        }
        public ShoucangManager getShouChangBLL() {
            return shouchangBll;
        }

        //替换
        //public LoginManager getLoginService(HttpSessionState httpSessionState)
        //{
        //    return new LoginManager(httpSessionState);
        //}

        //public LoginManager getJianJieBll()
        //{
        //    return loginservice;
        //}
        public userManageBLL getUserManage() {
            return umanager;
        }

        public ZhanlanManager getHuoDongBLL()
        {
            return huodBll;
        }

        public ZuopinManager getZuopinBLL(){
            return zuopBill;
        }
        public leibieManager getLeibieBLL()
        {
            return leibieBill;
        }
        //public pinglunManager getPinLunBLL()
        //{
        //    return pinlun;
        //}
        //public pingfenManager getPinFenBLL()
        //{
        //    return pinfen;
        //}
    }
}
