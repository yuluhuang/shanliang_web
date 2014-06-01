using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Transactions;

/// <summary>
///DataBase 的摘要说明
/// </summary>
namespace UN.DAL
{
    public class DataBase
    {
        string connectString;
        SqlConnection connection;
        public DataBase()
        {
            this.connectString = System.Configuration.ConfigurationManager.ConnectionStrings["PFMDBConnectionString"].ToString();
           //  this.connectString = "Data Source=.;Initial Catalog=yuluhuang_sl; Integrated Security=True";
        }
        public void Open()
        {
            if (this.connection == null)
            {
                this.connection = new SqlConnection(this.connectString);//建立一个连接
                this.connection.Open();

            }
            if (this.connection.State.Equals(ConnectionState.Closed))//如果连接状态是关闭的
            {
                this.connection.Open();
            }

        }

        public void Close()
        {
            if (this.connection != null)//如果连接存在
            {
                this.connection.Close();
            }

        }
        public SqlConnection GetConnection()
        {
            this.Open();
            return connection;
        }

        /// <summary>
        /// 对SqlCommand属性作设置
        /// </summary>
        /// <param name="cmd">SqlCommand对象</param>
        /// <param name="ct">SqlCommand对象的命令类型</param>
        /// <param name="cmdTxt">SqlCommand对象的文本</param>
        /// <param name="cmdParms">SqlCommand对象的参数</param>
        public void Preparecommand(SqlCommand cmd, CommandType ct, string cmdTxt, SqlParameter[] cmdParms)
        {
            this.Open();//打开连接
            cmd.Connection = this.connection;//指明这个Command是基于我打开的这个连接
            cmd.CommandText = cmdTxt;
            cmd.CommandType = ct;

            // cmd.Parameters的Add方法是增加一个参数,增加多个参数的的时候使用一个foreach循环而已
            //cmd.Parameters的AddRange方法是增加一个参数的数组

            //AddRange
            if (cmdParms != null)//如果使用存储过程
            {
                cmd.Parameters.AddRange(cmdParms);
            }
            /*
             * //Add
            if (cmdParms != null)//如果使用存储过程
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }*/
        }
        /// <summary>
        /// 多了事物
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="transaction"></param>
        /// <param name="ct"></param>
        /// <param name="cmdTxt"></param>
        /// <param name="cmdParms"></param>
        //public void Preparecommand(SqlCommand cmd, TransactionScope transaction, CommandType ct, string cmdTxt, SqlParameter[] cmdParms)
        //{
        //    try
        //    {
        //        this.Open();
        //        cmd.Connection = this.connection; ;
        //        cmd.CommandText = cmdTxt;
        //        cmd.CommandType = ct;
        //        if (cmdParms != null)//如果使用存储过程
        //        {
        //            foreach (SqlParameter parm in cmdParms)
        //            {
        //                cmd.Parameters.Add(parm);
        //            }
        //        }
        //    }
        //    catch (Exception ex){ 
        //    }
        //}

        /// <summary>
        /// 对数据库进行增删改方法
        /// void
        /// </summary>
        /// <param name="ct">SqlCommand对象的命令类型</param>
        /// <param name="cmdTxt">SqlCommand对象的文本</param>
        /// <param name="cmdParms">SqlCommand对象的参数</param>
        /// 
        //public void ExecuteNonQuery(CommandType ct, string cmdTxt, SqlParameter[] cmdParms)//对数据库进行增删改方法
        //{
        //    try
        //    {
        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            this.Preparecommand(cmd, ct, cmdTxt, cmdParms);
        //            cmd.ExecuteNonQuery();//返回受影响的行数
        //            cmd.Parameters.Clear();
        //            this.Close();
        //        }
        //    }
        //    catch (Exception ex) { }
        //}

        //对数据库进行增删改方法（使用事务）
        public void ExecuteNonQueryTranV(CommandType ct, string cmdTxt, SqlParameter[] cmdParms)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        this.Preparecommand(cmd, ct, cmdTxt, cmdParms);
                        cmd.ExecuteNonQuery();//返回受影响的行数
                        transaction.Complete();
                        cmd.Parameters.Clear();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

        }

