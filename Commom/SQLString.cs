using System;
using System.Collections;

namespace UN.Common
{
	/// <summary>
	/// SQLString ��ժҪ˵����
	/// </summary>
	public class SqlStringConstructor
	{
		/// <summary>
		/// ���о�̬���������ı�ת�����ʺ���Sql�����ʹ�õ��ַ�����
		/// </summary>
		/// <returns>ת�����ı�</returns>	
		public static String GetQuotedString(String pStr)
		{
			return ("'" + pStr.Replace("'","''") + "'");
		}

		/// <summary>
		/// ����������ϣ��,����SQL����е�AND�����Ӿ�
		/// </summary>
		/// <param name="conditionHash">������ϣ��</param>
		/// <returns>AND��ϵ�����Ӿ�</returns>
        public static String GetConditionClause(Hashtable queryItems)
        {

            int Count = 0;
            String Where = "";
           

            //���ݹ�ϣ��ѭ�����������Ӿ�
            foreach (DictionaryEntry item in queryItems)
            {
                if (Count == 0)
                    Where = " where ";
                else
                    Where += " and ";

                //���ݲ�ѯ�е��������ͣ������Ƿ�ӵ�����
                if (item.Value.GetType().ToString() == "System.String" )
                {
                    Where += item.Key.ToString()
                        + " like " 
                        + SqlStringConstructor.GetQuotedString("%"
                        + item.Value.ToString()
                        + "%");
                       
                }
                else if (item.Value.GetType().ToString() == "System.DateTime[]")
                {
                    string[] time = item.Value.ToString().Split(',');
                        Where += item.Key.ToString()
                               + " between "
                               + SqlStringConstructor.GetQuotedString(((DateTime[])item.Value)[0].ToString()) + " and " + SqlStringConstructor.GetQuotedString(((DateTime[])item.Value)[1].ToString());
                }

                else
                {
                    Where += item.Key.ToString() + "= " + item.Value.ToString();
                }
                Count++;
            }
            return Where;
        }
	}
}
