using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace SpriteAnimation
{
    public class AssetHandler
    {
        [OnOpenAsset()]
        public static bool OpenEditor(int instanceId, int line)
        {
            SpriteAnimationClip clip = EditorUtility.InstanceIDToObject(instanceId) as SpriteAnimationClip;

            if (clip != null)
            {
                SpriteAnimationEditorWindow.Open(clip);
                return true;
            }

            return false;
        }
    }

    [CustomEditor(typeof(SpriteAnimationClip))]
    public class SpriteAnimationCustomEditor : Editor
    {
        private bool foldoutExpanded;
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open Animation Editor"))
            {
                SpriteAnimationEditorWindow.Open((SpriteAnimationClip)target);
            }
        }
    }
}
