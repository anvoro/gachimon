using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DAO;
using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts
{
    public class Skill
    {
        public Sprite Sprite { get; private set; }

        public AnimationType AnimationType { get; private set; }

        private SkillType _skillType;

        private int _damageValue;
        private int _aoeMultiplier;

        private int _defense;

        private bool _isStun;

        public Skill(SkillInfo info)
        {
            this.Sprite = info.Sprite;
            this.AnimationType = info.AnimationType;

            this._skillType = info.SkillType;
            this._damageValue = info.DamageValue;
            this._aoeMultiplier = info.AoeMultiplier;

            this._defense = info.Defense;

            this._isStun = info.IsStun;
        }

        public bool IsAvailable(CharacterModel caster, CharacterModel target)
        {
            switch (this._skillType)
            {
                case SkillType.Damage:
                    return caster.Side != target.Side;

                case SkillType.SelfDefense:
                    return caster.Side == target.Side;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Cast(CharacterModel caster, CharacterModel target)
        {
            switch (this._skillType)
            {
                case SkillType.Damage:
                    target.CurrentHealth -= Mathf.Max(0, this._damageValue - target.Defense);
                    break;

                case SkillType.SelfDefense:
                    caster.Defense += caster.Defense;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (this._isStun == true)
                target.IsActive = false;
        }
    }
}
