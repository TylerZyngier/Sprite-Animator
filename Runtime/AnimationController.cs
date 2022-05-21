using UnityEngine;

namespace SpriteAnimator
{
    [CreateAssetMenu(fileName = "NewAnimationController", menuName = "Sprite Animator/Animation Controller (Currently not used)", order = 0)]
    public class AnimationController : ScriptableObject
    {
        public AnimationClip[] animationClips;
    }
}