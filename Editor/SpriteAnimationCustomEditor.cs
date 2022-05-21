using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace SpriteAnimator
{
    public class AssetHandler
    {
        [OnOpenAsset()]
        public static bool OpenEditor(int instanceId, int line)
        {
            AnimationClip clip = EditorUtility.InstanceIDToObject(instanceId) as AnimationClip;

            if (clip != null)
            {
                SpriteAnimationEditorWindow.Open(clip);
                return true;
            }

            return false;
        }
    }

    [CustomEditor(typeof(AnimationClip))]
    public class SpriteAnimationCustomEditor : Editor
    {
        private bool foldoutExpanded;
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open Animation Editor"))
            {
                SpriteAnimationEditorWindow.Open((AnimationClip)target);
            }
        }
    }
}