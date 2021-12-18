using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.DAO
{
    public enum SkillType
    {
        Damage = 0,
        SelfDefense = 1,
        Heal = 2,
    }

    [CreateAssetMenu]
    public class SkillInfo : ScriptableObject
    {
        public Sprite Sprite;
        public AnimationType AnimationType;

        public SkillType SkillType;

        public List<StatusInfoBase> Status;

        [Header("Damage")]
        public int DamageValue;
        public int AoeMultiplier;

        [Header("Defense")]
        public int Defense = 5;

        [Header("Heal")]
        public int Heal = 20;

        [Header("Stun")]
        public int IsStun;
    }
}
