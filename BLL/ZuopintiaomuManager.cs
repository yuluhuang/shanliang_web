using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using UN.DAL;
using UN.Model;

namespace UN.BLL
{

    public class ZuopintiaomuManager
    {
        private tiaom_DLL tmDLL = new tiaom_DLL();
        /// <summary>
        /// 插入 
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        //public int inserttm(tiaominfo tm)
        //{
        //    return tmDLL.insert_tm_DLL(tm);
        //}

        //public DataTable selectCurrentTM(tiaominfo tm)
        //{
        //    return tmDLL.selectTMID_DLL(tm);
        //}
        /// <summary>
        /// 更新作品条目表的排序列
        /// </summary>
        /// <param name="tm"></param>
        /// <param name="tuoz"></param>
        public void updatetmPaixuline(tiaominfo tm, string[] tuoz)
        {
            int a = tmDLL.select_for_updatexulie(tm);
            int b=a;
            for (int i = 0; i < tuoz.Length - 1; i++)
            {
                tmDLL.update_paixu_DLL(Convert.ToInt32(tuoz[i])-b+1,a);
                a++;
            }
        }

        /// <summary>
        /// 删除条目
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        public int delete_tm(string id)
        {
            return tmDLL.delete_tm_DLL(id);
        }
        /// <summary>
        /// 获取列表 
        /// </summary>
        /// <param name="wenzhangid"></param>
        /// <param name="leixing"></param>
        /// <returns></returns>
        public string getcaidan(string wenzhangid, string leixing)
        {
            return tmDLL.getcaidan(wenzhangid, leixing);
        }
        /// <summary>
        /// 插入条目表
        /// </summary>
        /// <param name="tm"></param>
        public int insertItem(tiaominfo tm)
        {
            return tmDLL.inserttm(tm);
        }

        /// <summary>
        /// 获取条目列
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string getItems(string id)
        {
            return tmDLL.getItems(id);
        }

        public int updateItem(string id, string title, string remark)
        {
            return tmDLL.updateItem(id, title, remark);
        }
    }
}
