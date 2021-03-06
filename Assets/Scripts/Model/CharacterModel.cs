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

        public Status GetStatus(string tag)
        {
            return StatusList.FirstOrDefault(_ => _.Tag == tag);
        }

        public event Action<List<Status>> OnStatusChange;
        public void AddStatus(Status status)
        {
            this.StatusList.Add(status);
            this.OnStatusChange?.Invoke(StatusList);
        }

        public void RemoveStatus()
        {
            this.StatusList.RemoveAll(_ => _.Duration == 0);
            this.OnStatusChange?.Invoke(StatusList);
        }

        public Skill SelectedSkill { get; set; }

        public List<Skill> Skills => this._skills;

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

        public event Action<CharacterModel> OnDeath;
        public event Action<int> OnHealthChange;
        public int CurrentHealth
        {
            get => this._currentHealth;
            set
            {
                int delta = value - this._currentHealth;
                this._currentHealth = value;
                if (this._currentHealth < 0)
                    this._currentHealth = 0;

                this.OnHealthChange?.Invoke(delta);

                if(this._currentHealth <= 0)
                    this.OnDeath?.Invoke(this);
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
