using UnityEngine;
using UnityEditor;
using System.Text;
using UnityEditor.SceneManagement;
using System.IO;
using static Tea.Log.Print;

/// <summary>
/// editor, editorWindow, 
/// </summary>
public static class LocalEditorTool
{
    [MenuItem("GameObject/CopyPath")]
    static void CopyPrefabGOPath()
    {
        var go = Selection.activeGameObject;
        var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        if (go == null || prefabStage == null)
        {
            return;
        }

        string path = GetPathWithoutRoot(go.transform, prefabStage.prefabContentsRoot.transform);

        // 复制到系统剪切板
        GUIUtility.systemCopyBuffer = path;

        Debug.Log($"路径复制成功: {path}, {go.name}");
    }

    [MenuItem("GameObject/CopyPath", true)]
    static bool ValidateCopyPath()
    {
        if (Selection.activeGameObject == null) return false;

        var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        return prefabStage != null && prefabStage.IsPartOfPrefabContents(Selection.activeGameObject);
    }

    static string GetPathWithoutRoot(Transform target, Transform root)
    {
        if (target == null) return "";

        StringBuilder sb = new StringBuilder();
        var current = target;
        while (current != null && current != root)
        {
            if (sb.Length > 0)
            {
                sb.Insert(0, "/");
            }
            sb.Insert(0, current.name);
            current = current.parent;
        }
        return sb.ToString();
    }

    [MenuItem("Assets/SetFolderAB")]
    static void SetFolderAB()
    {
        var obj = Selection.activeObject;
        if (obj == null) return;

        var path = AssetDatabase.GetAssetPath(obj);
        var guids = AssetDatabase.FindAssets("t:Sprite", new[] { path });
        if (guids.Length == 0) return;

        var folderName = Path.GetFileNameWithoutExtension(path);
        var abName = (folderName + ".u3d").ToLower();
        int processedCount = 0;
        foreach (var guid in guids)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var assetImporter = AssetImporter.GetAtPath(assetPath);
            if (assetImporter != null)
            {
                assetImporter.assetBundleName = abName;
                assetImporter.SaveAndReimport();
                processedCount++;

                Debug.Log($"set assetbundle {abName} for {assetPath}");
            }
        }

        AssetDatabase.Refresh();

        string msg = $"Sucessfully processed {processedCount} asset! AssetBundle Name {abName} has been set";
        EditorUtility.DisplayDialog("Complete", msg, "OK");
        Debug.Log(msg);
    }

    [MenuItem("Assets/SetFolderAB", true)]
    static bool ValidateSetFolderAB()
    {
        return Selection.activeObject && AssetDatabase.IsValidFolder(AssetDatabase.GetAssetPath(Selection.activeObject));
    }

    /// <summary>
    /// 设置prefab的ab名和AssetBundleReference组件引用
    /// ab名:父文件夹名/prefab名.u3d
    /// ab名小写
    /// </summary>
    [MenuItem("Assets/SetPrefabAttribute")]
    static void SetPrefabAttribute()
    {
        var obj = Selection.activeObject;
        if (obj == null) return;

        var path = AssetDatabase.GetAssetPath(obj);
        var fileName = Path.GetFileNameWithoutExtension(path);
        var folderName = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(path));
        // print("path, filename, foldername: ", path, fileName, folderName, obj);
        var abName = $"{folderName}/{fileName}.u3d".ToLower();

        var assetImporter = AssetImporter.GetAtPath(path);
        assetImporter.assetBundleName = abName;
        assetImporter.SaveAndReimport();

        var go = obj as GameObject;
        var abRef = go.GetComponent<AssetBundleReference>();
        if (abRef == null)
        {
            abRef = go.AddComponent<AssetBundleReference>();
            abRef.assetBundleName = abName;
        } 

        AssetDatabase.Refresh();
    }

    [MenuItem("Assets/SetPrefabAttribute", true)]
    static bool ValidateSetPrefabAttribute()
    {
        // 确保是prefab
        var obj = Selection.activeObject;
        return obj != null && obj is GameObject && PrefabUtility.IsPartOfAnyPrefab(obj);
    }
}
