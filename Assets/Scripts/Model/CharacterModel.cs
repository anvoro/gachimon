using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DAO;
using UnityEngine;
using CharacterInfo = Assets.Scripts.DAO.CharacterInfo;

namespace Assets.Scripts.Model
{
    public class CharacterModel
    {
        private int _maxHealth;

        private int _currentHealth;

        public Side Side { get; private set; }

        private int _initiative;

        private List<Skill> _skills;

        public Skill SelectedSkill { get; set; }

        public List<Skill> Skills => this._skills.ToList();

        public bool IsActive { get; set; }

        public int Initiative => this._initiative;

        public int Defense { get; set; }

        public int MaxHealth
        {
            get => this._maxHealth;
            set => this._maxHealth = value;
        }

        public event Action OnHealthChange;
        public int CurrentHealth
        {
            get => this._currentHealth;
            set
            {
                this._currentHealth = value;
                this.OnHealthChange?.Invoke();
            }
        }

        public CharacterView View { get; }

        public void Clear()
        {
            this.Defense = 0;
            this.IsActive = true;
        }

        public CharacterModel(CharacterInfo info)
        {
            this.IsActive = true;

            this.View = info.View;

            this._initiative = info.Initiative;
            this._maxHealth = info.MaxHealth;
            this.Side = info.Side;

            this._currentHealth = this._maxHealth;

            this._skills = info.Skills.Select(_ => new Skill(_)).ToList();

            this.SelectedSkill = this._skills[0];
        }
    }
}
