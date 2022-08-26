using UnityEngine;
using System.IO;

public class CreateJson : MonoBehaviour
{


    [System.Serializable]
    public class NewJson
    {
        [HideInInspector] public bool c2Array = true;
        [HideInInspector] public int[] size = new int[3];
        public string[,,] data;

        public NewJson(string[] x, string[] y, string[] z)
        {
            //x.CopyTo(data,0);
            data = new string[,,] { { { "a", "b", "c" } } };
        }
    }

    public NewJson JSONfile = new NewJson();

    public void CreateFileJSON()
    {
        JSONfile.size[0] = x.Length;
        JSONfile.size[1] = y.Length;
        JSONfile.size[2] = z.Length;

        JSONfile.data = new string[JSONfile.size[0], JSONfile.size[1], JSONfile.size[2]];

        for (int xIndex = 0; xIndex < x.Length; xIndex++)
            JSONfile.data[xIndex, 0, 0] = x[xIndex];

        for (int yIndex = 0; yIndex < y.Length; yIndex++)
            JSONfile.data[0, yIndex, 0] = y[yIndex];

        for (int zIndex = 0; zIndex < z.Length; zIndex++)
            JSONfile.data[0, 0, zIndex] = z[zIndex];



        string stringOutPut = JsonUtility.ToJson(JSONfile);

        File.WriteAllText(Application.dataPath + "/newJsonFile.txt", stringOutPut);
    }
}
