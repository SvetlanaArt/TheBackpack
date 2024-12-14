using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Text;
using BackpackUnit.Core;
using System;

namespace BackpackUnit.Server
{
    public class PostRequest : IRemoteRequest
    {
        [Serializable]
        struct SendData
        {
            public string id;
            public string acton;
        }

        private string serverUrl;
        private string token;

        public PostRequest(string url, string bearerToken)
        {
            serverUrl = url;
            token = bearerToken;
        }

        public void SendToServer(string objectId, string act)
        {
            if (serverUrl == "")
                return;
            SendData sendData = new SendData(){ id = objectId, acton = act};
            SendPostRequestAsync(serverUrl, sendData);
        }


        private async void SendPostRequestAsync(string url, object requestData)
        {
            string jsonData = JsonUtility.ToJson(requestData);

            byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);

            using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
            {
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
                request.downloadHandler = new DownloadHandlerBuffer();

                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", $"Bearer {token}");

                await request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    ResponseProcessor(request);
                }
                else
                {
                    ErrorHandler(request);
                }
            }
        }

        private static void ResponseProcessor(UnityWebRequest request)
        {
            Debug.Log("Server responce: " + request.downloadHandler.text);
        }

        private static void ErrorHandler(UnityWebRequest request)
        {
            Debug.LogError("Error: " + request.error);
        }


    }
}