        //对数据库进行增删改方法（使用事物）
        /// <summary>
        /// 返回id
        ///使用事物
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="ct"></param>
        /// <param name="cmdTxt"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public object ExecuteNonQueryTranO(CommandType ct, string cmdTxt, SqlParameter[] cmdParms)
        {

            try
            {
                object ob = null;
                using (TransactionScope transaction = new TransactionScope())
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        this.Preparecommand(cmd, ct, cmdTxt, cmdParms);
                        cmd.ExecuteNonQuery();
                        ob = cmd.Parameters["@Return"].Value;//获取数组中名为"@Return"的值(orderId)赋给ob
                        transaction.Complete();
                        cmd.Parameters.Clear();
                    }
                }
                return ob;

            }
            catch
            {
                this.Close();
                return null;
            }

        }

        /// <summary>
        /// 对数据库进行增删改方法
        /// 返回受影响的行数
        /// </summary>
        /// <param name="ct"></param>
        /// <param name="cmdTxt"></param>
        /// <param name="cmdParms"></param>
        /// <returns>返回受影响的行数</returns>
        public int ExecuteNonQuery(CommandType ct, string cmdTxt, SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.Clear();
                this.Preparecommand(cmd, ct, cmdTxt, cmdParms);
                return cmd.ExecuteNonQuery();
                
            }
        }


        /// <summary>
        ///  返回第一行第一列的一个数值
        /// </summary>
        /// <param name="ct"></param>
        /// <param name="cmdTxt"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public object ExecuteScalar(CommandType ct, string cmdTxt, SqlParameter[] cmdParms)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.Clear();
                this.Preparecommand(cmd, ct, cmdTxt, cmdParms);
                return cmd.ExecuteScalar();
            }
        }


        /// <summary>
        /// *返回SqlDataReader对象*   使用 DataReader 对象的 Read 方法可从查询结果中获取行。
        /// sqldatareader 是在线读取读取完后需关闭 可使用CommandBehavior.CloseConnection
        /// </summary>
        /// <param name="ct">SqlCommand对象的命令类型</param>
        /// <param name="cmdTxt">SqlCommand对象的文本</param>
        /// <param name="cmdParms">SqlCommand对象的参数</param>
        /// <returns></returns>
        public SqlDataReader ExecuteDataReader(CommandType ct, string cmdTxt, SqlParameter[] cmdParms)//读取数据
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.Clear();
                this.Preparecommand(cmd, ct, cmdTxt, cmdParms);

                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }
        /*
          SqlDataReader da = cmd.ExecuteReader();
          while (da.Read())
           {
               str += da[2].ToString() + "," + da[6].ToString() + ";";
           }
        */
        /// <summary>
        /// *返回SqlDataReader对象*  
        ///多了链接和事务
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExecuteDataReader(string cmdText)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                // SqlConnection conn = new SqlConnection(connectString);
                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {
                        cmd.Parameters.Clear();
                        this.Preparecommand(cmd, CommandType.StoredProcedure, cmdText, null);
                        SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        transaction.Complete();
                        return rdr;
                    }
                    catch
                    {
                        this.Close();
                        throw;
                    }
                }
            }
        }



        /// <summary>
        /// 返回SqlDataAdapter对象
        /// </summary>
        /// <param name="ct">SqlCommand的命令类型</param>
        /// <param name="cmdTxt">SqlCommand对象的文本</param>
        /// <param name="cmdParms">SqlCommand对象的参数</param>
        /// <param name="opt">整型数值</param>
        /// <returns>SqlDataAdapter</returns>
        public SqlDataAdapter CreateDataAdapter(CommandType ct, string cmdTxt, SqlParameter[] cmdParms, int opt)
        {
            SqlDataAdapter sda = new SqlDataAdapter();

            using (SqlCommand cmd = new SqlCommand())
            {
                this.Preparecommand(cmd, ct, cmdTxt, cmdParms);

                switch (opt)
                {
                    case 1:
                        sda.SelectCommand = cmd;
                        break;
                    case 2:
                        sda.InsertCommand = cmd;
                        break;
                    case 3:
                        sda.DeleteCommand = cmd;
                        break;
                    case 4:
                        sda.UpdateCommand = cmd;
                        break;
                    default:
                        break;
                }
                this.Close();
                return sda;
            }
        }
        
        /// <summary>
        /// *获得DataTable*
        /// </summary>
        /// <param name="ct"></param>
        /// <param name="cmdTxt"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public DataTable getDataTable(CommandType ct, string cmdTxt, SqlParameter[] param)
        {
            try
            {
                this.Open();
                SqlDataAdapter adapter = CreateDataAdapter(ct, cmdTxt, param, 1);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                this.Close();
                return dt;
            }
            catch
            {
                this.Close();
                throw;
            }
        }
        /*
            DataTable dt ;
            foreach (DataRow dr in dt.Rows)
            {
                aa[0] = dr["密码"].ToString();
                aa[1] = dr["salt"].ToString();
            }
         */
        /// <summary>
        /// *获得DataSet*
        /// </summary>
        /// <param name="ct"></param>
        /// <param name="cmdTxt"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public DataSet getDataSet(CommandType ct, string cmdTxt, SqlParameter[] param, string tablename)
        {
            try
            {
                this.Open();
                SqlDataAdapter sqlda = CreateDataAdapter(ct, cmdTxt, param, 1);
                DataSet ds = new DataSet();
                sqlda.Fill(ds, tablename);
                this.Close();
                return ds;

                /*
                 DataTableReader rdr = ds.CreateDataReader();
                 while (rdr.Read())//读取表中数据
                 {
                     for (int i = 0; i < rdr.FieldCount; i++)
                     {
                         Console.Write(rdr.GetName(i) + "\t" + rdr.GetValue(i) + "\t");
                     }
                 }
                 */
            }
            catch
            {
                this.Close();
                throw;
            }
        }
    }
}

/*
 *DataSet是用来做连接sql的一种方法,意思是把数据库教程的副本存在应用程序里,相当于存在内存中的数据库,应用程序开始运行时,把数据库相关数据保存到DataSet.
 * DataSet 是数据的内存驻留表示形式，它提供了独立于数据源的一致关系编程模型。
 * DataSet 独立于数据源，因此 DataSet 可以包含应用程序本地的数据，也可以包含来自多个数据源的数据。
 * 
 *DataTable表示内存中数据的一个表.常和DefaultView使用获取可能包括筛选视图或游标位置的表的自定义视图。
 * 
 *DataReader对象是用来读取数据库的最简单方式,它只能读取,不能写入,并且是从头至尾往下读的,无法只读某条数据,但它占用内存小,速度快
 *DataReader你读取的数据必须是新的，所以在每次需要数据的时候，你都必须从数据库读取,必须保持到数据库的连接，
 *使用 DataReader 可以提高应用程序的性能，原因是它只要数据可用就立即检索数据，并且（默认情况下）一次只在内存中存储一行，减少了系统开销
 *
 DataAdapter对象是用来读取数据库.可读取写入数据,某条数据超着强,但它占用内存比dataReader大,速度慢，一般和DataSet连用.
 */