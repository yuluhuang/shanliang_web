using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UN.DAL;
using UN.Model;

namespace UN.BLL
{
    public  class noteManager
    {
        note_DAL noteDAL = new note_DAL();
        public bool saveNote(noteinfo note)
        {
           return noteDAL.savanote(note);
        }

        /// <summary>
        /// 获取笔记集
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string getNoteList(string userid,int page,int row)
        {
            return noteDAL.getnotelist(userid,page,row);
        }

        /// <summary>
        /// 删除笔记
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int delNote(string id)
        {
            return noteDAL.delnote(id);
        }

        /// <summary>
        /// 搜索通过key获取notes
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string getNotesByKey(string key)
        {
            return noteDAL.getNotes(key);
        }

        public string getALLNotes(string userid)
        {
            return noteDAL.getallnotels(userid);
        }

        public string getNotesByNoteId(int noteid)
        {
            return noteDAL.getNotesByNoteId(noteid);
        }
    }
}
