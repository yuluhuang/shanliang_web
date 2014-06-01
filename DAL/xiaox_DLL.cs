using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using UN.Model;

namespace UN.DAL
{
    public class xiaox_DLL : ajxadatelist
    {
        DataBase DB = new DataBase();
        CommandType ct = CommandType.Text;
        CommandType cs = CommandType.StoredProcedure;
        /// <summary>
        /// 插入消息表
        /// </summary>
        /// <param name="xiaoxi"></param> 
        public int getshoucangxiaoxi_DLL(xiaoxinfo xx, string shoucangzheyonghubianhao)//shoucangzheyonghubianhao 用户id
        {
            string sqlstr = "select 姓名 from 用户表 where 用户ID=@用户ID";//收藏者姓名
            string name = getnamebyid(shoucangzheyonghubianhao, sqlstr);

            SqlParameter[] parame = new SqlParameter[]{
              new SqlParameter("@内容",SqlDbType.NVarChar,300),
              new SqlParameter("@类型",SqlDbType.NVarChar,10),
              new SqlParameter("@已读",SqlDbType.Bit),
              new SqlParameter("@时间",SqlDbType.DateTime),
              new SqlParameter("@用户编号",SqlDbType.NVarChar,20), 
            };
            parame[0].Value = string.Format("您被<b><a href='people.aspx?yonghuid={0}'>{1}</a></b>收藏了", shoucangzheyonghubianhao, name);
            parame[1].Value = "系统消息";
            parame[2].Value = 0;
            parame[3].Value = DateTime.Now.ToShortTimeString();
            parame[4].Value = xx.yonghubianhao;
            //string sql="insert into 消息表 (内容,类型,已读,时间,用户编号)values(@内容,@类型,@已读,@时间,@用户编号)";
            return DB.ExecuteNonQuery(cs, "插入_消息表", parame);

        }

