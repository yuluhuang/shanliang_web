using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UN.Model
{
    public  class xiaoxinfo
    {
        public int Id { get; set; }//id
        public string neirong { get; set; }//内容
        public string xiaoxiType { get; set; }//消息类型
        public bool yidu { get; set; }//已读
        public string shijian { get; set; }//时间
        public string yonghubianhao { get; set; }//用户编号
    }
}
