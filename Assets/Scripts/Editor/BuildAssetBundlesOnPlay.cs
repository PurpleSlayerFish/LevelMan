using UnityEditor;

namespace Editor
{
    [InitializeOnLoad]
    public class BuildAssetBundlesOnPlay
    {
        static BuildAssetBundlesOnPlay() => EditorApplication.playModeStateChanged += OnPlayModeStateChanged;

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
                AssetBundles.BuildAllAssetBundles();
        }
    }
}