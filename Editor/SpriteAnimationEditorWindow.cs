using UnityEngine;
using UnityEditor;

namespace SpriteAnimation
{
    public class SpriteAnimationEditorWindow : EditorWindow
    {
        private static SpriteAnimationClip selectedAnimation;
        private static Vector2 scrollPos;

        [MenuItem("Window/Animation/Sprite Animation")]
        public static void Open()
        {
            SpriteAnimationEditorWindow window = GetWindow<SpriteAnimationEditorWindow>("Sprite Animation");
            window.minSize = new Vector2(500, 300);
        }

        public static void Open(SpriteAnimationClip animationObj)
        {
            Open();
            selectedAnimation = animationObj;
        }

        private void OnSelectionChange()
        {
            if (Selection.activeObject is SpriteAnimationClip)
            {
                selectedAnimation = Selection.activeObject as SpriteAnimationClip;
                Repaint();
            }
        }

        private void OnGUI()
        {
            if (selectedAnimation == null && Selection.activeObject is SpriteAnimationClip)
            {
                selectedAnimation = Selection.activeObject as SpriteAnimationClip;
            }

            EditorGUILayout.BeginHorizontal(CustomStyles.section01);
            DrawSettings();
            DrawFrames();
            EditorGUILayout.EndHorizontal();

            if (selectedAnimation == null) return;

            EditorUtility.SetDirty(selectedAnimation);
        }

        private void DrawAnimationLabel() => GUILayout.Label("Animation: " + (selectedAnimation == null ? "None" : selectedAnimation.name), CustomStyles.header01);

        private void DrawSettings()
        {
            EditorGUILayout.BeginVertical(CustomStyles.section02(width: 200));

            GUILayout.Label("Settings", CustomStyles.header02);

            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.ExpandHeight(true));

            if (selectedAnimation != null)
            {
                var framerateTooltip = "Framerate refers to the amount of frames (sprites) that are displayed within a second";
                selectedAnimation.SetFramerate(EditorGUILayout.IntField(new GUIContent("Framerate", framerateTooltip), selectedAnimation.GetFramerate()));

                var startFrameTooltip = "Starting frame is overridden if the 'entryFrame' parameter is specified when calling the 'Play' method on an Animator Controller";
                selectedAnimation.SetStartingFrame(EditorGUILayout.IntField(new GUIContent("Starting Frame", startFrameTooltip), selectedAnimation.GetStartingFrame()));

                selectedAnimation.reverse = EditorGUILayout.Toggle("Reverse Animation", selectedAnimation.reverse);
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();
        }

        private void DrawFrames()
        {
            EditorGUILayout.BeginVertical(CustomStyles.section02());

            DrawAnimationLabel();
            DrawFrameHeader();
            DrawFrameBoard();

            static void DrawFrameHeader()
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Frames", CustomStyles.header02);
                if (selectedAnimation != null)
                {
                    if (GUILayout.Button("-", GUILayout.Width(20f)))
                    {
                        selectedAnimation.frameCount--;
                    }

                    selectedAnimation.frameCount = EditorGUILayout.IntField(selectedAnimation.frameCount, CustomStyles.frameLabel, GUILayout.Width(60f));

                    if (GUILayout.Button("+", GUILayout.Width(20f)))
                    {
                        selectedAnimation.frameCount++;
                    }

                    if (GUILayout.Button("Delete empty frames", GUILayout.Width(150f)))
                    {
                        for (int i = selectedAnimation.frames.Count - 1; i >= 0; i--)
                        {
                            Sprite item = selectedAnimation.frames[i];

                            if (item != null) continue;

                            selectedAnimation.frames.Remove(item);
                        }
                    }
                }
                GUILayout.EndHorizontal();
            }

            static void DrawFrameBoard()
            {
                var section = EditorGUILayout.BeginHorizontal(EditorStyles.helpBox, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));

                if (selectedAnimation != null)
                {
                    var columnCount = Mathf.FloorToInt((Screen.width - 300) / 64);

                    scrollPos = EditorGUILayout.BeginScrollView(scrollPos, false, false);
                    EditorGUILayout.BeginVertical();
                    EditorGUILayout.BeginHorizontal();

                    var columnIndex = 0;

                    for (int i = 0; i < selectedAnimation.frames.Count; i++)
                    {
                        if (columnIndex > columnCount)
                        {
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();
                            columnIndex = 0;
                        }

                        columnIndex++;

                        EditorGUILayout.BeginVertical(GUILayout.Width(60));

                        GUILayout.Label((i + 1).ToString(), CustomStyles.frameLabel);

                        Sprite frame = selectedAnimation.frames[i];
                        selectedAnimation.frames[i] = EditorGUILayout.ObjectField(frame, typeof(Sprite), false, GUILayout.Width(60), GUILayout.Height(60)) as Sprite;

                        EditorGUILayout.EndVertical();
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndScrollView();
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();

                if (selectedAnimation == null) return;

                Event evt = Event.current;

                switch (evt.type)
                {
                    case EventType.DragUpdated:
                    case EventType.DragPerform:
                        if (!section.Contains(evt.mousePosition))
                            return;

                        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                        if (evt.type == EventType.DragPerform)
                        {
                            DragAndDrop.AcceptDrag();

                            foreach (Object dragged_object in DragAndDrop.objectReferences)
                            {
                                string texturePath = AssetDatabase.GetAssetPath(dragged_object);
                                Object[] assets = AssetDatabase.LoadAllAssetsAtPath(texturePath);

                                foreach (Object asset in assets)
                                {
                                    if (asset.GetType() != typeof(Sprite)) continue;
                                    selectedAnimation.frames.Add(asset as Sprite);
                                }
                            }
                        }
                        break;
                }
            }
        }

        private void VerticalLine(int width = 1)
        {
            Rect rect = EditorGUILayout.GetControlRect(false, GUILayout.Width(1));

            rect.width = width;
            rect.height = Screen.height;

            EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
        }
    }
}
