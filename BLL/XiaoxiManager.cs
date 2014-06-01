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
    public class XiaoxiManager
    {
        xiaox_DLL xiaoxiDLL = new xiaox_DLL();

        /// <summary>
        /// 筛选未读消息
        /// </summary>
        /// <param name="xiaoxi"></param>
        public DataTable selectxiaoxi(xiaoxinfo xiaoxi)
        {
            return   xiaoxiDLL.getselectxiaoxi(xiaoxi);
        }
        /// <summary>
        /// 筛选所有消息
        /// </summary>
        /// <param name="xiaoxi"></param>
        public string  selectxiaoxiLine(string userId)
        {
            return xiaoxiDLL.getselectxiaoxiLine_DLL(userId);
           
        }
        /// <summary>
        /// 让消息变为已读/未读
        /// </summary>
        /// <param name="xiaoxi"></param>
        /// <returns></returns>
        public int  updateYiDuXiaoXi(xiaoxinfo xiaoxi)
        {
           return  xiaoxiDLL.getupdateyidu_DLL(xiaoxi);
        }
        /// <summary>
        /// 插入消息表[人物ID]
        /// </summary>
        /// <param name="xiaoxi"></param> 
        public int getshoucangxiaoxi(xiaoxinfo xx, string shoucangzheyonghubianhao)
        {
            return xiaoxiDLL.getshoucangxiaoxi_DLL(xx, shoucangzheyonghubianhao);
        }
        /// <summary>
        ///  插入消息表[作品ID]
        /// </summary>
        /// <param name="xx"></param>
        /// <param name="shoucangzheyonghubianhao"></param>
        /// <param name="xiaozucy"></param>
        /// <returns></returns>
        public int getshoucangxiaoxi(xiaoxinfo xx,shoucinfo sc, int yonghuid, string[] xiaozucy)
        {
            return xiaoxiDLL.getshoucangxiaoxi_zuop_DLL(xx,sc, yonghuid, xiaozucy);
        }
        /// <summary>
        /// 插入消息表[活动ID]
        /// </summary>
        /// <param name="sc"></param>
        /// <returns></returns>
        public int getshoucangxiaoxi(shoucinfo sc,int yonghuid,string huodongfaqiren)
        {
            return xiaoxiDLL.getshoucangxiaoxi_huod_DLL(sc, yonghuid,huodongfaqiren);
        }

        public int getxiaoxiwhereweidu(string id)
        {
            return xiaoxiDLL.getxiaoxiwhereweidu_DLL(id);
        }
    }
}
