using UnityEditor;
using UnityEngine;
using System.IO;

public class WorldObjectScriptCreator : EditorWindow
{
  [MenuItem("Assets/Create/WorldObject Script", false, 0)]
  private static void CreateCustomScript()
  {
    // Get the selected folder path
    string selectedFolderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
    if (string.IsNullOrEmpty(selectedFolderPath))
    {
      Debug.LogError("Please select a folder in the Project window where you want to create the script.");
      return;
    }

    // Get the name of the created file
    string filePath = selectedFolderPath + "/NewWorldObjectScript.cs";

    // Create a new C# script asset
    CreateScriptAsset(selectedFolderPath, filePath);

    Selection.activeObject = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(filePath);
    EditorGUIUtility.PingObject(Selection.activeObject);
  }

  private static void CreateScriptAsset(string folderPath, string filePath)
  {
    // Custom script template with the dynamic class name
    string scriptTemplate =
$@"
public class {Path.GetFileNameWithoutExtension(filePath)} : WorldObject
{{
  protected override void Start()
  {{
    // Your start logic goes here
  }}

  protected override void UpdateTick(object sender, OnTickEventArgs eventArgs)
  {{
    // Your update logic goes here
  }}
}}";


    // Write the template content to the file
    System.IO.File.WriteAllText(filePath, scriptTemplate);

    // Refresh the Asset Database to ensure that the changes are reflected in the Unity Editor
    AssetDatabase.Refresh();
  }
}
