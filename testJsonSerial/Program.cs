// See https://aka.ms/new-console-template for more information

using System.Text.Json.Serialization;





Console.WriteLine("Hello, World!");


TestJson json = new TestJson()
{
    Sn = "12233",
    Id = "123"
};


var newtontest = Newtonsoft.Json.JsonConvert.SerializeObject(json);

var itextJsonTest = System.Text.Json.JsonSerializer.Serialize(json);
Console.ReadKey();



public class TestJson
{
    [System.Text.Json.Serialization.JsonPropertyName("gwsn")]
    public string Sn { get; set; }

    [Newtonsoft.Json.JsonProperty("id")]
    public string Id { get;set; }
}