using System;
using System.Web;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UN.DAL;
using UN.Model;

namespace UN.BLL
{
    public class userManageBLL : IRequiresSessionState
    {
        userDAL userD = new userDAL();
        public string  loginManager(userinfo user) 
        {
            return userD.userLogin(user);
        }

        public Boolean registerManage(userinfo user)
        {
            return userD.userRegister(user);
        }

        public void logoutManage() { }

        public string userInfo(string userbianhao)
        {
            return userD.getUserInfo(userbianhao);
        }
        /// <summary>
        /// 设置头像
        /// </summary>
        public int setIcon(string userId,string path)
        {
            return userD.setUserIcon(userId,path);
        }
        /// <summary>
        /// 修改基本信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int modifyInfo(userinfo user)
        {
            return userD.modifyMyInfo(user);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int updatePassword(string userId,string password)
        {
            return userD.updatePassword(userId,password);
        }

        public string getUsersForManage(int page, int rows)
        {
            return userD.getUsers(page, rows);
        }

        /// <summary>
        /// 更新说说
        /// </summary>
        /// <param name="motto"></param>
        /// <returns></returns>
        public int updateMotto(string motto,string userId)
        {
            return userD.updatemotto(motto,userId);
        }
    }
}
