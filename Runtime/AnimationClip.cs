using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteAnimator
{
    [CreateAssetMenu(fileName = "NewAnimationClip", menuName = "2D Animator/Animation Clip", order = 1)]
    public class AnimationClip : ScriptableObject
    {
        [SerializeField] private int framerate;
        public int GetFramerate() => framerate;
        public void SetFramerate(int value)
        {
            if (value < 1)
            {
                value = 1;
            }

            framerate = value;
        }

        public bool reverse;

        public int frameCount
        {
            get => frames.Count;
            set
            {
                if (value == frameCount) return;

                if (value < 1)
                {
                    value = 1;
                }

                int size = frames.Count;

                if (value < size)
                {
                    frames.RemoveRange(value, size - value);
                }
                else if (value > size)
                {
                    frames.AddRange(Enumerable.Repeat(default(Sprite), value - size));
                }
            }
        }



        [SerializeField] private int startingFrame = 1;
        public int GetStartingFrame() => startingFrame;
        public void SetStartingFrame(int value)
        {
            if (value == startingFrame) return;

            if (value > frameCount)
            {
                value = frameCount;
            }
            else if (value < 1)
            {
                value = 1;
            }

            startingFrame = value;
        }

        public List<Sprite> frames = new List<Sprite>();
    }
}