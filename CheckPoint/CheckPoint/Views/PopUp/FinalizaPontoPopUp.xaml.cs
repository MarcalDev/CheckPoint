using CheckPoint.ViewModels;
using CheckPointBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using CheckPoint.Controls;

namespace CheckPoint.Views.PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinalizaPontoPopUp : Popup
    {
        FinalizaPontoViewModel _finalizaPontoViewModel;

        Map mapa;
        public FinalizaPontoPopUp(Ponto ponto)
        {
            InitializeComponent();

            _finalizaPontoViewModel = new FinalizaPontoViewModel();
            _finalizaPontoViewModel = BindingContext as FinalizaPontoViewModel;

            //_finalizaPontoViewModel.CarregaDados();
            _finalizaPontoViewModel.Ponto = ponto;            

            CarregaDados().GetAwaiter();

            PermissaoGPSAsync().GetAwaiter();



            LblHoraAtual.Text = DateTime.Now.ToString("hh:mm:ss");
        }




        public async Task CarregaDados()
        {
            CriarMapa();
            await PermissaoGPSAsync();
            await _finalizaPontoViewModel.GeocodEnderecoAsync();
            LblEndereco.Text = _finalizaPontoViewModel.Endereco;

        }

        public async Task PermissaoGPSAsync()
        {
            try
            {
                // Cria váriavel para armazenar o estado das permissões
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

                // Caso as permissões estejam desativadas
                if (status != PermissionStatus.Granted)
                {
                    // Verifica se deve fazer a requisição da ativação de permissões
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await App.Current.MainPage.DisplayAlert("Usar localização", "Por favor, autorize a utilização da localização", "OK");
                    }

                    // Atribui a váriavel o pedido de ativação das permissões
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }

                // Caso as Permissões estejam ativadas
                if (status == PermissionStatus.Granted)
                {
                    // Instancia o objeto gps
                    GPS gps = new GPS();

                    // Caso a localização esteja disponível
                    if (gps.IsLocationAvailable())
                    {
                        // Intancia a Position e requisita um retorno de localização
                        Plugin.Geolocator.Abstractions.Position pos = gps.GetCurrentLocation().GetAwaiter().GetResult();


                        double p1 = pos.Latitude;
                        double p2 = pos.Longitude;

                        _finalizaPontoViewModel.Latitude = p1;
                        _finalizaPontoViewModel.Longitude = p2;



                        // Caso o retorno não esteja vazio
                        if (pos != null)
                        {
                            // Instancia novo ponteiro, utilizando a localização trazida no retorno (pos)
                            var MyPos = new Pin()
                            {
                                Position = new Position(pos.Latitude, pos.Longitude),
                                Label = "Minha Posição"

                            };

                            // Adiciona esse ponteiro no mapa
                            mapa.Pins.Add(MyPos);



                        }
                    }

                    // Define o mapa como visível
                    mapa.IsShowingUser = true;
                }

                // Caso o estado de permissão seja desconhecido
                else if (status != PermissionStatus.Unknown)
                {
                    await App.Current.MainPage.DisplayAlert("Localização negada", "Não foi possível continuar, tente novamente.", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alerta!", ex.Message, "OK");
            }
        }

        public void CriarMapa()
        {
            // Atribui a mapa um Map e adiciona uma posição, definindo a distancia em quilometros
            mapa = new Map(MapSpan.FromCenterAndRadius(new Position(_finalizaPontoViewModel.Latitude, _finalizaPontoViewModel.Longitude), Distance.FromKilometers(1)));

            // Define o tipo de mapa
            mapa.MapType = MapType.Street;

            // Define mapa como conteúdo do stackLayout
            MapContainer.Children.Add(mapa);
        }
    }
}