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
    public class ShoucangManager
    {
        shouc_DLL scDAL = new shouc_DLL();
       // PROC_DLL Bll = new PROC_DLL();
        /// <summary>
        /// insertCollect 插入收藏信息
        /// </summary>
        /// <param name="sc"></param>
        public int insertCollect(shoucinfo sc)
        {
            return scDAL.insertCollect(sc);
        }

        public int updateshouchang(shoucinfo sc)
        {
            return scDAL.updateCollectInfo(sc);

        }
        /// <summary>
        /// 收藏人物
        /// </summary>
        /// <param name="sc"></param>
        /// <returns></returns>
        public int getshoucangrenwu(shoucinfo sc, int yonghuid)
        {
            return scDAL.getshoucangrenwu_DLL(sc, yonghuid);
        }
        /// <summary>
        /// 收藏作品
        /// </summary>
        /// <param name="sc"></param>
        /// <returns></returns>
        public int getshoucangzuopin(shoucinfo sc)
        {
            return scDAL.getshoucangzuopin_DLL(sc);
        }

        public int getshoucanghuodong(shoucinfo sc)
        {
            return scDAL.getshoucanghuodong_DLL(sc);
        }
        //获得用户收藏信息
        public string getCollectInfoList(string userbianhao)
        {
            return scDAL.getCollectInfos(userbianhao);
        }

        public int deleteshouchang(string id)
        {
            return scDAL.deleteCollect(id);
        }
    }
}
