using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Amigos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Listar_Click(object sender, RoutedEventArgs e)
        {
            for (int k = ListaAmigos.Items.Count - 1; k >= 0; --k)
            {
                ListaAmigos.Items.RemoveAt(k);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54321/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                
                // Obtener todos los elementos
                HttpResponseMessage response = client.GetAsync("api/amigo/").Result;
                if (response.IsSuccessStatusCode)
                {
                    Amigo[] amigos = response.Content.ReadAsAsync<Amigo[]>().Result;
                    foreach (Amigo amigo in amigos)
                        ListaAmigos.Items.Add(amigo.ID+"-"+amigo.name);
                }

                
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Detalles_Click(object sender, RoutedEventArgs e)
        {
            for (int k = DetallesAmigo.Items.Count - 1; k >= 0; --k)
            {
                DetallesAmigo.Items.RemoveAt(k);
            }

            string friend = ListaAmigos.SelectedItem.ToString();
            string[] S = friend.Split('-');
            string id = S[0];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54321/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/amigo/"+id).Result;
                if (response.IsSuccessStatusCode)
                {
                    Amigo amigo = response.Content.ReadAsAsync<Amigo>().Result;
                    DetallesAmigo.Items.Add("Nombre: "+amigo.name);
                    DetallesAmigo.Items.Add("Longitud: " + amigo.longi);
                    DetallesAmigo.Items.Add("Latitud: " + amigo.lati);

                }
            }
        }

        private void DetallesAmigo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54321/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                CajaLat.SelectAll();
                CajaNombre.SelectAll();
                CajaLon.SelectAll();
                Amigo pepe = new Amigo() { name = CajaNombre.SelectedText, longi = CajaLon.SelectedText, lati = CajaLat.SelectedText };
                HttpResponseMessage response = client.PostAsJsonAsync("api/amigo", pepe).Result;

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(this, "Amigo creado con éxito", "200 OK", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }

        private void Borrar_Click(object sender, RoutedEventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54321/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                string friend = ListaAmigos.SelectedItem.ToString();
                string[] S = friend.Split('-');
                string id = S[0];

                HttpResponseMessage response = client.DeleteAsync("api/amigo/"+id).Result;
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(this, "Amigo borrado con éxito", "200 OK", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            string friend = ListaAmigos.SelectedItem.ToString();
            string[] S = friend.Split('-');
            string id = S[0];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:54321/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                CajaLat.SelectAll();
                CajaNombre.SelectAll();
                CajaLon.SelectAll();

                Amigo pepe = new Amigo() { name = CajaNombre.SelectedText, longi = CajaLon.SelectedText, lati = CajaLat.SelectedText};
                pepe.ID = Convert.ToInt16(id);
                HttpResponseMessage response = client.PutAsJsonAsync("api/amigo/"+id, pepe).Result;

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(this, "Amigo editado con éxito", "200 OK", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
        }

    }
}
