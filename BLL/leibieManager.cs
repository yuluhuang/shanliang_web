using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using UN.DAL;

namespace UN.BLL
{
    public  class leibieManager
    {
        leib_DLL leibie = new leib_DLL();
        public DataTable selectleibie()
        {
            return leibie.getselectleibie_DLL();
        }
    }
}
