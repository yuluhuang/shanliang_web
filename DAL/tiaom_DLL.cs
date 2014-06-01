using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using UN.Model;
using UN.Common;


namespace UN.DAL
{
    public class tiaom_DLL :ajxadatelist
    {
        DataBase DB = new DataBase();
        CommandType cs = CommandType.StoredProcedure;

        /// <summary>
        /// 删除条目
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        public int delete_tm_DLL(string id)
        {
            SqlParameter[] dlete = new SqlParameter[]{
             new SqlParameter("@itemID",SqlDbType.Int),
            };
            dlete[0].Value = id;
            return DB.ExecuteNonQuery(cs, "deleteItem", dlete);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="wenzhangid"></param>
        /// <param name="leixing"></param>
        /// <returns></returns>
        public string getcaidan(string wenzhangid, string leixing)
        {
            try
            {
                string str = "";
                string getwendanstr = "select * from 作品条目表 where 作品ID=" + wenzhangid + " and 类型='" + leixing + "'";
                SqlCommand cmd = new SqlCommand(getwendanstr, DB.GetConnection());
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    str += da[2].ToString() + "," + da[6].ToString() + ";";
                }
                da.Close();
                return str;
            }
            catch (Exception ex) { throw ex; }
            finally { DB.Close(); }
        }

       

        /// <summary>
        /// 为排序选择某作品(作品ID)下的第一个作品条目
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        public int select_for_updatexulie(Model.tiaominfo tm)
        {
            CommandType ct = CommandType.Text;
            string sql = "select 条目ID from 作品条目表 where  作品ID=@作品ID"; //+ tm.zuopId;
            SqlParameter[] paras = new SqlParameter[]{
             new SqlParameter("@作品ID",SqlDbType.Int),
             };
            paras[0].Value = tm.zuopId;
            int a = Convert.ToInt32(DB.ExecuteScalar(ct, sql, paras));
            return a;
        }

        /// <summary>
        /// 更新条目排序列
        /// </summary>
        /// <param name="tuoz"></param>
        /// <param name="a"></param>
        public void update_paixu_DLL(int tuoz, int a)
        {
            CommandType ct = CommandType.Text;
            string sql = "update 作品条目表 set 排序=@排序 where 条目ID=@条目ID";
            SqlParameter[] paras = new SqlParameter[]{
             new SqlParameter("@排序",SqlDbType.Int),
             new SqlParameter("@条目ID",SqlDbType.Int),
             };
            paras[0].Value = tuoz;
            paras[1].Value = a;
            DB.ExecuteNonQuery(ct, sql, paras);
        }
        /// <summary>
        /// 选择条目id
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        //public DataTable selectTMID_DLL(Model.tiaominfo tm)
        //{
        //    SqlParameter[] parmer = new SqlParameter[] {
        //            new SqlParameter("@作品ID",SqlDbType.Int)       
        //        };
        //    parmer[0].Value = tm.zuopId;
        //    return DB.getDataTable(cs, "选择_条目表", parmer);
        //}

        public int inserttm(tiaominfo tm)
        {
            SqlParameter[] parmers = new SqlParameter[] {
                            new SqlParameter("@taskID",SqlDbType.NVarChar,12),
                            new SqlParameter( "@oldName",SqlDbType.NVarChar,100),
                            new SqlParameter( "@newName",SqlDbType.NVarChar,100),
                            new SqlParameter( "@category",SqlDbType.NVarChar,100),
                            new SqlParameter( "@path1",SqlDbType.NVarChar,300),
                            new SqlParameter( "@path2",SqlDbType.NVarChar,300),
                            new SqlParameter("@sort",SqlDbType.NVarChar,12)
                            };
            parmers[0].Value = tm.zuopId;
            parmers[1].Value = tm.oldname;
            parmers[2].Value = tm.newname;
            parmers[3].Value = tm.leixing;
            parmers[4].Value = tm.lujing1;
            parmers[5].Value = tm.lujing2;
            parmers[6].Value = tm.paixu;
            return DB.ExecuteNonQuery(cs, "insertItem", parmers);
        }
        /// <summary>
        /// 获取条目集
        /// </summary>
        /// <param name="id">taskID</param>
        /// <returns></returns>
        public string getItems(string id)
        {
            try
            {
                SqlParameter[] parmer = new SqlParameter[] {
                    new SqlParameter("@taskID",SqlDbType.Int)       
                };
                parmer[0].Value = id;
                DataTable dt = DB.getDataTable(cs, "getItems", parmer);
                string aa = CreateJsonParameters(dt, "items");
                return aa;
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 添加主题和介绍
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public int updateItem(string id, string title, string remark)
        {
            try
            {
                SqlParameter[] par = new SqlParameter[] {
                    new SqlParameter("@itemID",SqlDbType.Int),        
                    new SqlParameter( "@title",SqlDbType.NVarChar,100),
                    new SqlParameter( "@remark",SqlDbType.NVarChar)   
                };
                par[0].Value = id;
                par[1].Value = title;
                par[2].Value = remark;
                int a = DB.ExecuteNonQuery(cs, "updateItem", par);
                return a;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
