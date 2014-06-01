using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UN.Model
{
    public  class userinfo
    {
        public int Id { get; set; }
        public string yonghubianhao { get; set; }//用户编号
        public string name { get; set; }//姓名
        public string mima { get; set; }//密码
        public string qq { get; set; }//qq
        public string phone { get; set; }//phone
        public string Email { get; set; }//Email
        public string touxiang { get; set; }//头像
        public string jianjie { get; set; }//简介
        public string qianmidang { get; set; }//签名档
        public string shenfen { get; set; }//身份
    }
}
