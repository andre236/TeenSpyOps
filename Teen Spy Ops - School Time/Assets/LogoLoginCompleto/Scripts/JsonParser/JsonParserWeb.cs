using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace JsonsUnip
{
    public class JsonParserWeb : MonoBehaviour
    {
        public static JsonParserWeb instance;

        private static JsonString jsonString;

        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public IEnumerator GetWebRequest(string url)
        {
            jsonString = null;
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(request.error);
            }
            else
            {
                jsonString = JsonConvert.DeserializeObject<JsonString>(request.downloadHandler.text);
            }
        }


        private IEnumerator WaitGetRequest(string url, System.Action<bool> callback)
        {
            yield return StartCoroutine(GetWebRequest(url));
            callback(true);
        }

        public void JsonStringReturn(string url, string fuctionName)
        {
            StartCoroutine(WaitGetRequest(url, (callback) =>
            {
                if (callback)
                {
                    SendMessage(fuctionName);
                }
            }));
        }

        public JsonString JStringReturnValue()
        {
            return jsonString;
        }
    }
}