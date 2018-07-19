using DontPad.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace DontPad.Services
{
    public class DontPadService
    {
        public static async Task<Arquivo> Carregar(List<String> pesquisa, long id)
        {
            String pesquisaa = String.Join("/", pesquisa.ToArray());
            String url = pesquisaa + ".body.json?lastUpdate=" + id;
            String resultado = await System.Threading.Tasks.Task.Run(() => DontPadWebApiService.Ler(url));
            Arquivo arquivo = JsonConvert.DeserializeObject<Arquivo>(resultado);

            if (arquivo.changed)
            {
                return arquivo;
            }
            return null;
        }
        public static async Task<List<String>> CarregarMenu(List<String> pesquisa, long id)
        {
            String pesquisaa = String.Join("/", pesquisa.ToArray());
            String url = pesquisaa + ".menu.json?lastUpdate=" + id;
            String resultado = await System.Threading.Tasks.Task.Run(() => DontPadWebApiService.Ler(url));
            Arquivo arquivo = JsonConvert.DeserializeObject<Arquivo>("{\"menu\":" + resultado + "}");

            return arquivo.menu;
        }
        public static async Task<long> Enviar(String body, List<String> pesquisa)
        {
            Arquivo leitura = new Arquivo();
            String pesquisaa = String.Join("/", pesquisa.ToArray());

            return await System.Threading.Tasks.Task.Run(() => DontPadWebApiService.Escrever(body, pesquisaa.ToString()));
        }

        public static List<string> AdicionarCaminhoAsync(String caminho)
        {
            List<String> pesquisa = new List<String>();

            String pesquisaInicial = caminho;

            if (pesquisaInicial.Contains(".zip"))
            {
                pesquisaInicial = pesquisaInicial.Replace(".zip", "");

                pesquisa.AddRange(pesquisaInicial.Split('/'));

                DownloadAsync(pesquisa);
            }
            else
            {
                pesquisa.AddRange(pesquisaInicial.Split('/'));
            }
            return pesquisa;
        }

        public static async void DownloadAsync(List<String> pesquisa)
        {
            String nome = pesquisa[pesquisa.Count - 1];
            String address = "http://www.dontpad.com/" + String.Join("/", pesquisa.ToArray()) + "/.zip";

            //seleciona a pasta de destino.
            StorageFolder folder = await FolderPickerService.SelectFolderAsync();

            //cria o arquivo
            StorageFile file = await folder.CreateFileAsync(nome + ".zip", CreationCollisionOption.GenerateUniqueName);

            //incia o request
            Stream stream = await Task.Run(() => DontPadWebApiService.DownloadAsync(address));

            //escreve no arquivo
            await Windows.Storage.FileIO.WriteBytesAsync(file, DontPadWebApiService.ReadStream(stream));

            //mensagem de conclusão
            await DialogService.ShowDialogForResults("Arquivo baixado com sucesso", "Download completo!");

        }
    }
}
