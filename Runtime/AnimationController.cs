using UnityEngine;

namespace SpriteAnimation
{
    [CreateAssetMenu(fileName = "NewAnimationController", menuName = "Sprite Animator/Animation Controller (Currently not used)", order = 0)]
    public class AnimationController : ScriptableObject
    {
        public SpriteAnimationClip[] animationClips;
    }
}
