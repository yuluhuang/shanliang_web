using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace UN.DAL
{
    public  class leib_DLL
    {
        DataBase DB = new DataBase();
        public System.Data.DataTable getselectleibie_DLL()
        {
            CommandType ct = CommandType.Text;
            SqlParameter [] parame = new SqlParameter[]{};
            string sql = "select * from 类别表";
            return DB.getDataTable(ct,sql,parame);
        }
    }
}
 