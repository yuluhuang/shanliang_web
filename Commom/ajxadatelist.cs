using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Security.Cryptography;

/// <summary>
///ajxadatelist 的摘要说明
/// </summary>
public class ajxadatelist
{
    public ajxadatelist()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    public static string CreateJsonParameters(DataTable dt, string name)
    {
        /**/
        /**/
        /**/
        /* /****************************************************************************
      * Without goingin to the depth of the functioning of this Method, i will try to give an overview
      * As soon as this method gets a DataTable it starts to convert it into JSON String,
      * it takes each row and in each row it grabs the cell name and its data.
      * This kind of JSON is very usefull when developer have to have Column name of the .
      * Values Can be Access on clien in this way. OBJ.HEAD[0].<ColumnName>
      * NOTE: One negative point. by this method user will not be able to call any cell by its index.
     * *************************************************************************/
        StringBuilder JsonString = new StringBuilder();
        //Exception Handling        
        if (dt != null && dt.Rows.Count > 0)
        {
            // JsonString.Append("{ ");
            JsonString.Append("\"" + name + "\":[ ");
            //JsonString.Append("\"summary\":\"Blogs\",\"blogrolls\": [");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JsonString.Append("{ ");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j < dt.Columns.Count - 1)
                    {
                        JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\",");
                    }
                    else if (j == dt.Columns.Count - 1)
                    {
                        JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\"");
                    }
                }
                /**/
                /**/
                /**/
                /*end Of String*/
                if (i == dt.Rows.Count - 1)
                {
                    JsonString.Append("}");
                }
                else
                {
                    JsonString.Append("}, ");
                }
            }
            //JsonString.Append("]}");
            JsonString.Append("]");
            return JsonString.ToString();
        }
        else
        {
            return null;
        }
    }


    /// <summary>
    /// "{\"total\":"+count+","+ ss+"}"
    /// </summary>
    /// <param name="count">总行数</param>
    /// <param name="ss">xx:[{...}]</param>
    /// <returns></returns>
    public static string toEasyuiJson(int count, string ss)
    {
        return "{\"total\":" + count + "," + ss + "}";
    }


    /// <summary>
    /// 分页（部分转换）
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="name"></param>
    /// <param name="page">第几页</param>
    /// <param name="row">每页行数</param>
    /// <returns></returns>
    public static string CreateJsonForPage(DataTable dt, string name, int page, int row)
    {
        int from = (page - 1) * row;
        int to = (page - 1) * row + row > dt.Rows.Count ? dt.Rows.Count : (page - 1) * row + row;//客户端所要记录数要小于数据库总记录数，否则取数据库总记录数

        StringBuilder JsonString = new StringBuilder();

        if (dt != null && dt.Rows.Count > 0)//存在记录
        {
            JsonString.Append("\"" + name + "\":[ ");

            for (int i = from; i < to; i++)
            {
                JsonString.Append("{ ");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j < dt.Columns.Count - 1)
                    {
                        JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\",");
                    }
                    else if (j == dt.Columns.Count - 1)
                    {
                        JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\"");
                    }
                }
                if (i == to - 1)
                {
                    JsonString.Append("}");
                }
                else
                {
                    JsonString.Append("}, ");
                }
            }
            JsonString.Append("]");
            return JsonString.ToString();
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// 获得hash值 #目的：获取用户名和盐[通过用户输入的用户名]
    /// </summary>
    /// <param name="md5Hash"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string GetMd5Hash(MD5 md5Hash, string input)
    {

        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }



}