using System;
using System.Collections.Generic;
using System.Text;
using XFLocalNotifications.Global;
using XFLocalNotifications.Model;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XFLocalNotifications.ApiController
{
    public class FactoryAPI
    {
        
        public async Task<List<Factory>> GetFactoriesAsync()
        {
            List<Factory> data = new List<Factory>();
            var client = new HttpClient(); 
            HttpResponseMessage response = await client.GetAsync(Global.Global.apiURL+ "getFactory");
            
            string result = await response.Content.ReadAsStringAsync();
            
            if (response.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<List<Factory>>(result);              
            }
            return data;
        }

    }
}
