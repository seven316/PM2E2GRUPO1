using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
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

      





        private async void listaUbicaciones_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            itemUbicacion = (Ubicacion)e.SelectedItem;



        }

        private async void verItem_Clicked(object sender, EventArgs e)
        {
            if (itemUbicacion.lat != null)
            {
                String desc = itemUbicacion.descripcion;



                var ubicacions = await firebaseHelper.GetPerson(desc);


                if (ubicacions != null)
                {
                    var a = ubicacions.lat.ToString();
                    var b = ubicacions.lng.ToString();
                    var c = ubicacions.descripcion.ToString();
                   
                    await Navigation.PushAsync(new detalleUbicacion(a, b, c));

                }
                else
                {
                    await DisplayAlert("Success", "Ubicacion no disponible", "OK");
                }


            }

            else
            {
                await this.DisplayToastAsync("Asegurate de seleccionar una ubicacion", 10000);
            }
        }

        


       

        private async void editarItem_Clicked(object sender, EventArgs e)
        {
            if (txtDescripcion.Text != null)
            {
                if (itemUbicacion.lat != null)
                {
                    var d = itemUbicacion.id;
                    await firebaseHelper.UpdatePerson(Convert.ToString(d), txtDescripcion.Text);
                    txtDescripcion.Text = string.Empty;
                   
                    await DisplayAlert("Success", "Ubicacion Modificada Satisfactoriamente", "OK");
                    var allPersons = await firebaseHelper.GetAllPersons();
                    listaUbicaciones.ItemsSource = allPersons;

                }
                else
                {
                    await this.DisplayToastAsync("Asegurate de seleccionar una ubicacion", 10000);
                }
            }
            else
            {
                await this.DisplayToastAsync("Asegurate de llenar los campos necesarios", 10000);
            }
        }

        private async void borrarItem_Clicked(object sender, EventArgs e)
        {
            if (itemUbicacion.lat != null)
            {
                var d = itemUbicacion.id;

                await firebaseHelper.DeletePerson(Convert.ToString(d));
                await DisplayAlert("Success", "Ubicacion Eliminada Exitosamente", "OK");
                var allPersons = await firebaseHelper.GetAllPersons();
                listaUbicaciones.ItemsSource = allPersons;


            }

            else
            {
                await this.DisplayToastAsync("Asegurate de seleccionar una ubicacion", 10000);
            }
        }
    }
}