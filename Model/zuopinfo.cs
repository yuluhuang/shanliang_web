using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UN.Model
{
    public  class zuopinfo
    {
        public int zuopId { get; set; }//作品id

        public int huodId { get; set; }//活动id

        public string zuopName { get; set; }//作品名D:\shanliang\shanliang\Model\zuopinfo.cs

        public string zuopbeizhu { get; set; }//备注
        public string leibie { get; set; }//类别
        public bool isSee { get; set; }//是否可见
        public string shijain { get; set; }//时间
        public string tubiao { get; set; }//图标
        public  int dianjishu{ get; set; }//点击数
        public string  jinghua { get; set; }//精华
    }
}
