using UnityEditor;
using System.Diagnostics;

public class ScriptBatch 
{
    [MenuItem("CustomBuild/Windows Build With Postprocess")]
    public static void BuildGame ()
    {
        // Get filename.
        string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "Built", "");

        // Build player.
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, path + "/BuiltGame.exe", BuildTarget.StandaloneWindows, BuildOptions.None);

        FileUtil.DeleteFileOrDirectory(path + "/Songs");
        FileUtil.CopyFileOrDirectory("Songs", path + "/Songs");
        FileUtil.DeleteFileOrDirectory(path + "/Keybindings.txt");
        FileUtil.CopyFileOrDirectory("Keybindings.txt", path + "/Keybindings.txt");
    }
}
