using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.DAO;
using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts
{
    public class Skill
    {
        public AudioClip AudioClip;

        public Sprite Sprite { get; private set; }

        public AnimationType AnimationType { get; private set; }

        private SkillType _skillType;

        private int _damageValue;

        private int _defense;

        private int _heal;

        private int _isStun;

        private List<StatusInfoBase> _status;

        private int _cooldown;

        public int CurrentCooldown { get; set; }

        public Skill(SkillInfo info)
        {
            this.AudioClip = info.AudioClip;

            this.Sprite = info.Sprite;
            this.AnimationType = info.AnimationType;

            this._skillType = info.SkillType;
            this._damageValue = info.DamageValue;

            this._defense = info.Defense;

            this._heal = info.Heal;

            this._isStun = info.IsStun;

            if(info.Cooldown > 0)
                this._cooldown = info.Cooldown + 1;
            else
            {
                this._cooldown = 0;
            }

            this._status = info.Status.ToList();
        }

        public bool IsAvailable(CharacterModel caster, CharacterModel target)
        {
            switch (this._skillType)
            {
                case SkillType.AOEDamage:
                case SkillType.Damage:
                    return caster.Side != target.Side;

                case SkillType.Heal:
                case SkillType.SelfDefense:
                    return caster.Side == target.Side;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Cast(CharacterModel caster, CharacterModel target, List<CharacterModel> targets)
        {
            this.CurrentCooldown = this._cooldown;

            foreach (StatusInfoBase statusInfo in this._status)
            {
                Status existingStatus = target.GetStatus(statusInfo.name);
                if (existingStatus == null)
                {
                    target.AddStatus(new Status(statusInfo));
                }
                else
                {
                    existingStatus.Duration = statusInfo.Duration;
                }
            }

            switch (this._skillType)
            {
                case SkillType.Damage:
                    target.Hurt(this._damageValue);
                    break;

                case SkillType.AOEDamage:
                    foreach (CharacterModel t in targets)
                    {
                        t.Hurt(this._damageValue);
                    }
                    break;

                case SkillType.SelfDefense:
                    caster.Defense += this._defense;
                    break;

                case SkillType.Heal:
                    target.CurrentHealth += this._heal;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if(this._isStun > 0)
                target.IsActive -= (this._isStun + 1);
        }
    }
}