        /// <summary>
        /// 筛选未读消息
        /// </summary>
        /// <param name="xiaoxi"></param>
        public DataTable getselectxiaoxi(xiaoxinfo xiaoxi)
        {
            SqlParameter[] parme = new SqlParameter[] {      
                    new SqlParameter("@已读",SqlDbType.Bit), 
                    new SqlParameter("@用户编号",SqlDbType.NVarChar,20), 
                };
            parme[0].Value = xiaoxi.yidu;
            parme[1].Value = xiaoxi.yonghubianhao;//用户编号
            DataTable dt = DB.getDataTable(cs, "选择_消息表", parme);
            return dt;
        }
        /// <summary>
        /// 获取所有消息
        /// </summary>
        /// <param name="xiaoxi"></param>
        public string getselectxiaoxiLine_DLL(string userId)
        {
            try
            {
                SqlParameter[] parme = new SqlParameter[] {      
                      new SqlParameter("@userId",SqlDbType.NVarChar,32), 
                };
                parme[0].Value = userId;//用户编号
                DataTable dt = DB.getDataTable(cs, "getInfoList", parme);

                string aa = CreateJsonParameters(dt, "infos");
                return aa;
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        /// <summary>
        /// 让消息变为已读/未读
        /// </summary>
        /// <param name="xiaoxi"></param>
        /// <returns></returns>
        public int getupdateyidu_DLL(xiaoxinfo xiaoxi)
        {
            SqlParameter[] parme = new SqlParameter[] {      
                      new SqlParameter("@id",SqlDbType.Int), 
                      new SqlParameter("@read",SqlDbType.Bit), 
                };
            parme[0].Value = xiaoxi.Id;
            parme[1].Value = xiaoxi.yidu;
            int a = DB.ExecuteNonQuery(cs, "updateinfo", parme);
            return a;
        }
        /// <summary>
        /// 插入消息表根据[作品ID]
        /// </summary>
        /// <param name="xx">消息</param>
        /// <param name="xiaozucy">编号</param>
        /// <returns></returns>
        public int getshoucangxiaoxi_zuop_DLL(xiaoxinfo xx, shoucinfo sc, int yonghuid, string[] xiaozucy)
        {
            try
            {
                for (int i = 0; i < xiaozucy.Length - 1; i++)
                {
                    string sqlstr = "select 姓名 from 用户表 where 用户ID=@用户ID";//收藏者姓名
                    string name = getnamebyid(yonghuid.ToString(), sqlstr);

                    SqlParameter[] parame = new SqlParameter[]{
                      new SqlParameter("@内容",SqlDbType.NVarChar,300),
                      new SqlParameter("@类型",SqlDbType.NVarChar,10),
                      new SqlParameter("@已读",SqlDbType.Bit),
                      new SqlParameter("@时间",SqlDbType.DateTime),
                      new SqlParameter("@用户编号",SqlDbType.NVarChar,20), //将要收到消息的id
                     };
                    parame[0].Value = string.Format("您的作品<b><a href='zuopin.aspx?wenzhangid={0}'>{1}</a></b>被<b><a href='people.aspx?yonghuid={2}'>{3}</a></b>收藏了", sc.zuopId, sc.tmName, yonghuid, name);
                    parame[1].Value = "系统消息";
                    parame[2].Value = 0;
                    parame[3].Value = DateTime.Now.ToShortTimeString();
                    parame[4].Value = xiaozucy[i];
                    DB.ExecuteNonQuery(cs, "插入_消息表", parame);
                }
                return 1;
            }
            catch { return 0; }
        }

        public string getname(string yhbh, string sqlstr)
        {
            SqlParameter[] paramer = new SqlParameter[]{
                     new SqlParameter("@用户编号",SqlDbType.NVarChar,20),
                    };
            paramer[0].Value = yhbh;
            return DB.ExecuteScalar(ct, sqlstr, paramer).ToString();
        }

        public string getnamebyid(string yhbh, string sqlstr)
        {
            SqlParameter[] paramer = new SqlParameter[]{
                     new SqlParameter("@用户ID",SqlDbType.NVarChar,20),
                    };
            paramer[0].Value = yhbh;
            return DB.ExecuteScalar(ct, sqlstr, paramer).ToString();
        }
        /// <summary>
        /// 插入消息表根据[活动ID]
        /// </summary>
        /// <param name="sc"></param>
        /// <returns></returns>
        public int getshoucangxiaoxi_huod_DLL(shoucinfo sc, int yonghuid, string huodongfaqiren)
        {
            try
            {
                string sqlstr = "select 姓名 from 用户表 where 用户编号=@用户编号";//收藏者姓名
                string name = getname(sc.yonghubianhao, sqlstr);

                SqlParameter[] parame = new SqlParameter[]{
                      new SqlParameter("@内容",SqlDbType.NVarChar,300),
                      new SqlParameter("@类型",SqlDbType.NVarChar,10),
                      new SqlParameter("@已读",SqlDbType.Bit),
                      new SqlParameter("@时间",SqlDbType.DateTime),
                      new SqlParameter("@用户编号",SqlDbType.NVarChar,20), //将要收到消息的id
                     };
                parame[0].Value = string.Format("您的活动<b><a href='zhanlan.aspx?huodongid={0}'>{1}</a></b>被<b><a href='people.aspx?yonghuid={2}'>{3}</a></b>收藏了", sc.huodId, sc.tmName, yonghuid, name);
                parame[1].Value = "系统消息";
                parame[2].Value = 0;
                parame[3].Value = DateTime.Now.ToShortTimeString();
                parame[4].Value = huodongfaqiren;
                return DB.ExecuteNonQuery(cs, "插入_消息表", parame);
            }
            catch { return 0; }
        }

        public int getxiaoxiwhereweidu_DLL(string id)
        {
            SqlParameter[] parme = new SqlParameter[] {      
                    new SqlParameter("@已读",SqlDbType.Bit), 
                    new SqlParameter("@用户编号",SqlDbType.NVarChar,20), 
                };
            parme[0].Value = 0;
            parme[1].Value = id;//用户编号
            return DB.ExecuteNonQuery(cs, "选择_消息表", parme);

        }
    }
}
