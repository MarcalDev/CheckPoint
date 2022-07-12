using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace CheckPoint.Controls
{
    public class GPS
    {
        public bool IsLocationAvailable()
        {
            // Verifica se o dispositivo suporta o plugin
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }

        public async Task<Position> GetCurrentLocation()
        {
            Position position = null;
            try
            {
                var locator = CrossGeolocator.Current;
                // Define a precisão do o raio da localização (Metros)
                locator.DesiredAccuracy = 1000;

                // Define a posição atual como a última a ser detectada (salva no cache)
                position = await locator.GetLastKnownLocationAsync();

                if (position != null)
                {
                    // Retorna posição válida
                    return position;
                }

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    // Retorna plugin não disponível
                    return position;
                }

                // Chama o método Get da posição atual.
                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(120), null, true);



            }
            catch (Exception ex)
            {
                //Exceção para erros
            }

            // Caso a posição seja inválida
            if (position == null)
                return position;

            return position;
        }
    }
}
