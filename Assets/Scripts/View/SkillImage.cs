using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class SkillImage : MonoBehaviour, IPointerDownHandler
    {
        private static event Action<SkillImage> OnSelected;

        [SerializeField] private Image _image;

        [SerializeField] private TMP_Text _cooldown;

        private Skill _skill;

        public event Action<Skill> OnClick;

        private void Awake()
        {
            OnSelected += item =>
            {
                this._image.color = item == this ? Color.white : Color.gray;
            };
        }

        private void OnDestroy()
        {
            OnSelected = null;
        }

        public void Clear()
        {
            this.gameObject.SetActive(false);
        }

        public void Init(Skill skill, bool isSelected)
        {
            this._image.sprite = skill.Sprite;

            this._image.color = isSelected ? Color.white : Color.gray;

            this.gameObject.SetActive(true);

            this._skill = skill;

            if (this._skill.CurrentCooldown > 0)
            {
                _cooldown.gameObject.SetActive(true);
                this._cooldown.text = this._skill.CurrentCooldown.ToString();
            }
            else
            {
                _cooldown.gameObject.SetActive(false);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(this._skill.CurrentCooldown > 0)
                return;

            OnSelected?.Invoke(this);

            this.OnClick?.Invoke(this._skill);
        }
    }
}
