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
using Random = UnityEngine.Random;

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
                case AnimationType.Attack1:
                    this._animator.SetTrigger("Attack1");
                    break;

                case AnimationType.Attack2:
                    this._animator.SetTrigger("Attack2");
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
            this._animator.SetFloat("Offset", Random.Range(0f, 4f));

            this.ActivateTurnMark = false;

            this._model = model;

            this._combatPanel = CharacterCombatPanel.Instantiate(this._healthBarTransform, parent, camera);
            this._combatPanel.SetHealthBar(this._model, true);

            this._model.OnHealthChange += value =>
            {
                this._combatPanel.SetHealthBar(this._model, false);

                if (value <= 0)
                {
                    this._animator.SetTrigger("Hurt");

                    HealthPopup.Instantiate(this._healthBarTransform.position + new Vector3(0, .5f, 0), value.ToString(), Color.red,
                        Color.black);
                }
                else
                {
                    HealthPopup.Instantiate(this._healthBarTransform.position + new Vector3(0, .5f, 0), value.ToString(), Color.green,
                        Color.black);
                }
            };

            this._model.OnActiveStateChange += value =>
            {
                this._animator.SetBool("Stunned", value < 0);
            };

            this._model.OnDeath += character =>
            {
                this._animator.SetTrigger("Death");

                Battle.WaitWithDelay(2f).Done(() => this._combatPanel.gameObject.SetActive(false));
            };
        }

        private void OnMouseDown()
        {
            this.OnCharacterSelected?.Invoke(this._model);
        }
    }
}
