using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DAO
{
    public enum SkillType
    {
        Damage = 0,
        SelfDefense = 1,
    }

    [CreateAssetMenu]
    public class SkillInfo : ScriptableObject
    {
        public Sprite Sprite;
        public AnimationType AnimationType;

        public SkillType SkillType;

        [Header("Damage")]
        public int DamageValue;
        public int AoeMultiplier;

        [Header("Defense")]
        public int Defense = 5;

        [Header("Stun")]
        public bool IsStun;
    }
}
