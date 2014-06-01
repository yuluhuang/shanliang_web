using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UN.Model;
using System.Data;
using System.Data.SqlClient;

namespace UN.DAL
{
    public  class note_DAL:ajxadatelist
    {
        private DataBase DB = new DataBase();
        CommandType cs = CommandType.StoredProcedure;
        /// <summary>
        /// 保存笔记
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public bool savanote(noteinfo note)
        {
            try
            {
                SqlParameter[] parames = new SqlParameter[]{
                    new SqlParameter("@noteTitle",SqlDbType.NVarChar,150),
                    new SqlParameter("@noteContent",SqlDbType.NVarChar),
                    new SqlParameter("@noteTime",SqlDbType.NVarChar,13),
                    new SqlParameter("@noteTag",SqlDbType.NVarChar,100),
                    new SqlParameter("@userId",SqlDbType.NVarChar,32),
                    };
                parames[0].Value = note.noteTitle;
                parames[1].Value = note.noteContent;
                parames[2].Value = note.noteTime;
                parames[3].Value = note.noteTag;
                parames[4].Value = note.userId;

                return DB.ExecuteNonQuery(cs, "saveNote", parames) > 0 ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
       /// <summary>
       /// 获得笔记集
       /// </summary>
       /// <param name="userid"></param>
       /// <returns></returns>
        public string getnotelist(string userid)
        {
            try
            {
                SqlParameter[] parames = new SqlParameter[]{
  
                    new SqlParameter("@userId",SqlDbType.NVarChar,32),
                    };

                parames[0].Value = userid;

                DataTable  dt= DB.getDataTable(cs, "getNoteList", parames);
                string aa = CreateJsonParameters(dt,"notes");
                return aa;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public int delnote(string id)
        {
            try
            {
                SqlParameter[] parames = new SqlParameter[]{
  
                    new SqlParameter("@noteID",SqlDbType.Int),
                    };

                parames[0].Value = id;

                return  DB.ExecuteNonQuery(cs, "delNote", parames);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public string getNotes(string key)
        {
            try
            {
                SqlParameter[] parames = new SqlParameter[]{
                    new SqlParameter("@noteTitle",SqlDbType.NVarChar,150),
                  
                    };
                parames[0].Value = key;
                DataTable dt = DB.getDataTable(cs, "getNoteByTitle",parames);
                string s = CreateJsonParameters(dt, "note");
                return s;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
