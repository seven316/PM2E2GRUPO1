using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PM2E2GRUPO1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class detalleUbicacion : ContentPage
    {
        public detalleUbicacion(String lat, String lng, String desc)
        {
            InitializeComponent();


            double doubleVal = Convert.ToDouble(lat);
            double doubleVal2 = Convert.ToDouble(lng);


            Pin locacion = new Pin();
            locacion.Label = desc;
            locacion.Position = new Position(doubleVal, doubleVal2);
            mapas.Pins.Add(locacion);




            mapas.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(doubleVal, doubleVal2), Distance.FromKilometers(2000)));
        }
    }
}