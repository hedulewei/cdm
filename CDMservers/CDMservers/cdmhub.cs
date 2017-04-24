using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Common;
using log4net;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace CDMservers
{
    public class Cdmhub : Hub
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly List<CdmClient> CdmClients=new List<CdmClient>(); 
        public void Login(CdmClient str)
        {
            var founded = false;
            foreach (CdmClient cdmClient in CdmClients)
            {
                if (str.CountyCode == cdmClient.CountyCode && str.ClientType == cdmClient.ClientType)
                {
                    cdmClient.ConnectId = Context.ConnectionId;
                    founded = true;
                    Log.Info("client relogin :" + JsonConvert.SerializeObject(cdmClient));
                    break;
                }
            }
            if (!founded)
            {
                str.ConnectId = Context.ConnectionId;
                CdmClients.Add(str);
                Log.Info("new client login:"+JsonConvert.SerializeObject(str));
            }
        }
        public void Message(CdmMessage message)
        {
            Log.Info("11");
            Log.InfoFormat("cdm hub message invoke by {0},{1}",Context.ConnectionId,"");
            Log.Info("22");
            Clients.Caller.Message(message);
            Log.Info("33");
        }
        public void VoiceMessage(CdmMessage message)
        {
         //   Log.InfoFormat("cdm hub VoiceMessage invoke by {0},{1}", Context.ConnectionId, JsonConvert.SerializeObject(message));
            Log.Info("all logined clients:"+JsonConvert.SerializeObject(CdmClients));
            foreach (CdmClient cdmClient in CdmClients)
            {
                if (message.CountyCode == cdmClient.CountyCode && message.ClientType == cdmClient.ClientType)
                {
                    Clients.Client(cdmClient.ConnectId).VoiceMessage(message);
                    return;
                }
            }
            Log.InfoFormat("VoiceMessage:{0} can't find logined client,discarded.", JsonConvert.SerializeObject(message));
        }
        public void LedMessage(CdmMessage message)
        {
            //   Log.InfoFormat("cdm hub VoiceMessage invoke by {0},{1}", Context.ConnectionId, JsonConvert.SerializeObject(message));
            Log.Info("all logined clients:" + JsonConvert.SerializeObject(CdmClients));
            foreach (CdmClient cdmClient in CdmClients)
            {
                if (message.CountyCode == cdmClient.CountyCode && message.ClientType == cdmClient.ClientType)
                {
                    Clients.Client(cdmClient.ConnectId).LedMessage(message);
                    return;
                }
            }
            Log.InfoFormat("LedMessage:{0} can't find logined client,discarded.", JsonConvert.SerializeObject(message));
        }
    }
}