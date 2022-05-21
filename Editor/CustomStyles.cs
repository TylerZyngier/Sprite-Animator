using UnityEngine;
using UnityEditor;

namespace SpriteAnimator
{
    public static class CustomStyles
    {
        public static GUIStyle section01
        {
            get
            {
                var style = new GUIStyle();
                var marginSize = 5;
                style.margin = new RectOffset(marginSize, marginSize, marginSize, marginSize);

                return style;
            }
        }

        public static GUIStyle section02(int width = 0)
        {
            var style = new GUIStyle();
            var marginSize = 2;
            style.margin = new RectOffset(marginSize, marginSize, marginSize, marginSize);
            style.fixedWidth = width;

            return style;
        }

        public static GUIStyle header01
        {
            get
            {
                var style = new GUIStyle();

                var marginSize = 2;
                style.margin = new RectOffset(marginSize, marginSize, marginSize + 2, marginSize + 2);

                var paddingSize = 2;
                style.padding = new RectOffset(paddingSize, paddingSize, paddingSize, paddingSize);

                var borderSize = 2;
                style.border = new RectOffset(borderSize, borderSize, borderSize, borderSize);

                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = EditorStyles.boldLabel.normal.textColor;

                style.fontSize = 14;

                return style;
            }
        }

        public static GUIStyle header02
        {
            get
            {
                var style = new GUIStyle();

                var marginSize = 2;
                style.margin = new RectOffset(marginSize, marginSize, marginSize, marginSize);

                var paddingSize = 2;
                style.padding = new RectOffset(paddingSize, paddingSize, paddingSize, paddingSize);

                var borderSize = 2;
                style.border = new RectOffset(borderSize, borderSize, borderSize, borderSize);

                style.fontStyle = FontStyle.Bold;
                style.normal.textColor = EditorStyles.boldLabel.normal.textColor;

                return style;
            }
        }

        public static GUIStyle frameLabel
        {
            get
            {
                var style = new GUIStyle();
                var boldLabel = GUI.skin.GetStyle("boldLabel");

                style = boldLabel;
                style.alignment = TextAnchor.MiddleCenter;

                return style;
            }
        }
    }
}