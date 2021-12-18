﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DAO;
using Assets.Scripts.Model;
using Assets.Scripts.View;
using RSG;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class CharacterView : MonoBehaviour
    {
        private CharacterModel _model;

        private CharacterCombatPanel _combatPanel;

        [SerializeField] private Animator _animator;

        [SerializeField] private GameObject _turnMark;

        [SerializeField] private Transform _healthBarTransform;

        public bool ActivateTurnMark
        {
            set => this._turnMark.SetActive(value);
        }

        public event Action<CharacterModel> OnCharacterSelected;

        public IPromise PlayAnimation(AnimationType animation)
        {
            switch (animation)
            {
                case AnimationType.Attack:
                    this._animator.SetTrigger("Attack");
                    break;

                case AnimationType.Defense:
                    this._animator.SetTrigger("Defense");
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(animation), animation, null);
            }

            float length = this._animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

            return Battle.WaitWithDelay(length);
        }

        public void Init(CharacterModel model, RectTransform parent, Camera camera)
        {
            this.ActivateTurnMark = false;

            this._model = model;

            this._combatPanel = CharacterCombatPanel.Instantiate(this._healthBarTransform, parent, camera);
            this._combatPanel.SetHealthBar(this._model, true);

            this._model.OnHealthChange += value =>
            {
                this._combatPanel.SetHealthBar(this._model, false);
                HealthPopup.Instantiate(this._healthBarTransform.position + new Vector3(0, .5f, 0), value.ToString(), Color.red,
                    Color.black);
            };

            this._model.OnActiveStateChange += value =>
            {
                this._animator.SetBool("Stuned", value < 0);
            };
        }

        private void OnMouseDown()
        {
            this.OnCharacterSelected?.Invoke(this._model);
        }
    }
}
