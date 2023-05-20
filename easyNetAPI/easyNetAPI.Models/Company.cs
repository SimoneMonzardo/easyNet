using easyNetAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Company
{
    [JsonProperty("company_id")]
    public int CompanyId { get; set; }
    [JsonProperty("companyName")]
    public string? CompanyName { get; set; }
    public Bot? Bot { get; set; }
}
