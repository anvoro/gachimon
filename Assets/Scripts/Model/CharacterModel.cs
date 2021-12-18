using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.DAO;
using UnityEngine;
using CharacterInfo = Assets.Scripts.DAO.CharacterInfo;

namespace Assets.Scripts.Model
{
    public class CharacterModel
    {
        private int _maxHealth;

        private int _currentHealth;

        private int _initiative;

        private List<Skill> _skills;
        private int _isActive;

        public readonly List<Status> StatusList = new List<Status>();

        public Skill SelectedSkill { get; set; }

        public List<Skill> Skills => this._skills.ToList();

        public event Action<int> OnActiveStateChange;
        public int IsActive
        {
            get => this._isActive;
            set
            {
                this._isActive = value;
                this.OnActiveStateChange?.Invoke(this._isActive);
            }
        }

        public Side Side { get; }

        public int Initiative => this._initiative;

        public int Defense { get; set; }

        public int MaxHealth
        {
            get => this._maxHealth;
            set => this._maxHealth = value;
        }

        public event Action<int> OnHealthChange;
        public int CurrentHealth
        {
            get => this._currentHealth;
            set
            {
                int delta = this._currentHealth - value;
                this._currentHealth = value;
                this.OnHealthChange?.Invoke(delta);
            }
        }

        public CharacterView View { get; }

        public void Clear()
        {
            this.Defense = 0;

            if (this.IsActive < 0)
                this.IsActive++;
        }

        public CharacterModel(CharacterInfo info)
        {
            this.IsActive = 0;

            this.View = info.View;

            this._initiative = info.Initiative;
            this._maxHealth = info.MaxHealth;
            this.Side = info.Side;

            this._currentHealth = this._maxHealth;

            this._skills = info.Skills.Select(_ => new Skill(_)).ToList();

            this.SelectedSkill = this._skills[0];
        }

        public void Hurt(int damageValue)
        {
            this.CurrentHealth -= Mathf.Max(0, damageValue - this.Defense);
        }
    }
}
