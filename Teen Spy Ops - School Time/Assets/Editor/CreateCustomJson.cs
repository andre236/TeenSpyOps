#if (UNITY_EDITOR)
using UnityEngine;
using UnityEditor;

public class CreateCustomJson : EditorWindow
{
    private string _fileName;
    private int _width;
    private int _height;
    private string[] _datasX;
    private string[] _datasY;
    private int _depth;
    private bool groupEnabled;
    private bool myBool;
    private float myFloat;


    // Add menu item named "My Window" to the Window menu
    [MenuItem("Tools/New JSON")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(CreateCustomJson));
    }

    void OnGUI()
    {

        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        _fileName = EditorGUILayout.TextField("File Name", _fileName);

        EditorGUILayout.BeginHorizontal();
        _width = EditorGUILayout.IntField("Width", Mathf.Clamp(_width, 0, 50), GUILayout.MaxWidth(200));
        _height = EditorGUILayout.IntField("Height", Mathf.Clamp(_height, 0, 50), GUILayout.MaxWidth(200));
        _depth = EditorGUILayout.IntField("Depth", Mathf.Clamp(_depth, 0, 50), GUILayout.MaxWidth(200));
        EditorGUILayout.EndHorizontal();

        _datasX = new string[_width];
        _datasY = new string[_height];

        EditorGUILayout.BeginHorizontal();
        for (int i = 0; i < _width; i++)
        {
            _datasX[i] = EditorGUILayout.TextField("x" + i + ": ", _datasX[i]);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginVertical();

        for (int j = 0; j < _height; j++)
        {
            if (j % 3 == 0)
            {
                EditorGUILayout.BeginVertical();
                _datasY[j] = EditorGUILayout.TextField("y" + j, _datasY[j], GUILayout.MaxWidth(500));
                EditorGUILayout.EndVertical();
            }
            else
            {
                _datasY[j] = EditorGUILayout.TextField("y" + j, _datasY[j], GUILayout.MaxWidth(500));

            }
        }
        EditorGUILayout.EndVertical();
    }
}
#endif