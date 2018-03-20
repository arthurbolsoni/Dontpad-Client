using DontPad.Base;
using DontPad.Model;
using DontPad.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace DontPad.ViewModels
{
    public class ConteudoViewModel : ViewModelBase
    {
        ThreadPoolTimer PeriodicTimer;
        private bool modificar = false;

        private List<String> pesquisa;

        private String _cPesquisa;
        public String cPesquisa { get => _cPesquisa; set { _cPesquisa = value; RaisePropertyChanged(); } }
            
        private bool _ProgressRing;
        public bool ProgressRing { get => _ProgressRing; set { _ProgressRing = value; RaisePropertyChanged(); } }

        private bool _ProgressBar;
        public bool ProgressBar { get => _ProgressBar; set { _ProgressBar = value; RaisePropertyChanged(); } }

        private String _cPagina;
        public String cPagina{ get => _cPagina; set { _cPagina = value; RaisePropertyChanged(); } }

        private Arquivo _arquivo;
        public Arquivo arquivo { get => _arquivo; set { _arquivo = value; RaisePropertyChanged(); } }

        private String _SelectedItem { get; set; }
        public String SelectedItem
        {
            set
            {
                _SelectedItem = value;
                atualizar(value);
            }
        }

        private ObservableCollection<String> _items = new ObservableCollection<String>();
        public ObservableCollection<String> Items
        {
            get => _items;
            set
            {
                _items = new ObservableCollection<String>(value);
                RaisePropertyChanged();
            }
        }

        private RelayCommand _KeyUpCommand;
        public RelayCommand KeyUpCommand => _KeyUpCommand = new RelayCommand(() => modificar = true);

        private RelayCommand _RefreshCommand;
        public RelayCommand RefreshCommand => _RefreshCommand = new RelayCommand(() => atualizar());

        private RelayCommand _ShareCommand;
        public RelayCommand ShareCommand => _ShareCommand = new RelayCommand(() => PlatformService.Share(arquivo.body));

        private RelayCommand _CopyCommand;
        public RelayCommand CopyCommand => _CopyCommand = new RelayCommand(() => PlatformService.CopyTextToClipboard(arquivo.body));

        private RelayCommand _NewItemCommand;
        public RelayCommand NewItemCommand => _NewItemCommand = new RelayCommand(() => novoItemMenu());

        private RelayCommand _DownloadCommand;
        public RelayCommand DownloadCommand => _DownloadCommand = new RelayCommand(() => DontPadService.DownloadAsync(pesquisa));

        private RelayCommand _GoBackCommand;
        public RelayCommand GoBackCommand => _GoBackCommand = new RelayCommand(() => base.NavigationService.GoBack());

        public ConteudoViewModel(INavigationService navigationService) : base(navigationService)
        {

        }

        private void interval()
        {
            TimeSpan period = TimeSpan.FromMilliseconds(2000);
            PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer(async (source) =>
            {
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    async () =>
                    {
                        try
                        {
                            await carregaOuEnviarAsync();
                        }
                        catch
                        {
                            PeriodicTimer.Cancel();
                        }
                    });
            }, period);
        }

        private async Task carregaOuEnviarAsync()
        {
            if (modificar)
            {
                await DontPadService.Enviar(arquivo.body, pesquisa);
                modificar = false;
            }
            else
            {
                Arquivo arq = await Task.Run(() => DontPadService.Carregar(pesquisa, arquivo.lastUpdate));
                if (arq != null)
                {
                    arquivo = arq;
                }
            }
        }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            pesquisa = DontPadService.AdicionarCaminhoAsync(e.Parameter.ToString());
            atualizar();
            interval();
            base.OnNavigatedTo(e);
        }

        public async void atualizar(String caminho = null)
        {
            if (caminho != null)
            {
                String cPesquisa = pesquisa == null ? caminho : String.Join("/", pesquisa.ToArray()) + "/" + caminho;
                pesquisa = DontPadService.AdicionarCaminhoAsync(cPesquisa);
            }

            //Zera  o menu e o conteudo
            arquivo = new Arquivo();
            Items = new ObservableCollection<string>();

            ProgressBar = true;
            ProgressRing = true;

            //Muda variaveis da tela
            cPesquisa = String.Join("/", pesquisa.ToArray());
            cPagina = pesquisa[pesquisa.Count() - 1];

            //Inicia os requests
            Arquivo arq = await Task.Run(() => DontPadService.Carregar(pesquisa, arquivo.lastUpdate));
            if (arq != null)
            {
                arquivo = arq;
            }

            List<String> menu = await Task.Run(() => DontPadService.CarregarMenu(pesquisa, arquivo.lastUpdate));
            Items = new ObservableCollection<String>(menu as List<String>);

            ProgressBar = false;
            ProgressRing = false;
        }

        public async void novoItemMenu()
        {
            String item = await Services.DialogService.ShowDialogForResults("Será salvo quando ouver conteúdo.", "Adicionar nova pasta.");

            List<String> list = Items.ToList();

            if (list.Contains(item) || item == null)
            {
                return;
            }

            list.Add(item);
            list = list.OrderBy(x => x).ToList();
            Items = new ObservableCollection<String>(list as List<String>);
        }

        public override bool OnBackRequested()
        {
            if (pesquisa.Count == 1)
            {
                PeriodicTimer.Cancel();
                return true;
            }
            else
            {
                pesquisa.RemoveAt(pesquisa.Count() - 1);
                atualizar();
            }

            base.OnBackRequested();
            return false;
        }
    }
}
