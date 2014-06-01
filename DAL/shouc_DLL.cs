using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace UN.DAL
{
    public class shouc_DLL : ajxadatelist
    {
        DataBase DB = new DataBase();
        CommandType ct = CommandType.Text;
        CommandType cs = CommandType.StoredProcedure;
        /// <summary>
        /// 收藏人物
        /// </summary>
        /// <param name="sc"></param>
        /// <returns></returns>
        public int getshoucangrenwu_DLL(Model.shoucinfo sc, int yonghuid)
        {
            SqlParameter[] paramer = new SqlParameter[]{
              new SqlParameter("@人物ID",SqlDbType.NVarChar,20),
            };
            paramer[0].Value = sc.renwuId;
            string sqlstr = "select 姓名 from 用户表 where 用户编号=@人物ID";
            sc.tmName = DB.ExecuteScalar(ct, sqlstr, paramer).ToString();

            SqlParameter[] paramers = new SqlParameter[]{
              new SqlParameter("@用户编号",SqlDbType.NVarChar,20), 
              new SqlParameter("@条目名称",SqlDbType.NVarChar,100), 
              new SqlParameter("@描述",SqlDbType.NVarChar,300), 
              new SqlParameter("@路径",SqlDbType.NVarChar,300),
              new SqlParameter("@时间",SqlDbType.DateTime),
              new SqlParameter("@人物ID",SqlDbType.NVarChar,20),
            };
            paramers[0].Value = sc.yonghubianhao;
            paramers[1].Value = sc.tmName;
            paramers[2].Value = "人物";//sc.miaoshu;
            paramers[3].Value = "people.aspx?yonghuid=" + yonghuid;
            paramers[4].Value = DateTime.Now.ToShortTimeString();
            paramers[5].Value = sc.renwuId;
            string sql = " insert into 收藏表 (用户编号,条目名称,描述,路径,时间,人物ID)values(@用户编号,@条目名称,@描述,@路径,@时间,@人物ID)";
            int a = DB.ExecuteNonQuery(ct, sql, paramers);
            return a;
        }
        /// <summary>
        /// 收藏作品
        /// </summary>
        /// <param name="sc"></param>
        /// <returns></returns>
        public int getshoucangzuopin_DLL(Model.shoucinfo sc)
        {
            SqlParameter[] paramers = new SqlParameter[]{
              new SqlParameter("@用户编号",SqlDbType.NVarChar,20), 
              new SqlParameter("@条目名称",SqlDbType.NVarChar,100), 
              new SqlParameter("@描述",SqlDbType.NVarChar,300), 
              new SqlParameter("@路径",SqlDbType.NVarChar,300),
              new SqlParameter("@时间",SqlDbType.DateTime),
              new SqlParameter("@作品ID",SqlDbType.Int),
            };
            paramers[0].Value = sc.yonghubianhao;
            paramers[1].Value = sc.tmName;
            paramers[2].Value = "作品";//sc.miaoshu;
            paramers[3].Value = "zuopin.aspx?wenzhangid=" + sc.zuopId;
            paramers[4].Value = DateTime.Now.ToShortTimeString();
            paramers[5].Value = sc.zuopId;
            string sql = " insert into 收藏表 (用户编号,条目名称,描述,路径,时间,作品ID)values(@用户编号,@条目名称,@描述,@路径,@时间,@作品ID)";
            int a = DB.ExecuteNonQuery(ct, sql, paramers);
            return a;
        }
        /// <summary>
        /// 收藏活动
        /// </summary>
        /// <param name="sc"></param>
        /// <returns></returns>
        public int getshoucanghuodong_DLL(Model.shoucinfo sc)
        {
            SqlParameter[] paramers = new SqlParameter[]{
              new SqlParameter("@用户编号",SqlDbType.NVarChar,20), 
              new SqlParameter("@条目名称",SqlDbType.NVarChar,100), 
              new SqlParameter("@描述",SqlDbType.NVarChar,300), 
              new SqlParameter("@路径",SqlDbType.NVarChar,300),
              new SqlParameter("@时间",SqlDbType.DateTime),
              new SqlParameter("@活动ID",SqlDbType.Int),
            };
            paramers[0].Value = sc.yonghubianhao;
            paramers[1].Value = sc.tmName;
            paramers[2].Value = "活动";//sc.miaoshu;
            paramers[3].Value = "zhanlan.aspx?huodongid=" + sc.huodId;
            paramers[4].Value = DateTime.Now.ToShortTimeString();
            paramers[5].Value = sc.huodId;
            string sql = " insert into 收藏表 (用户编号,条目名称,描述,路径,时间,活动ID)values(@用户编号,@条目名称,@描述,@路径,@时间,@活动ID)";
            int a = DB.ExecuteNonQuery(ct, sql, paramers);
            return a;
        }

        /// <summary>
        /// get collectLists
        /// </summary>
        /// <param name="userbianhao"></param>
        /// <returns></returns>
        public string getCollectInfos(string userbianhao)
        {
            SqlParameter[] paramers = new SqlParameter[]{
              new SqlParameter("@userId",SqlDbType.NVarChar,32), 
            
            };
            paramers[0].Value = userbianhao;

            DataTable dt = DB.getDataTable(cs, "selectCollect", paramers);
            string aa = CreateJsonParameters(dt, "collect");
            return aa;
        }

        /// <summary>
        /// update Collect myCollect
        /// </summary>
        /// <param name="sc"></param>
        /// <returns></returns>
        public int updateCollectInfo(Model.shoucinfo sc)
        {
            try
            {
                SqlParameter[] parme = new SqlParameter[] {
                    new SqlParameter( "@collectID",SqlDbType.Int),        
                    new SqlParameter( "@collectName",SqlDbType.NVarChar,100),  
                    new SqlParameter( "@description",SqlDbType.NVarChar,300),  
                    new SqlParameter( "@URL",SqlDbType.NVarChar,300),
                };
                parme[0].Value = sc.shoucId;
                parme[1].Value = sc.tmName;
                parme[2].Value = sc.miaoshu;
                parme[3].Value = sc.lujing;
                return DB.ExecuteNonQuery(cs, "updateCollectInfo", parme);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int deleteCollect(string id)
        {
            try
            {
                SqlParameter[] parme = new SqlParameter[] {
                    new SqlParameter( "@collectID",SqlDbType.Int),        
                   
                };
                parme[0].Value = id;

                return DB.ExecuteNonQuery(cs, "deleteCollect", parme);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int insertCollect(Model.shoucinfo sc)
        {
            SqlParameter[] parme = new SqlParameter[] {
                    new SqlParameter("@userId",SqlDbType.NVarChar,32),        
                    new SqlParameter("@collectName",SqlDbType.NVarChar,100),  
                    new SqlParameter("@description",SqlDbType.NVarChar,300),  
                    new SqlParameter("@URL",SqlDbType.NVarChar,300),
                    new SqlParameter("@time",SqlDbType.NVarChar,13), 
                };
            parme[0].Value = sc.yonghubianhao;
            parme[1].Value = sc.tmName;
            parme[2].Value = sc.miaoshu;
            parme[3].Value = sc.lujing;
            parme[4].Value = sc.shijian;
            return DB.ExecuteNonQuery(cs, "insertCollect", parme);
        }
    }
}
