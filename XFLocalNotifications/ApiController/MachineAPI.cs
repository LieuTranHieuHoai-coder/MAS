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
            try
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
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<string> UpdateMachineAlertAsync(string idButton,string idLine, string idFactory)
        {
            try
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
            catch (Exception ex)
            {

                return ex.Message;
            }
            
        }

        public async Task<string> ScanMachineAlertAsync(string idButton, string idLine, string idFactory)
        {
            try
            {
                List<MachineAlert> data = new List<MachineAlert>();
                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(Global.Global.apiURL + "completeFix&ID_Button=" + idButton + "&ID_Line=" + idLine + "&ID_Factory=" + idFactory);
                _ = await client.GetAsync(Global.Global.apiURL + "updateTime&ID_ButtonTime=" + idButton + "&ID_Line=" + idLine + "&ID_Factory=" + idFactory);

                string result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    if (result.Contains("notUpdate"))
                    {
                        return null;
                    }
                    else
                    {
                        return "wasUpdate";
                    }
                }

                return "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
