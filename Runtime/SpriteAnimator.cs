using UnityEngine;

namespace SpriteAnimation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        //public AnimationController animationController;

        public SpriteAnimationClip currentAnimation { get; private set; }
        private float frameTimer;

        private int currentFrameIndex;
        private int frameIndex
        {
            get => currentFrameIndex;
            set
            {
                if (value == currentFrameIndex) return;

                if (value > currentAnimation.frameCount - 1)
                {
                    value = 0;
                }
                else if (value < 0)
                {
                    value = currentAnimation.frameCount - 1;
                }

                currentFrameIndex = value;
            }
        }

        private float frameTime => 1f / currentAnimation.GetFramerate();

        private void Update() => HandleAnimations();

        private void HandleAnimations()
        {
            // if (animationController != null && currentAnimation == null)
            // {
            //     Play(animationController.animationClips[0]);
            // }

            if (currentAnimation == null) return;

            frameTimer += Time.deltaTime;

            if (frameTimer >= frameTime)
            {
                frameIndex = currentAnimation.reverse ? frameIndex - 1 : frameIndex + 1;
                frameTimer = 0;
            }

            spriteRenderer.sprite = currentAnimation.frames[frameIndex];
        }

        public void Play(SpriteAnimationClip nextAnimation)
        {
            if (currentAnimation == nextAnimation) return;

            currentAnimation = nextAnimation;
            frameIndex = currentAnimation.GetStartingFrame() - 1;
            frameTimer = 0;
        }

        /// <Description> 
        /// Old play mathod
        /// Makes use of animator controller
        /// May reimpliment animator controller
        /// </Description>
        // public void Play(string animationName, int entryFrame = 0)
        // {
        //     if (currentAnimation.name == animationName) return;

        //     SpriteAnimationClip newAnimation = null;

        //     foreach (SpriteAnimationClip clip in animationController.animationClips)
        //     {
        //         if (clip.name != animationName) continue;

        //         newAnimation = clip;
        //         break;
        //     }

        //     if (newAnimation == null)
        //     {
        //         Debug.Log("No animation with the name '" + animationName + "' has been found, maybe you made a typo?");
        //     }
        //     else
        //     {
        //         currentAnimation = newAnimation;
        //         frameIndex = entryFrame == 0 ? currentAnimation.startingFrame : entryFrame;
        //     }
        // }
    }
}
