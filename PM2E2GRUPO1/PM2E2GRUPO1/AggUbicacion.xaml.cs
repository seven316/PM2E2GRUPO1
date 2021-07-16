
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E2GRUPO1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AggUbicacion : ContentPage
    {
        Byte[] StringA { get; set; }
       String str { get; set; }
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public AggUbicacion(Double lat, Double lng)
        {
            InitializeComponent();

            txtlatitud.Text = lat.ToString();

            txtlongitud.Text = lng.ToString();
        }


        protected async override void OnAppearing()
        {
            var tomarfoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "MiApp",
                Name = "Prueba.jpg"
              

            });







            if (tomarfoto != null)
            {
                foto.Source = ImageSource.FromStream(() =>
                {
                    return tomarfoto.GetStream();
                });
            }

            byte[] imageArray = null;

            //Insercion de mapa de bytes
            using (MemoryStream memory = new MemoryStream())
            {

                Stream stream = tomarfoto.GetStream();
                stream.CopyTo(memory);
                imageArray = memory.ToArray();
            }

         

            string str = Encoding.Default.GetString(imageArray);

        }
    
    
        private async void guardarUbicacion_Clicked(object sender, EventArgs e)
        {
   

            if (String.IsNullOrWhiteSpace(txtdescripcion.Text))
            {
                await this.DisplayToastAsync("Asegurate de llenar los campos vacios", 10000);

            }
            else
            {
                await firebaseHelper.AddUbicacion(txtdescripcion.Text, txtlatitud.Text, txtlongitud.Text, str);
                txtdescripcion.Text = string.Empty;
         
                await DisplayAlert("Success", "Person Added Successfully", "OK");
         
            }
   
        }

        private async void verListaUbicaciones_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new ListaUbicaciones());
        }
    }
}