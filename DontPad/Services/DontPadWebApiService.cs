using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace DontPad.Services
{
    public class DontPadWebApiService
    {
        public static async Task<String> Ler(String url)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

                var uri = new Uri("http://www.dontpad.com/" + url);
                var httpClient = new HttpClient();

                var resultado = await httpClient.GetStringAsync(uri);
                return resultado;
            }
            catch
            {
                ContentDialog erro = new ContentDialog()
                {
                    Title = "Erro!",
                    Content = "Falha ao conectar-se a internet, tente novamente mais tarde.",
                    PrimaryButtonText = "Ok"
                };

                ContentDialogResult result = await erro.ShowAsync();
            }

            return null;

        }
        public static async Task<long> Escrever(String text, String url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://www.dontpad.com/" + url);
                    var content = new FormUrlEncodedContent(new[]
                    {
                new KeyValuePair<string, string>("text", text)
                });
                    var result = await client.PostAsync("", content);
                    String resultContent = await result.Content.ReadAsStringAsync();
                    return Convert.ToInt64(resultContent);
                }
            }
            catch
            {
                ContentDialog erro = new ContentDialog()
                {
                    Title = "Erro!",
                    Content = "Falha ao conectar-se a internet, tente novamente mais tarde.",
                    PrimaryButtonText = "Ok"
                };

                ContentDialogResult result = await erro.ShowAsync();
            }
            return 0;
        }
        public static async Task<Stream> DownloadAsync(String url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                WebResponse response = await request.GetResponseAsync();
                Stream stream = response.GetResponseStream();
                return stream;
            }
            catch
            {
                ContentDialog erro = new ContentDialog()
                {
                    Title = "Erro!",
                    Content = "Falha ao conectar-se a internet, tente novamente mais tarde.",
                    PrimaryButtonText = "Ok"
                };

                ContentDialogResult result = await erro.ShowAsync();
                return null;
            }
        }

        public static byte[] ReadStream(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }

        }
    }
}
