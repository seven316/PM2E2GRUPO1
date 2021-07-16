using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace PM2E2GRUPO1
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaUbicaciones : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public ListaUbicaciones()
        {
            InitializeComponent();




        }

        Ubicacion itemUbicacion = new Ubicacion();
        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var allPersons = await firebaseHelper.GetAllPersons();
            listaUbicaciones.ItemsSource = allPersons;
        }

        private async void listaUbicaciones_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            itemUbicacion = (Ubicacion)e.Item;
            String desc = itemUbicacion.descripcion;

            var ubicacions = await firebaseHelper.GetPerson(desc);
            if (ubicacions != null)
            {
                var a = ubicacions.lat.ToString();
                var b = ubicacions.lng.ToString();
                var c = ubicacions.descripcion.ToString();
               
                await Navigation.PushAsync(new detalleUbicacion(a,b,c));

            }
            else
            {
                await DisplayAlert("Success", "No Person Available", "OK");
            }

        }
    }
}