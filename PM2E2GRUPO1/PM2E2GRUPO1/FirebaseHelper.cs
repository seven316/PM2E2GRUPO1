using System;
using System.Collections.Generic;
using System.Text;

using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PM2E2GRUPO1
{
    class FirebaseHelper
    {



            FirebaseClient firebase = new FirebaseClient("https://prjctxamarin-default-rtdb.firebaseio.com/");


            public async Task<List<Ubicacion>> GetAllPersons()
            {
            try

            {

               var value = (await firebase
                  .Child("Ubicacion")
                  .OnceAsync<Ubicacion>()).Select(item => new Ubicacion
                  {
                      id = item.Key,
                      descripcion = item.Object.descripcion,
                      lat = item.Object.lat,
                      lng = item.Object.lng,

                  }).ToList();
                return value;

            }
            catch (Exception e)
            {
      
                return null;
            }
        }


        public async Task AddUbicacion(String descripcion, String lat, String lng, String foto)
        {

            await firebase
              .Child("Ubicacion")
              .PostAsync(new Ubicacion() { descripcion = descripcion, lat = lat, lng = lng, foto = foto});
        }

        public async Task DeletePerson(String personId)
        {
            var toDeletePerson = (await firebase
              .Child("Ubicacion")
              .OnceAsync<Ubicacion>()).Where(a => a.Key == personId).FirstOrDefault();
            await firebase.Child("Ubicacion").Child(toDeletePerson.Key).DeleteAsync();

        }


        public async Task UpdatePerson(String personId, string Descripcion)
        {
            var toUpdatePerson = (await firebase
              .Child("Ubicacion")
              .OnceAsync<Ubicacion>()).Where(a => a.Key == personId).FirstOrDefault();

            await firebase
              .Child("Ubicacion")
              .Child(toUpdatePerson.Key)
              .PutAsync(new Ubicacion() { descripcion = Descripcion });
        }




        public async Task<Ubicacion> GetPerson(String desc)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("Ubicacion")
              .OnceAsync<Ubicacion>();
            return allPersons.Where(a => a.descripcion == desc).FirstOrDefault();
        }

    }
}

