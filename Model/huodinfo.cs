using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UN.Model
{
    public  class huodinfo
    {
        public int huodId { get; set; }//活动id
        public string huodName { get; set; }//活动名称
        public string huodContext { get; set; }//活动内容
        public string yonghubianhao { get; set; }//用户编号
        public string  leibie { get; set; }//类别
        public string  kejian { get; set; }//可见 
        public string tubian { get; set; }//图标
        public string jinghua { get; set; }//精华
    }
}
