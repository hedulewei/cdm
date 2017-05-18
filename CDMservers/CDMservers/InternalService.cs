using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Common;
using log4net;
using Oracle.ManagedDataAccess.Client;

namespace CDMservers
{
    public static class InternalService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static Dictionary<string, Dictionary<string, string>> queueLock =
            new Dictionary<string, Dictionary<string, string>>();

        static InternalService()
        {
            queueLock.Add("haiyang", new Dictionary<string, string> { { "cars", "cars1" }, { "drivers", "drivers1" }, { "archives", "archives1" } });
            queueLock.Add("laizhou", new Dictionary<string, string> { { "cars", "cars2" }, { "drivers", "drivers2" }, { "archives", "archives2" } });
            queueLock.Add("zhifu", new Dictionary<string, string> { { "cars", "cars3" }, { "drivers", "drivers3" }, { "archives", "archives3" } });
            queueLock.Add("laishan", new Dictionary<string, string> { { "cars", "cars4" }, { "drivers", "drivers4" }, { "archives", "archives4" } });
            queueLock.Add("fushan", new Dictionary<string, string> { { "cars", "cars5" }, { "drivers", "drivers5" }, { "archives", "archives5" } });

            queueLock.Add("longkou", new Dictionary<string, string> { { "cars", "cars6" }, { "drivers", "drivers6" }, { "archives", "archives6" } });
            queueLock.Add("penglai", new Dictionary<string, string> { { "cars", "cars7" }, { "drivers", "drivers7" }, { "archives", "archives7" } });
            queueLock.Add("muping", new Dictionary<string, string> { { "cars", "cars8" }, { "drivers", "drivers8" }, { "archives", "archives8" } });
            queueLock.Add("zhaoyuan", new Dictionary<string, string> { { "cars", "cars9" }, { "drivers", "drivers9" }, { "archives", "archives9" } });
            queueLock.Add("qixia", new Dictionary<string, string> { { "cars", "cars10" }, { "drivers", "drivers10" }, { "archives", "archives10" } });

            queueLock.Add("laiyang", new Dictionary<string, string> { { "cars", "cars11" }, { "drivers", "drivers11" }, { "archives", "archives11" } });
            queueLock.Add("changdao", new Dictionary<string, string> { { "cars", "cars12" }, { "drivers", "drivers12" }, { "archives", "archives12" } });
        }
       
        public static int GetBusinessId()
        {
            using (var oracleConnectionconn = new OracleConnection(CdmConfiguration.DataSource))
            {
                oracleConnectionconn.Open();//打开指定的连接  
                OracleCommand com = oracleConnectionconn.CreateCommand();
                com.CommandText = string.Format("SELECT businessSequence.nextval FROM dual");
                var odr = com.ExecuteScalar();
                return int.Parse(odr.ToString());
            }
        }
        public static int GetOrdinal(BusinessModel input)
        {
            var category = input.businessCategory;
            var countycode = input.countyCode;
            var currentdate = DateTime.Now.Date;
            var scurrentdate = string.Format("{0}/{1}/{2}", currentdate.Year, currentdate.Month, currentdate.Day);
            //   Log.Info("000");
            var OracleConnectionconn = new OracleConnection(CdmConfiguration.DataSource);//进行连接           
            try
            {
                var lockvalue= queueLock[input.countyCode][input.businessCategory];
              //  Log.Error("GetOrdinal lockvalue:" + lockvalue);
                lock (lockvalue)
                {
                    OracleConnectionconn.Open();//打开指定的连接  
                    OracleCommand com = OracleConnectionconn.CreateCommand();
                    com.CommandText = string.Format("Select businessdate ,ordinal  From businessordinal where category =  '{0}' and countycode='{1}'", category, countycode);//写好想执行的Sql语句                   
                    //  Log.Info("222"+com.CommandText);
                    OracleDataReader odr = com.ExecuteReader();
                    var recordDate = string.Empty;
                    var ordinal = 0;
                    //   Log.Info("222");
                    while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了                    
                    {
                        for (int i = 0; i < odr.FieldCount; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    recordDate = odr.GetString(i);
                                    break;
                                case 1:
                                    ordinal = odr.GetInt32(i);
                                    break;
                            }
                        }
                    }
                    odr.Close();//关闭reader.这是一定要写的  
                    //   Log.Info("222"+ recordDate + "-"+ordinal+ currentdate);

                    if (scurrentdate == recordDate)
                    {
                        com.CommandText =
                               string.Format("update businessordinal set  ordinal = {1} where category = '{0}'and countycode='{2}' ", category, ++ordinal, countycode);
                        //    Log.Info("333" + com.CommandText);
                        com.ExecuteNonQuery();
                        return ordinal;
                    }
                    else
                    {
                        if (recordDate == string.Empty)
                        {
                            com.CommandText =
                               string.Format("insert into businessordinal (businessdate,category,ordinal,countycode) values(   '{2}' ,  '{0}', '{1}','{3}') ", category, 1, scurrentdate, countycode);
                        }
                        else
                        {
                            com.CommandText =
                               string.Format("update businessordinal set  ordinal = {1} , businessdate = '{2}' where category = '{0}'and countycode='{3}' ", category, 1, scurrentdate, countycode);
                        }
                        //   Log.Info("444" + com.CommandText);
                        com.ExecuteNonQuery();
                        return 1;
                    }
                }
               
            }
            catch (Exception eex)
            {
                Log.Error("GetOrdinal operation:" + eex.Message);
            }
            finally
            {
                OracleConnectionconn.Close();//关闭打开的连接              
            }
            return 0;
        }
    }
}