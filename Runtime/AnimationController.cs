using UnityEngine;

namespace SpriteAnimator
{
    [CreateAssetMenu(fileName = "NewAnimationController", menuName = "2D Animator/Animation Controller", order = 0)]
    public class AnimationController : ScriptableObject
    {
        public AnimationClip[] animationClips;
    }
}