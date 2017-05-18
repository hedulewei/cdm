using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Oracle.ManagedDataAccess.Client;
using log4net;
using System.Reflection;

namespace DataService
{
    //public class OracleOperation
    //{
    //    private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    //    const string DataSource = "user id=city;password=city;data source=localhost:1521/xe";
     
     

      
       
     
    //    public int GetOrdinal(BusinessModel input)
    //    {
    //        var category = input.businessCategory;
    //        var countycode = input.countyCode;
    //        var currentdate = DateTime.Now.Date;
    //        var scurrentdate = string.Format("{0}/{1}/{2}", currentdate.Year, currentdate.Month, currentdate.Day);
    //     //   Log.Info("000");
    //        var OracleConnectionconn = new OracleConnection(DataSource);//进行连接           
    //        try
    //        {
    //         //   Log.Info("111");
    //            OracleConnectionconn.Open();//打开指定的连接  
    //            OracleCommand com = OracleConnectionconn.CreateCommand();
    //            com.CommandText =string.Format( "Select businessdate ,ordinal  From businessordinal where category =  '{0}' and countycode='{1}'", category,countycode);//写好想执行的Sql语句                   
    //          //  Log.Info("222"+com.CommandText);
    //            OracleDataReader odr = com.ExecuteReader();
    //            var recordDate = string.Empty;
    //            var ordinal = 0;
    //         //   Log.Info("222");
    //            while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了                    
    //            {                   
    //                for (int i = 0; i < odr.FieldCount; i++)
    //                {                      
    //                    switch (i)
    //                    {
    //                        case 0:
    //                            recordDate = odr.GetString(i);
    //                            break;
    //                        case 1:
    //                            ordinal =odr.GetInt32(i);
    //                            break;
    //                    }
    //                }
    //            }
    //            odr.Close();//关闭reader.这是一定要写的  
    //         //   Log.Info("222"+ recordDate + "-"+ordinal+ currentdate);
              
    //            if (scurrentdate == recordDate)
    //            {
    //                com.CommandText =
    //                       string.Format("update businessordinal set  ordinal = {1} where category = '{0}'and countycode='{2}' ", category, ++ordinal,countycode);
    //            //    Log.Info("333" + com.CommandText);
    //                com.ExecuteNonQuery();
    //                return ordinal;
    //            }
    //            else
    //            {
    //                if (recordDate == string.Empty)
    //                {
    //                    com.CommandText =
    //                       string.Format("insert into businessordinal (businessdate,category,ordinal,countycode) values(   '{2}' ,  '{0}', '{1}','{3}') ", category, 1, scurrentdate, countycode);
    //                }
    //                else
    //                {
    //                    com.CommandText =
    //                       string.Format("update businessordinal set  ordinal = {1} , businessdate = '{2}' where category = '{0}'and countycode='{3}' ", category, 1, scurrentdate, countycode);
    //                }
    //                //   Log.Info("444" + com.CommandText);
    //                com.ExecuteNonQuery();
    //                return 1;
    //            }
    //        }
    //        catch (Exception eex)
    //        {
    //            Log.Error("GetOrdinal operation:" + eex.Message);
    //        }
    //        finally
    //        {
    //            OracleConnectionconn.Close();//关闭打开的连接              
    //        }
    //        return 0;
    //    }

       
    //}
}
