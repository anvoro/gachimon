using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public enum SkillType
    {
        Damage = 0,
    }

    public class Skill : MonoBehaviour
    {
        [SerializeField] private SkillType _skillType;

        [SerializeField] private int _damageValue;
        [SerializeField] private int _aoeMultiplier;

        public void Cast()
        {
            switch (this._skillType)
            {
                case SkillType.Damage:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
