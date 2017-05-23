using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestAmigos
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54321/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Obtener información de un elemento
                HttpResponseMessage response = client.GetAsync("api/amigo/1").Result;
                if (response.IsSuccessStatusCode)
                {
                    Amigo amigo = response.Content.ReadAsAsync<Amigo>().Result;
                    Console.WriteLine("{0}\t{1}\t{2}", amigo.name, amigo.longi, amigo.lati);
                }

                // Obtener todos los elementos
                response = client.GetAsync("api/amigo/").Result;
                if (response.IsSuccessStatusCode)
                {
                    Amigo[] amigos = response.Content.ReadAsAsync<Amigo[]>().Result;
                    foreach (Amigo amigo in amigos)
                        Console.WriteLine("{0} {1}:\t{2}\t{3}", amigo.name, amigo.ID, amigo.longi, amigo.lati);
                }

                // Crea un nuevo elemento
                Amigo pepe = new Amigo() { name = "pepe", longi = "100", lati = "100" };
                response = client.PostAsJsonAsync("api/amigo", pepe).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Amigo creado con éxito. URL: {0}", response.Headers.Location);
                }

                // Modifica un elemento
                pepe.name = "Rubén";
                pepe.ID = 6;
                response = client.PutAsJsonAsync("api/amigo/6", pepe).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Amigo modificado con éxito.");
                }

                // Borrar un elemento
                response = client.DeleteAsync("api/amigo/6").Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Amigo borrado con éxito.");
                }
            }
        }
    }
}
