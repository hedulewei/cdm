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
    public class OracleOperation
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        const string DataSource = "user id=city;password=city;data source=localhost:1521/xe";
        public int GetBusinessId()
        {
            using (var oracleConnectionconn = new OracleConnection(DataSource))
            {
                oracleConnectionconn.Open();//打开指定的连接  
                OracleCommand com = oracleConnectionconn.CreateCommand();
                com.CommandText = string.Format("SELECT businessSequence.nextval FROM dual");
                var odr = com.ExecuteScalar();
                return int.Parse(odr.ToString());
            }
        }
        public BusinessModel RetrieveCorporateInfo(BusinessModel input)
        {
            using (var oracleConnectionconn = new OracleConnection(DataSource))
            {
                oracleConnectionconn.Open();//打开指定的连接  
                OracleCommand com = oracleConnectionconn.CreateCommand();
                com.CommandText = string.Format("SELECT name,address from corporateinfo where code='{0}'", input.IDum);
                OracleDataReader odr = com.ExecuteReader();
                var count = new BusinessModel();
                while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了                    
                {
                    count.name = odr.GetString(0);
                    count.address = odr.GetString(1);
                }
                odr.Close();
                return count;
            }
        }

        public int SendCorporateInfo(BusinessModel input)
        {
            using (var oracleConnectionconn = new OracleConnection(DataSource))
            {
                oracleConnectionconn.Open();//打开指定的连接  
                OracleCommand com = oracleConnectionconn.CreateCommand();
                com.CommandText = string.Format("SELECT count(code) from corporateinfo where code='{0}'", input.IDum);
                OracleDataReader odr = com.ExecuteReader();
                var count = 0;
                while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了                    
                {
                    count = odr.GetInt32(0);
                }
                odr.Close();
                if (count < 1)
                {
                    Log.Info("insert aaa=");
                    com.CommandText = string.Format("insert into corporateinfo (code,name,address) values('{0}','{1}','{2}')",
                        input.IDum, input.name, input.address);//写好想执行的Sql语句   
                    Log.Info("insert bbb=");
                    Log.Info("insert CommandText=" + com.CommandText);
                    return com.ExecuteNonQuery();
                }
                else
                {
                    Log.Info("update aaa=");
                    com.CommandText = string.Format("update corporateinfo set name='{0}',address='{1}' where code='{2}'",
                        input.name, input.address, input.IDum);//写好想执行的Sql语句   
                    Log.Info("update bbb=");
                    Log.Info("update CommandText=" + com.CommandText);
                    return com.ExecuteNonQuery();
                }
            }
        }
        public string RetrieveCellPhoneNumber(BusinessModel input)
        {
            using (var oracleConnectionconn = new OracleConnection(DataSource))
            {
                oracleConnectionconn.Open();//打开指定的连接  
                OracleCommand com = oracleConnectionconn.CreateCommand();
                com.CommandText = string.Format("SELECT mobile from population where idnum='{0}'", input.IDum);
                OracleDataReader odr = com.ExecuteReader();
                var count = string.Empty;
                while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了                    
                {
                    count = odr.GetString(0);
                }
                odr.Close();
                return count;
            }
        }
        public int SendIdentityCardInfo(BusinessModel input)
        {
            using (var oracleConnectionconn = new OracleConnection(DataSource))
            {
                oracleConnectionconn.Open();//打开指定的连接  
                OracleCommand com = oracleConnectionconn.CreateCommand();
                com.CommandText = string.Format("SELECT count(idnum) from population where idnum='{0}'",input.IDum);
                OracleDataReader odr = com.ExecuteReader();
                var count = 0;
                while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了                    
                {
                    count = odr.GetInt32(0);
                }
                odr.Close();
                if (count <1)
                {
                    Log.Info("insert aaa=");
                    com.CommandText = string.Format("insert into population (name,sex,nation,born,address,postcode,idnum,mobile) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                        input.name, input.Gender, input.Nationality, input.Birthday, input.address, input.ZipCode, input.IDum, input.phoneNum);//写好想执行的Sql语句   
                    Log.Info("insert bbb=");
                    Log.Info("insert CommandText=" + com.CommandText);
                    return com.ExecuteNonQuery();
                }
                else
                {
                    Log.Info("update aaa=");
                    com.CommandText = string.Format("update population set name='{0}',sex='{1}',nation='{2}',born='{3}',address='{4}',postcode='{5}',mobile='{7}' where idnum='{6}'",
                        input.name, input.Gender, input.Nationality, input.Birthday, input.address, input.ZipCode, input.IDum, input.phoneNum);//写好想执行的Sql语句   
                    Log.Info("update bbb=");
                    Log.Info("update CommandText=" + com.CommandText);
                    return com.ExecuteNonQuery();
                }
            }
        }
        public int GetOrdinal(BusinessModel input)
        {
            var category = input.businessCategory;
            var countycode = input.countyCode;
            var currentdate = DateTime.Now.Date;
            var scurrentdate = string.Format("{0}/{1}/{2}", currentdate.Year, currentdate.Month, currentdate.Day);
         //   Log.Info("000");
            var OracleConnectionconn = new OracleConnection(DataSource);//进行连接           
            try
            {
             //   Log.Info("111");
                OracleConnectionconn.Open();//打开指定的连接  
                OracleCommand com = OracleConnectionconn.CreateCommand();
                com.CommandText =string.Format( "Select businessdate ,ordinal  From businessordinal where category =  '{0}' and countycode='{1}'", category,countycode);//写好想执行的Sql语句                   
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
                                ordinal =odr.GetInt32(i);
                                break;
                        }
                    }
                }
                odr.Close();//关闭reader.这是一定要写的  
             //   Log.Info("222"+ recordDate + "-"+ordinal+ currentdate);
              
                if (scurrentdate == recordDate)
                {
                    com.CommandText =
                           string.Format("update businessordinal set  ordinal = {1} where category = '{0}'and countycode='{2}' ", category, ++ordinal,countycode);
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

        public int dueAndChangeCertification(BusinessModel input)
        {
            using (var OracleConnectionconn = new OracleConnection(DataSource))
            {
                OracleConnectionconn.Open();//打开指定的连接  
                OracleCommand com = OracleConnectionconn.CreateCommand();
                //SELECT  businessSequence.nextval   FROM dual;
                com.CommandText = string.Format("SELECT businessSequence.nextval FROM dual");//写好想执行的Sql语句 
                Log.Info("CommandText=" + com.CommandText);
                OracleDataReader odr = com.ExecuteReader();
                var ordinal = -1;
                while (odr.Read())//读取数据，如果返回为false的话，就说明到记录集的尾部了                    
                {
                    ordinal = odr.GetInt32(0);
                }
                odr.Close();//关闭reader.这是一定要写的  
                Log.Info("sequence.next="+ordinal);
                  var currentdate = DateTime.Now.Date;
                  var filepath = string.Format("d:\\{0}\\{1}", input.countyCode,
                      string.Format("{0}-{1}-{2}", currentdate.Year, currentdate.Month, currentdate.Day));
                  Log.Info("path 11 =" + filepath);
                if (!Directory.Exists(@filepath))
                {
                    Log.Info("path="+filepath);
                    Directory.CreateDirectory(@filepath);
                }
                var filepath2 = string.Format("{0}\\{1}", filepath, ordinal);
                if (!Directory.Exists(@filepath2))
                {
                    Log.Info("filepath2=" + filepath2);
                    Directory.CreateDirectory(@filepath2);
                }
                var filename = string.Format("{0}\\{1}", filepath2, input.fileName);
                Log.Info("file name=" + filename);
                File.WriteAllBytes(filename,input.zipFile);
              
                var scurrentdate = string.Format("{0}/{1}/{2}", currentdate.Year, currentdate.Month, currentdate.Day);
                com.CommandText = string.Format("insert into bussiness (id,type,start_time,status,queue_num,name,id_num,address,phone_num,attention) values({0},{1},'{2}',{3},'{4}','{5}','{6}','{7}','{8}','{9}')",
                    ordinal, input.type, scurrentdate, 3, input.queueNum,input.name,input.IDum,input.address,input.phoneNum,input.attention);//写好想执行的Sql语句   
                Log.Info("insert CommandText=" + com.CommandText);
                return com.ExecuteNonQuery();
            }
        }
    }
}
