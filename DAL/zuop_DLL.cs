using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using UN.Common;
namespace UN.DAL
{
    public class zuop_DLL : ajxadatelist//ajxadatelist.cs  to json
    {
        private DataBase DB = new DataBase();
        CommandType cs = CommandType.StoredProcedure;
       
        /// <summary>
        /// 创建作品
        /// </summary>
        /// <param name="zuop"></param>
        /// <returns></returns>
        public int insertzuopin_DLL(Model.zuopinfo zuop)
        {
            try
            {
                SqlParameter[] parmers = new SqlParameter[] {
                    new SqlParameter( "@taskName",SqlDbType.NVarChar,100),
                    new SqlParameter( "@remark",SqlDbType.NVarChar),
                    new SqlParameter("@category",SqlDbType.NVarChar,50),
                    new SqlParameter("@time",SqlDbType.NVarChar,13),
                       new SqlParameter("themeID",SqlDbType.Int),
                };

                parmers[0].Value = zuop.zuopName;
                parmers[1].Value = zuop.zuopbeizhu;
                parmers[2].Value = zuop.leibie;
                parmers[3].Value = zuop.shijain;
                parmers[4].Value = zuop.huodId;
                int a = Convert.ToInt32(DB.ExecuteScalar(cs, "createTask", parmers));
                return a;
            }catch(Exception ex){
                return 0;
            }
        }
    
        /// <summary>
        /// 删除作品
        /// </summary>
        /// <param name="zp"></param>
        /// <returns></returns>
        public int delectzuopin_DLL(Model.zuopinfo zp)
        {
            SqlParameter[] parme = new SqlParameter[] {
                    new SqlParameter("@taskID",SqlDbType.Int),        
                };
            parme[0].Value = zp.zuopId;
            return DB.ExecuteNonQuery(cs, "deleteTask", parme);
        }

 

        /// <summary>
        /// 编辑作品
        /// </summary>
        /// <param name="zuop"></param>
        /// <returns></returns>
        public int getupdatezuop_DLL(Model.zuopinfo zuop)
        {
            try
            {
                SqlParameter[] editzuop_zx = new SqlParameter[] {
                    new SqlParameter("@taskID",SqlDbType.Int),        
                    new SqlParameter("@taskName",SqlDbType.NVarChar,100),  
                    new SqlParameter("@remark",SqlDbType.NVarChar),
                    
                };
                editzuop_zx[0].Value = zuop.zuopId;
                editzuop_zx[1].Value = zuop.zuopName;
                editzuop_zx[2].Value = zuop.zuopbeizhu;


                int a = DB.ExecuteNonQuery(cs, "editTask", editzuop_zx);
                return a;
            }catch(Exception ex){
                return 0;
            }
        }

        /// <summary>
        /// 获得task集通过themeid
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        public string getZuoPinInfo(string themeId)
        {

            try
            {
                SqlParameter[] parme = new SqlParameter[] {
                    new SqlParameter("@themeID",SqlDbType.Int),        
                };
                parme[0].Value = themeId;
                DataTable dt = DB.getDataTable(cs, "GetTaskInfo", parme);
                return CreateJsonParameters(dt, "task");
            }
            catch (Exception) {
                return "";
            }
        }

        /// <summary>
        /// 获得以sort列排序最大的作品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int getMax(string id)
        {
            try
            {
                SqlParameter[] parmers = new SqlParameter[] {
                    new SqlParameter( "@taskID",SqlDbType.Int),
                };

                parmers[0].Value = id;

                int a = Convert.ToInt32(DB.ExecuteScalar(cs, "getMaxBySort", parmers));
                return a;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int updateTaskIcon(string taskId, string path)
        {
            try
            {
                SqlParameter[] parmers = new SqlParameter[] {
                    new SqlParameter( "@taskID",SqlDbType.Int),
                    new SqlParameter("@icon",SqlDbType.NVarChar,300),
                    
                };
                parmers[0].Value = taskId;
                parmers[1].Value = path;
                int a = Convert.ToInt32(DB.ExecuteNonQuery(cs, "updateTaskIcon", parmers));
                return a;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        /// <summary>
        /// 获取taskinfo 通过taskid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string getTaskInfo(string id)
        {
            try
            {
                SqlParameter[] parmers = new SqlParameter[] {
                    new SqlParameter( "@taskID",SqlDbType.Int)
                    
                };
                parmers[0].Value = id;
                DataTable dt = DB.getDataTable(cs, "getTaskInfoByTaskId", parmers);
                string a = CreateJsonParameters(dt,"task");
                return a;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        ///  更新taskinfo 通过taskid
        /// </summary>
        /// <returns></returns>
        public int updateTaskInfo(Model.zuopinfo zuop)
        {
            try
            {
                SqlParameter[] parmers = new SqlParameter[] {
                    new SqlParameter( "@taskName",SqlDbType.NVarChar,100),
                    new SqlParameter( "@remark",SqlDbType.NVarChar),
                    new SqlParameter("@category",SqlDbType.NVarChar,50),
                    new SqlParameter("@time",SqlDbType.NVarChar,13),
                    new SqlParameter("themeID",SqlDbType.Int),
                    new SqlParameter("taskID",SqlDbType.Int)
                };

                parmers[0].Value = zuop.zuopName;
                parmers[1].Value = zuop.zuopbeizhu;
                parmers[2].Value = zuop.leibie;
                parmers[3].Value = zuop.shijain;
                parmers[4].Value = zuop.huodId;
                parmers[5].Value = zuop.zuopId;
                int a = Convert.ToInt32(DB.ExecuteNonQuery(cs, "updateTask", parmers));
                if (a == 1) {
                    return zuop.zuopId;
                }
                return 0;
            }catch(Exception ex){
                return 0;
            }
        }

        public string getTasksByKey(string key)
        {
            try
            {
                SqlParameter[] parmae = new SqlParameter[] {
                    new SqlParameter("@taskName",SqlDbType.NVarChar,100),                      
                };
                parmae[0].Value = key;
                DataTable dt = DB.getDataTable(cs, "getTaskByName", parmae);
                string s = CreateJsonParameters(dt, "task");
                return s;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
