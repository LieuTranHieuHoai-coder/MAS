using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XFLocalNotifications.Model;

namespace XFLocalNotifications.ApiController
{
    public class MachineAPI
    {
        public async Task<List<MachineAlert>> GetMachineAlertAsync()
        {
            List<MachineAlert> data = new List<MachineAlert>();
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(Global.Global.apiURL + "getAlertList&id_fact=" + Global.Global.globalFactory);

            string result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                if (result.Contains("NoBrokenMachine"))
                {
                    return null;
                }
                else
                {
                    data = JsonConvert.DeserializeObject<List<MachineAlert>>(result);
                }   
            }

            return data;
        }

        public async Task<string> UpdateMachineAlertAsync(string idButton,string idLine, string idFactory)
        {
            List<MachineAlert> data = new List<MachineAlert>();
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(Global.Global.apiURL + "updateTime&ID_ButtonTime=" + idButton + "&ID_Line=" + idLine + "&ID_Factory=" + idFactory);

            string result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                if (result.Contains("NoBrokenMachine"))
                {
                    return null;
                }
                else
                {
                    return "wasUpdateTime";
                }
            }

            return "Error";
        }
    }
}
