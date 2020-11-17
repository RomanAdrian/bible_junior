//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public static class FirebaseAuthHandler
//{
//    private const string ApiKey = " from Firebase projecct ";
//    public static void SignInWithToken(string idToken, string providerId)
//    {
//        var payLoad = $" from Firebase OAuth";
//        RestClient.Post($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithIdp?key={ApiKey}", payLoad).Then(
//            response =>
//            {
//                Debug.Log(response.Text);
//            });
//    }
//}
