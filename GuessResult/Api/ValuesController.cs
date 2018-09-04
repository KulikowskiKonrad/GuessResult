using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GuessResult.Api
{
 public class ValuesController : ApiController
{
    static string _address = "https://apifootball.com/api/?action=get_odds&from=2017-02-13&to=2017-02-13&APIkey=ca484e2080320b8fda0139ce774b264c31507afcec8b231286d93eaa98ebf5ab";
    private string result;

    // GET api/values
    public IEnumerable<string> Get()
    {
        GetResponse();
        return new string[] { result, "value2" };
    }

    private async void GetResponse()
    {
        var client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(_address);
        response.EnsureSuccessStatusCode();
        result = await response.Content.ReadAsStringAsync();
    }
}
}