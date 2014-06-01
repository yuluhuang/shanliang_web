using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UN.Model
{
    public  class tiaominfo
    {
        public int tmId { get; set; }//条目id
        public int zuopId { get; set; }//作品id
        public string oldname { get; set; }//旧姓名
        public string newname { get; set; }//新姓名
        public string leixing { get; set; }//类型
        public string lujing1 { get; set; }//路径1
        public string lujing2 { get; set; }//路径2
        public string zhuti { get; set; }//主题
        public string jieshao { get; set; }//介绍
        public string paixu { get; set; }//排序
        public bool xiazai { get; set; }//下载
    }
}
