using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UN.DAL;
using UN.Model;
using System.Data.SqlClient;
using System.Data;

namespace UN.BLL
{
    public class ZhanlanManager
    {
        private huod_DLL huodongDAL = new huod_DLL();
      
        /// <summary>
        /// 删除活动
        /// </summary>
        /// <param name="huod"></param>
        /// <returns></returns>
        public int deletehuodong(huodinfo huod)
        {
            return huodongDAL.deletehuodong(huod);
        }
   
        /// <summary>
        /// 更新活动表
        /// </summary>
        /// <param name="hd"></param>
        /// <returns></returns>
        public int updateTheme(huodinfo hd)
        {
            return huodongDAL.getupdatehuodong_DLL(hd);
        }
       

        public string getHuodongById(string userbianhao,string key)
        {
            //return huodongDAL.gethuodonginfo(userbianhao ,key);
            return huodongDAL.getThemeInfoByKey(userbianhao, key);
        }



        /// <summary>
        /// 通过用户输入的关键字筛选
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        public string getHuodongByKey(string userId, string key)
        {
            return huodongDAL.getThemeInfoByKey(userId,key);
        }
        /// <summary>
        /// 创建主题返回主题ID
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        public int createThemeGetID(huodinfo theme)
        {
            return huodongDAL.createTheme(theme);
        }

        public string getThemeInfoById(string id)
        {
            return huodongDAL.selectThemeInfo(id);
        }
        /// <summary>
        /// 更新theme
        /// </summary>
        /// <param name="id">themeid</param>
        /// <param name="p">path</param>
        /// <returns></returns>
        public int updateThemeIcon(string id, string p)
        {
            return huodongDAL.updateThemeIcon(id,p);
        }

        /// <summary>
        /// 获取所有主题
        /// </summary>
        /// <returns></returns>
        public string getAllTheme(int page, int rows)
        {
            return huodongDAL.getAllThemes(page,rows);
        }
    }
}
