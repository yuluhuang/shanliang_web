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
    public class huod_DLL : ajxadatelist
    {
        private DataBase DB = new DataBase();
        CommandType cs = CommandType.StoredProcedure;
        /// <summary>
        /// 更新活动表 myhome
        /// </summary>
        /// <param name="hd"></param>
        /// <returns></returns>
        public int getupdatehuodong_DLL(Model.huodinfo hd)
        {
            try
            {
                SqlParameter[] paramer = new SqlParameter[] {   
                     new SqlParameter("@themeID",SqlDbType.Int),
                     new SqlParameter("@themeName",SqlDbType.NVarChar,100),  
                     new SqlParameter("@remark",SqlDbType.NVarChar),
                     new SqlParameter("@category",SqlDbType.NVarChar,50)  
                 };
                paramer[0].Value = hd.huodId;
                paramer[1].Value = hd.huodName ?? "";
                paramer[2].Value = hd.huodContext ?? "";
                paramer[3].Value = hd.leibie ?? "";
                int a = DB.ExecuteNonQuery(cs, "updateTheme", paramer);
                return a;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        /// <summary>
        /// 通过用户输入的关键字筛选
        /// 获得用户所需主题
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string getThemeInfoByKey(string userId, string key)
        {
            string aa = "";
            try
            {
                SqlParameter[] paramer = new SqlParameter[] {    
                     new SqlParameter( "@userId",SqlDbType.NVarChar,32),  
                     new SqlParameter( "@themeName",SqlDbType.NVarChar,100), 
                 };
                paramer[0].Value = userId;
                paramer[1].Value = key;

                DataTable dt = DB.getDataTable(cs, "GetThemeByKeyAndUserId", paramer);
                if (dt.Rows.Count < 1) { return aa; }//如果没有记录
                else
                {
                    aa = CreateJsonParameters(dt, "theme");

                }
            }
            catch (Exception ex)
            {
                return aa;
            }
            return aa;
        }

        /// <summary>
        /// 创建主题
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        public int createTheme(Model.huodinfo theme)
        {
            try
            {
                SqlParameter[] paramer = new SqlParameter[] {    
                     new SqlParameter( "@category",SqlDbType.NVarChar,50),  
                     new SqlParameter( "@themeName",SqlDbType.NVarChar,100), 
                     new SqlParameter( "@remark",SqlDbType.NVarChar),  
                     new SqlParameter( "@userId",SqlDbType.NVarChar,32), 
                     new SqlParameter( "@show",SqlDbType.NVarChar,2),  
                     new SqlParameter( "@icon",SqlDbType.NVarChar,300), 
                     new SqlParameter( "@point",SqlDbType.NVarChar,2), 

                 };
                paramer[0].Value = theme.leibie ?? "";
                paramer[1].Value = theme.huodName ?? "";
                paramer[2].Value = theme.huodContext ?? "";
                paramer[3].Value = theme.yonghubianhao ?? "";
                paramer[4].Value = theme.kejian ?? "";
                paramer[5].Value = theme.tubian ?? "";
                paramer[6].Value = theme.jinghua ?? "";
                int aa = Convert.ToInt32(DB.ExecuteScalar(cs, "createTheme", paramer));
                return aa;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// 获取某theme详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string selectThemeInfo(string id)
        {
            string aa = "";
            try
            {
                SqlParameter[] paramer = new SqlParameter[] {    
                     new SqlParameter( "@themeID",SqlDbType.NVarChar,32),  
                 };
                paramer[0].Value = id;


                DataTable dt = DB.getDataTable(cs, "GetThemeInfo", paramer);
                if (dt.Rows.Count < 1) { return aa; }//如果没有记录
                else
                {
                    aa = CreateJsonParameters(dt, "theme");

                }
            }
            catch (Exception ex)
            {
                return aa;
            }
            return aa;
        }
        /// <summary>
        /// 更新theme图标
        /// </summary>
        /// <param name="id"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public int updateThemeIcon(string id, string p)
        {
            try
            {
                SqlParameter[] paramer = new SqlParameter[] {   
                     new SqlParameter("@themeID",SqlDbType.Int),
                     new SqlParameter("@icon",SqlDbType.NVarChar,300)

                 };
                paramer[0].Value = id ?? "";
                paramer[1].Value = p ?? "";
                int a = DB.ExecuteNonQuery(cs, "updateThemeIcon", paramer);
                return a;
            }
            catch (Exception es)
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取所有theme
        /// </summary>
        /// <returns></returns>
        public string getAllThemes(int page, int rows)
        {
            try
            {

                SqlParameter[] Parame = new SqlParameter[] { };
                DataTable dt = DB.getDataTable(cs, "getAllTheme", Parame);
                string ss = toEasyuiJson(dt.Rows.Count, CreateJsonForPage(dt, "rows", page, rows));
                return ss;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public  int deletehuodong(Model.huodinfo huod)
        {
            try
            {
                SqlParameter[] parme = new SqlParameter[] {
                    new SqlParameter("@themeID",SqlDbType.Int),        
                };
                parme[0].Value = huod.huodId;
                return DB.ExecuteNonQuery(cs, "deleteThemeById", parme);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
