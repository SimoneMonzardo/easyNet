using easyNetAPI.Models;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Company
{
    [BsonElement("company_id")]
    public int CompanyId { get; set; }
    [BsonElement("companyName")]
    public string? CompanyName { get; set; }
    [BsonElement("bot")]
    public Bot? Bot { get; set; }
    [BsonElement("profile_picture")]
    public string? ProfilePicture { get; set; }
}
