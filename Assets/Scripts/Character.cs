using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSG;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public enum Side
    {
        Player = 0,
        Enemy = 1,
    }

    public enum AnimationType
    {
        Attack = 1,
    }

    public class Character : MonoBehaviour
    {
        private CharacterCombatPanel _combatPanel;

        [SerializeField] private int _maxHealth = 100;

        private int _currentHealth;

        [SerializeField] private Side _side;

        [SerializeField] private int _initiative;

        [SerializeField] private List<Skill> _skills;

        [SerializeField] private Animator _animator;

        [SerializeField] private GameObject _turnMark;

        [SerializeField] private Transform _healthBarTransform;

        public bool ActivateTurnMark
        {
            set => this._turnMark.SetActive(value);
        }

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

        public event Action<Character> OnCharacterSelected;

        private IPromiseTimer promiseTimer = new PromiseTimer();
        private void Update()
        {
            promiseTimer.Update(Time.deltaTime);
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
            Debug.Log($"Anim length: {length}");

            return promiseTimer.WaitFor(length);
        }

        public void Init(RectTransform parent, Camera camera)
        {
            this._combatPanel = CharacterCombatPanel.Instantiate(this._healthBarTransform, parent, camera);
        }

        private void Awake()
        {
            this.ActivateTurnMark = false;
            this._currentHealth = this._maxHealth;
        }

        private void OnMouseDown()
        {
            this.OnCharacterSelected?.Invoke(this);
        }
    }
}
