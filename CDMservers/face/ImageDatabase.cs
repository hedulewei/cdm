//----------------------------------------------------------------------------
//  Copyright (C) 2004-2017 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace face
{
   public sealed class ImageDatabase
   {
      //private const string CurrentSessionKey = "nhibernate.current_session";
      private static readonly ISessionFactory sessionFactory;
      private static ISession _currentSession;
       public static string connection_string;
      private static TraceSource GetTrace(string name)
      {
          var trace = new TraceSource(name, SourceLevels.Information);
          trace.Listeners.Add(
              new TextWriterTraceListener(Path.Combine(Path.GetTempPath(),
                  "TraceLog" + DateTime.Now.ToString("yyyy-MM-dd") + ".log")));
          return trace;
      }
      static ImageDatabase()
      {
         
          var ts = GetTrace(MethodBase.GetCurrentMethod().Name);
          try
          {
              Configuration cfg = new Configuration().Configure("SqliteDB.XML");
              ts.TraceInformation("error: {0}", 111);
              String dbFileName = "test.db";
              connection_string = String.Format("Data Source={0};Version=3", dbFileName);
              cfg.Properties["connection.connection_string"] = connection_string;
              ts.TraceInformation("error: {0}", 222);
              cfg.AddAssembly(typeof (ImageDatabase).Assembly);
              ts.TraceInformation("error: {0}", 333);
              //Create the table if this is a new database
              if (!System.IO.File.Exists(dbFileName))
              {
                  ts.TraceInformation("error: {0}", 444);
                  new SchemaExport(cfg).Execute(false, true, false);
                  
              }

              ts.TraceInformation("error: {0}", 555);
              sessionFactory = cfg.BuildSessionFactory();
          }
          catch (Exception ex)
          {
              ts.TraceInformation(string.Format("error: {0}",ex));
          }
          finally
          {
              ts.Close();
          }
      }

      public static ISession GetCurrentSession()
      {
         if (_currentSession == null || !_currentSession.IsOpen )
            _currentSession = sessionFactory.OpenSession();
         return _currentSession;
      }

      public static void CloseSession()
      {
         if (_currentSession == null)
         {
            // No current session
            return;
         }

         _currentSession.Close();
         _currentSession = null;
      }

      public static void CloseSessionFactory()
      {
         if (sessionFactory != null)
         {
            sessionFactory.Close();
         }
      }
   }
}
