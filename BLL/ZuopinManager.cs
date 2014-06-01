using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using UN.DAL;
using UN.Model;
using System.Collections;

namespace UN.BLL
{
    public class ZuopinManager
    {
        private zuop_DLL zuopinDAL = new zuop_DLL();
       

        /// <summary>
        /// 创建作品
        /// </summary>
        /// <param name="zuop"></param>
        /// <returns></returns>
        public int insertzuop(zuopinfo zuop)
        {
            return zuopinDAL.insertzuopin_DLL(zuop);
        }
       
        /// <summary>
        /// 删除作品
        /// </summary>
        /// <param name="zp"></param>
        /// <returns></returns>
        public int delectzuopin(zuopinfo zp)
        {
            return zuopinDAL.delectzuopin_DLL(zp);
        }
       
        /// <summary>
        /// 更新作品
        /// </summary>
        /// <param name="zuop"></param>
        /// <returns></returns>
        public int getupdatezuop(zuopinfo zuop)
        {
            return zuopinDAL.getupdatezuop_DLL(zuop);
        }

        //通过活动Id获取作品集合
        public string getZuopinById(string themeId)
        {
            return zuopinDAL.getZuoPinInfo(themeId);
        }
        /// <summary>
        /// 获得以sort列排序最大的作品
        /// </summary>
        /// <param name="id_zuop"></param>
        /// <returns></returns>
        public int selectMaxSort(string id_zuop)
        {
            return zuopinDAL.getMax(id_zuop);
        }
        /// <summary>
        /// 更新task图标
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public int updateTaskIcon(string taskId,string path)
        {
            return zuopinDAL.updateTaskIcon(taskId,path);
        }
        /// <summary>
        /// 获取task详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string getTaskInfoById(string id)
        {
            return zuopinDAL.getTaskInfo(id);
        }

        //更新task
        public int updatetask(zuopinfo zp)
        {
            return zuopinDAL.updateTaskInfo(zp);
        }

        /// <summary>
        /// 搜索通过key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string getTasksByKey(string key)
        {
            return zuopinDAL.getTasksByKey(key);
        }
    }
}
