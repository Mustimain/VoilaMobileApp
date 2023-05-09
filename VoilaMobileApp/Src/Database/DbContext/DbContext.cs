using System;
using Firebase.Database;

namespace VoilaMobileApp.Src.Database.DbContext
{
    public static class DbContext
    {
        public static FirebaseClient StartFirebaseClient()
        {
            FirebaseClient firebaseClient = new FirebaseClient("https://voilacafeapp-default-rtdb.europe-west1.firebasedatabase.app/");

            return firebaseClient;
        }

    }
}

