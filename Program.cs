
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Program
{
    [Serializable]
    public class JsonData
    {
       
        public List<JsonDataSet> data = new List<JsonDataSet>();
    }

  //  [Serializable]
    public class JsonDataSet
    {
        [JsonProperty("key")]
        public string key;// { get; set; } = string.Empty;
        [JsonProperty("age")]
        public string age;// { get; set; } = string.Empty;
    }
    public class Root
    {
        public string data { get; set; }
    }
    public static async Task Main()
    {
        string apiUrl = "https://coderbyte.com/api/challenges/json/age-counting";
          List<JsonDataSet> datanew = new List<JsonDataSet>();
    HttpClient client = new HttpClient();
        try
        {
            string response = await client.GetStringAsync(apiUrl);            
            var data = JsonConvert.DeserializeObject<dynamic>(response);
            
            var data2= data["data"].ToString().Split(',');
            int count = 0;
            JsonDataSet newset = new JsonDataSet();
            foreach ( var item in data2)
            {
                
                //Console.WriteLine(item);
                var KeyValue= item.ToString().Split('=');
               
                if (KeyValue[0].ToString().Trim().StartsWith("key"))
                {
                    
                    newset.key = KeyValue[1];
                }
                if (KeyValue[0].Trim().StartsWith( "age"))
                {
                    newset.age = KeyValue[1];
                }
                if(newset.key!=null && newset.age!=null)
                {
                    datanew.Add(newset);
                    newset = new JsonDataSet();
                }
            }            
     
            datanew.RemoveAll(icount=> int.Parse( icount.age)==1);
            foreach (var item in datanew)
            {
               
                if (int.Parse(item.age) == 1)
                {
                    count++;
                }
            }
            var newdataset = datanew;
            var json = JsonConvert.SerializeObject(newdataset);
            Console.WriteLine("new Json updated");
    
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error is :" + ex.Message);
        }
      
    }
   
   
}
