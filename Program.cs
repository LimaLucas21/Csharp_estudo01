using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Digite o Cep desejado: ");
        string CEP = Console.ReadLine();

        var client = new HttpClient();
        var url = "https://viacep.com.br/ws/"+ CEP + "/json/";

        var resposta = await client.GetAsync(url);

        if(resposta.IsSuccessStatusCode)
        {
            var obj_resp = await resposta.Content.ReadAsStringAsync();

            var objApi = JsonSerializer.Deserialize<Api>(obj_resp);

            Console.WriteLine(
                "Cep: " + objApi.cep + "\n" +
                "Logradouro: " + objApi.logradouro);
        }
        else
        {
            Console.WriteLine("A solicitação falhou devido: " + resposta.StatusCode);
            Console.WriteLine(await resposta.Content.ReadAsStringAsync());
        }
    }
}
