using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Common;
using log4net;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;

namespace CDMservers
{
    public static class MessagePush
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IHubProxy HubProxy { set; get; }
        private static HubConnection Connection { get; set; }
        static MessagePush()
        {
            var server = string.Format("http://{0}/", "localhost:8000");
            Connection = new HubConnection(server);
            HubProxy = Connection.CreateHubProxy("Cdmhub");
            Log.InfoFormat("signalr 查询: {0}", "CreateHubProxy ok");
        }
        public async static Task<int> PushVoiceMessage(CdmMessage mt)
        {
            if (Connection.State.Equals(Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected))
            {
                try
                {
                    await Connection.Start();
                }
                catch (HttpRequestException hex)
                {
                    Log.Error("pushmsg.HttpRequestException" +hex.Message);
                    return -1;
                }
                catch (Exception ex)
                {
                    Log.Error("pushmsg." + ex.Message);
                    return -2;
                }
            }
            try
            {
                await HubProxy.Invoke("VoiceMessage", mt);
            }
            catch (Exception ex)
            {
                Log.Error("HubProxy.Invoke(PushMsg, mt);." + ex.Message);
                return -3;
            }
            return 0;
        }
        public async static Task<int> PushLedMessage(CdmMessage mt)
        {
            if (Connection.State.Equals(Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected))
            {
                try
                {
                    await Connection.Start();
                }
                catch (HttpRequestException hex)
                {
                    Log.Error("pushledmsg.HttpRequestException" + hex.Message);
                    return -1;
                }
                catch (Exception ex)
                {
                    Log.Error("pushledmsg." + ex.Message);
                    return -2;
                }
            }
            try
            {
                await HubProxy.Invoke("LedMessage", mt);
            }
            catch (Exception ex)
            {
                Log.Error("HubProxy.Invoke(PushLedMsg, mt);." + ex.Message);
                return -3;
            }
            return 0;
        }
    }
}