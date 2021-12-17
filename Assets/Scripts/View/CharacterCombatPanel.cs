﻿using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CharacterCombatPanel : MonoBehaviour
    {
        private static GameObject prefab;
        
        private static GameObject Prefab => prefab ?? (prefab = Resources.Load("CharacterCombatPanel") as GameObject);

        [SerializeField]
        private Slider _healthBar;

        public void SetHealthBar(CharacterModel character, bool isSetMax)
        {
            int fullHealth = character.MaxHealth;
            float hpPart = (float)character.CurrentHealth / fullHealth;

            this._healthBar.value = isSetMax ? fullHealth : hpPart;
        }

        public static CharacterCombatPanel Instantiate(Transform position, RectTransform parent, Camera camera)
        {
            Vector2 screenPoint = camera.WorldToScreenPoint(position.position);

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, screenPoint, null, out Vector2 localPoint))
            {
                GameObject result = GameObject.Instantiate(Prefab);
                result.transform.SetParent(parent, false);
                result.transform.localPosition = localPoint;

                return result.GetComponent<CharacterCombatPanel>();
            }

            return null;
        }
    }
}