using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class PrefabPathPrinter
{
    private const string MenuPath = "GameObject/Copy Prefab Path";

    // Validate the menu item to only appear when a GameObject is selected within Prefab Mode
    [MenuItem(MenuPath, true)]
    private static bool ValidatePrintPrefabPath()
    {
        // Only enable the menu item if a GameObject is selected
        if (Selection.activeGameObject == null)
            return false;

        // Check if we are currently in Prefab Isolation Mode (editing a prefab in a separate scene)
        PrefabStage currentPrefabStage = PrefabStageUtility.GetCurrentPrefabStage();

        // If in Prefab Mode and the selected GameObject belongs to the current prefab stage, enable the menu item
        return currentPrefabStage != null && currentPrefabStage.IsPartOfPrefabContents(Selection.activeGameObject); // Check if the selected object is part of the current prefab being edited.
    }

    // Add the menu item
    [MenuItem(MenuPath)]
    private static void PrintPrefabPath()
    {
        GameObject selectedGameObject = Selection.activeGameObject;
        if (selectedGameObject == null)
        {
            Debug.LogError("No GameObject selected.");
            return;
        }

        // Get the current PrefabStage to access the prefab root
        PrefabStage currentPrefabStage = PrefabStageUtility.GetCurrentPrefabStage();
        if (currentPrefabStage == null)
        {
            Debug.LogError("Not in Prefab Mode.");
            return;
        }

        GameObject prefabRoot = currentPrefabStage.prefabContentsRoot;

        // Construct the path from the prefab root to the selected GameObject
        string noRootPath;
        string path = GetPathFromRoot(prefabRoot, selectedGameObject.transform, out noRootPath);
        EditorGUIUtility.systemCopyBuffer = noRootPath; // Copy to system clipboard
        Debug.Log($"Copy path without prefab root success!\nPath from Prefab Root: {path}, No Prefab path: {noRootPath}", selectedGameObject); // Prints the path to the console, highlighting the GameObject.
    }

    private static string GetPathFromRoot(GameObject root, Transform targetTransform, out string noRootPath)
    {
        noRootPath = string.Empty;
        if (targetTransform == null)
        {
            return string.Empty;
        }

        if (targetTransform.gameObject == root)
        {
            return root.name; // If the target is the root, return its name.
        }

        string path = targetTransform.name;
        Transform currentParent = targetTransform.parent;

        // Iterate up the hierarchy until the root is reached or no parent is found
        while (currentParent != null && currentParent.gameObject != root)
        {
            path = currentParent.name + "/" + path;
            currentParent = currentParent.parent;
        }

        noRootPath = path;
        // Add the root's name to the beginning of the path.
        if (currentParent != null && currentParent.gameObject == root)
        {
            path = root.name + "/" + path;
        }

        return path;
    }
}
