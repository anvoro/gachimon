using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class SkillImage : MonoBehaviour, IPointerDownHandler
    {
        private static event Action<SkillImage> OnSelected;

        [SerializeField] private GameObject _border;

        [SerializeField] private Image _image;

        private Skill _skill;

        public event Action<Skill> OnClick;

        private void Awake()
        {
            OnSelected += item =>
            {
                this._border.SetActive(item == this);
            };
        }

        public void Clear()
        {
            this.gameObject.SetActive(false);
        }

        public void Init(Skill skill, bool isSelected)
        {
            this._image.sprite = skill.Sprite;

            this._border.SetActive(isSelected);

            this.gameObject.SetActive(true);

            this._skill = skill;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnSelected?.Invoke(this);

            this.OnClick?.Invoke(this._skill);
        }
    }
}
