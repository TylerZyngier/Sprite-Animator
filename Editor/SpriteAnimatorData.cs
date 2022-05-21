using UnityEngine;

namespace SpriteAnimator
{
    [CreateAssetMenu(fileName = "NewSpriteAnimatorData", menuName = "2D Animator/SpriteAnimatorData", order = 2)]
    public class SpriteAnimatorData : ScriptableObject
    {
        public AnimationClip selectedAnimation;
    }
}