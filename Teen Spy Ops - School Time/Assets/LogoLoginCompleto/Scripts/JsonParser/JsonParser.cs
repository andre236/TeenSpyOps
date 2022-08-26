using Newtonsoft.Json;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace JsonsUnip
{
    public static class JsonParser
    {

        #region BASE FUNCTIONS
        public static T[,,] FromJson<T>(string json) //Load Json
        {
            Wrapper<T> wrapper = JsonConvert.DeserializeObject<Wrapper<T>>(json);
            return wrapper.data;
        }

        public static string ToJson<T>(T[,,] array) //Save as Json
        {
            Wrapper<T> wrapper = new Wrapper<T>();

            wrapper.data = array;

            for (int i = 0; i < wrapper.size.Length; i++)
            {

                if (wrapper.data == null)
                {
                    wrapper.data = new T[1, 1, 1];
                }
                wrapper.size[i] = wrapper.data.GetLength(i);

            }

            return JsonConvert.SerializeObject(wrapper, Formatting.Indented);
        }

        public static void SaveJson<T>(string path, T[,,] array) //Save Fuction
        {
            string contents = ToJson(array);
            File.WriteAllText(path, contents);
        }

        public static T[,,] LoadJson<T>(string path) //Load Fuction
        {
            string contents = File.ReadAllText(path);
            Wrapper<T> wrapper = JsonConvert.DeserializeObject<Wrapper<T>>(contents);
            return wrapper.data;
        }

        #endregion

        #region UTILITY FUNCTIONS
        public static T[,,] LoadJsonResources<T>(string path) //Load from Resources
        {
            var contents = Resources.Load<TextAsset>(path);
            Wrapper<T> wrapper = JsonConvert.DeserializeObject<Wrapper<T>>(contents.text);
            return wrapper.data;
        }

        public static T[,,] LoadJsonStreamAssets<T>(string path)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, path + ".json");
            string contents = File.ReadAllText(filePath);
            Wrapper<T> wrapper = JsonConvert.DeserializeObject<Wrapper<T>>(contents);
            return wrapper.data;
        }

        public static void SaveJsonStreamAssets<T>(string path, T[,,] array) //Save Fuction
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, path + ".json");
            string contents = ToJson(array);
            File.WriteAllText(filePath, contents);
        }
        #endregion

        #region STRING DATA CLASS

        public static JsonString LoadJsonDataClass(string path) //Load String Data (Class)
        {
            string contents = File.ReadAllText(path);
            JsonString jString = JsonConvert.DeserializeObject<JsonString>(contents);
            return jString;
        }

        public static void SaveJsonDataClass(string path, JsonString jString) //Save String Data (Class)
        {
            for (int i = 0; i < jString.size.Length; i++)
            {

                if (jString.data == null)
                {
                    jString.data = new string[1, 1, 1];
                }

                jString.size[i] = jString.data.GetLength(i);
            }

            string contents = JsonConvert.SerializeObject(jString, Formatting.Indented);
            File.WriteAllText(path, contents);
        }

        public static JsonString LoadJsonDataClassResources(string path) //Load from Resources Data Class
        {
            var contents = Resources.Load<TextAsset>(path);
            JsonString jString = JsonConvert.DeserializeObject<JsonString>(contents.text);
            return jString;
        }

        public static JsonString LoadJsonDataClassStreamAssets(string path) //Load from Resources Data Class (StreamingAssets)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, path + ".json");
            string contents = File.ReadAllText(filePath);
            JsonString wrapper = JsonConvert.DeserializeObject<JsonString>(contents);
            return wrapper;
        }

        public static void SaveJsonDataClassStreamAssets(string path, JsonString jstring) //Save to Data Class (StreamingAssets)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, path + ".json");
            SaveJsonDataClass(filePath, jstring);
        }
        #endregion

        [Serializable]
        public class Wrapper<T>
        {
            public bool c2Array = true;
            public int[] size = new int[3];
            public T[,,] data;
        }
    }
}