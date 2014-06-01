using System;
using System.Web;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using UN.Common;

using UN.Model;

namespace UN.DAL
{
    public class userDAL : ajxadatelist//ajxadatelist.cs  to json
    {
        private DataBase DB = new DataBase();
       // CommandType ct = CommandType.Text;
        CommandType cs = CommandType.StoredProcedure;//存储过程类型
        public string userLogin(userinfo user)
        {
            try
            {
                //System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(test, "MD5");  using System.Web.Security;
                //1.将用户输入用户名hash，用于取出数据库中的密码和盐
                //2.将用户输入密码加盐hash，跟数据库取出密码比较
                using (MD5 md5Hash = MD5.Create())
                {

                    string usernamehash = GetMd5Hash(md5Hash, user.yonghubianhao);//将用户编号hash
                    string[] pands = GetPasswordAndSalt(usernamehash).Split(','); //通过hash后的用户名获得数据库内密码和盐

                    if (pands == null) return null;//如果没有记录

                    string password = pands[0];
                    string salt = pands[1];
                    string userpassword = passwordToMd5(user.mima, salt);//将用户输入密码加盐hash[与数据库中获取的密码比较]

                    if (userpassword.Equals(password))
                    {
                        return usernamehash + "," + pands[2];
                    }
                    else
                    {
                        return null;
                    }
                    //比较
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
     


        /// <summary>
        /// 获得数据库内密码和盐
        /// 即通过hash后的用户名
        /// </summary>
        /// <param name="usermanehash"></param>
        /// <returns></returns>
        public string GetPasswordAndSalt(string usernamehash)
        {
            try
            {
                string aa = "";
                SqlParameter[] usernameParameter = new SqlParameter[]{
                    new SqlParameter("@userId",SqlDbType.NVarChar,32)
                 };
                usernameParameter[0].Value = usernamehash;

                // existUser(注册时判断是否存在该用户)   select * from userTable where userId=@userId
                DataTable dt = DB.getDataTable(cs, "existUser", usernameParameter);
                if (dt.Rows.Count != 1) { return aa; }//如果没有记录
                else
                {
                    //dt.Rows[0]["password"].ToString();
                    foreach (DataRow dr in dt.Rows)
                    {
                        aa = dr["password"].ToString() + "," + dr["salt"].ToString() + "," + dr["indentity"].ToString();
                    }
                }
                return aa;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 用户是否已被注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int existUser(userinfo user)
        {
            string  userId= usernameToMd5(user.yonghubianhao);
            SqlParameter[] registerParamer = new SqlParameter[]{
                new SqlParameter("@userId",SqlDbType.NVarChar,32),
            };
            registerParamer[0].Value = userId;
            //string sql = "select * from userTable where userId=@userId";
            int a = Convert.ToInt32(DB.ExecuteScalar(cs, "existUser", registerParamer));
            return a;

        }



        /// <summary>
        /// 注册,将用户名,密码hash后同salt一起存入数据库
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Boolean userRegister(userinfo user)
        {
            //当表中没有数据时放回0，ExecuteScalar
            if (existUser(user) == 0)
            {
                string salt = getSaltForPassword();
                SqlParameter[] registerParamer = new SqlParameter[]{
                new SqlParameter("@userId",SqlDbType.NVarChar,32),
                new SqlParameter("@password",SqlDbType.NVarChar,32),
                new SqlParameter("@salt",SqlDbType.NVarChar,32),
                new SqlParameter("@Email",SqlDbType.NVarChar,50),
                new SqlParameter("@indentity",SqlDbType.NVarChar,20),
            };
                registerParamer[0].Value = usernameToMd5(user.yonghubianhao);
                registerParamer[1].Value = passwordToMd5(user.mima, salt);
                registerParamer[2].Value = salt;
                registerParamer[3].Value = user.Email;
                registerParamer[4].Value = "1";
                return DB.ExecuteNonQuery(cs, "registerUser", registerParamer) == 1 ? true : false;
            }
            else
            {
                return false;
            }

        }

        //
        //将用户名密码hash后存入数据库
        //

        /// <summary>
        /// 将用户名hash【注册】
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private string usernameToMd5(string username)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                return GetMd5Hash(md5Hash, username);//将用户编号hash

            }
        }
        /// <summary>
        /// 将密码加盐哈希【注册】
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string passwordToMd5(string password, string salt)
        {

            using (MD5 md5Hash = MD5.Create())
            {
                return GetMd5Hash(md5Hash, password + salt);//将用户编号hash
            }
        }
        /// <summary>
        /// 获得salt【注册】
        /// </summary>
        /// <returns></returns>
        private string getSaltForPassword()
        {
            return Guid.NewGuid().ToString("N");
        }

        public string getUserInfo(string userbianhao)
        {
            string aa = "";
            SqlParameter[] usernameParameter = new SqlParameter[]{
                    new SqlParameter("@userId",SqlDbType.NVarChar,32)
                 };
            usernameParameter[0].Value = userbianhao;


            DataTable dt = DB.getDataTable(cs, "GetUserInfo", usernameParameter);
            aa = CreateJsonParameters(dt, "user");
            //aa="[{\"a\":1}]";
            //if (dt.Rows.Count != 1) { return aa; }//如果没有记录
            //else
            //{

            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        aa = "\"姓名\":" + dr["姓名"].ToString() + ",QQ:" + dr["QQ"].ToString() + ",电话:" + dr["电话"].ToString() + ",Email:" + dr["Email"].ToString()
            //            + ",头像:" + dr["头像"].ToString() + ",简介:" + dr["简介"].ToString() + ",签名档:" + dr["签名档"].ToString() + ",身份:" + dr["身份"].ToString();
            //    }
            //}
            return aa;
        }

        public int setUserIcon(string userId, string path)
        {
            SqlParameter[] Parame = new SqlParameter[]{
                new SqlParameter("@userId",SqlDbType.NVarChar,32),
                new SqlParameter("@icon",SqlDbType.NVarChar,300),
            };
            Parame[0].Value = userId;
            Parame[1].Value = path;

            int a = DB.ExecuteNonQuery(cs, "SetUserIcon", Parame);
            return a;
        }
        /// <summary>
        /// 修改基本资料
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int modifyMyInfo(userinfo user)
        {
            try
            {
                SqlParameter[] Parame = new SqlParameter[]{
                new SqlParameter("@userId",SqlDbType.NVarChar,32),
                new SqlParameter("@name",SqlDbType.NVarChar,300),
                new SqlParameter("@Email",SqlDbType.NVarChar,300),
                new SqlParameter("@introduction",SqlDbType.NVarChar),
            };
                Parame[0].Value = user.yonghubianhao;
                Parame[1].Value = user.name;
                Parame[2].Value = user.Email;
                Parame[3].Value = user.jianjie;

                int a = DB.ExecuteNonQuery(cs, "modifyinfo", Parame);
                return a;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int updatePassword(string userId, string password)
        {

            string[] pands = GetPasswordAndSalt(userId).Split(','); //通过hash后的用户名获得数据库内密码和盐
            if (pands == null) return 0;//如果没有记录
            string salt = pands[1];

            try
            {
                SqlParameter[] Parame = new SqlParameter[]{
                new SqlParameter("@userId",SqlDbType.NVarChar,32),
                new SqlParameter("@password",SqlDbType.NVarChar,32),

            };
                Parame[0].Value = userId;
                Parame[1].Value = passwordToMd5(password, salt);


                int a = DB.ExecuteNonQuery(cs, "updatepassword", Parame);
                return a;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public string getUsers(int page,int  rows)
        {
            try
            {
                
                SqlParameter[] Parame = new SqlParameter[] { };
                DataTable dt = DB.getDataTable(cs, "getusers", Parame);


                string ss = toEasyuiJson(dt.Rows.Count, CreateJsonForPage(dt, "rows",page,rows));
                return ss;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public int updatemotto(string motto,string userId)
        {
            try
            {
                SqlParameter[] Parame = new SqlParameter[] {
                  new SqlParameter("@userId",SqlDbType.NVarChar,32),
                  new SqlParameter("@motto",SqlDbType.NVarChar),
                };
                Parame[0].Value = userId;
                Parame[1].Value = motto;

                int a = DB.ExecuteNonQuery(cs, "updatemotto", Parame);
                return a;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
