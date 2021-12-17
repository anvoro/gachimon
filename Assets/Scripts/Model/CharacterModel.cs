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
        private int _maxHealth = 100;

        private int _currentHealth;

        private Side _side;

        private int _initiative;

        private List<Skill> _skills;

        public int Initiative => this._initiative;

        public int MaxHealth
        {
            get => this._maxHealth;
            set => this._maxHealth = value;
        }

        public int CurrentHealth
        {
            get => this._currentHealth;
            set => this._currentHealth = value;
        }

        public CharacterView View
        {
            get;
        }

        public CharacterModel(CharacterInfo info)
        {
            this.View = info.View;

            this._initiative = info.Initiative;
            this._maxHealth = info.MaxHealth;
            this._side = info.Side;

            this._currentHealth = this._maxHealth;
        }
    }
}
