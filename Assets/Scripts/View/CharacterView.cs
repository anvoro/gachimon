using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DAO;
using Assets.Scripts.Model;
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

        public void Init(CharacterModel model)
        {
            this.ActivateTurnMark = false;

            this._model = model;
        }

        public IPromise PlayAnimation(AnimationType animation)
        {
            switch (animation)
            {
                case AnimationType.Attack:
                    this._animator.SetTrigger("Attack");
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(animation), animation, null);
            }

            float length = this._animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

            return Battle.WaitWithDelay(length);
        }

        public void Init(RectTransform parent, Camera camera)
        {
            this._combatPanel = CharacterCombatPanel.Instantiate(this._healthBarTransform, parent, camera);
        }

        private void OnMouseDown()
        {
            this.OnCharacterSelected?.Invoke(this._model);
        }
    }
}
