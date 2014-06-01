using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UN.Model
{
    public  class shoucinfo
    {
        public int shoucId { get; set; }//收藏id
        public string yonghubianhao { get; set; }//用户编号
        public string tmName { get; set; }//条目名称
        public string miaoshu { get; set; }//描述
        public string lujing { get; set; }//路径
        public string shijian { get; set; }//时间
        public string renwuId { get; set; }//人物ID
        public int huodId { get; set; }//活动ID
        public int zuopId { get; set; }//作品ID
    }
}
