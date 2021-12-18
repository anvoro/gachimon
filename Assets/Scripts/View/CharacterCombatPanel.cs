using System.Collections.Generic;
using Assets.Scripts.Model;
using TMPro;
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

        [SerializeField] private Image[] _statusList;

        [SerializeField] private TMP_Text _hp;

        private void Start()
        {
            foreach (Image image in this._statusList)
            {
                image.gameObject.SetActive(false);
            }
        }

        public void SetStatusList(List<Status> statusList)
        {
            foreach (Image image in this._statusList)
            {
                image.gameObject.SetActive(false);
            }

            for (int i = 0; i < statusList.Count; i++)
            {
                Status status = statusList[i];

                this._statusList[i].gameObject.SetActive(true);
                this._statusList[i].sprite = status.Sprite;
            }
        }

        public void SetHealthBar(CharacterModel character, bool isSetMax)
        {
            if (this._hp != null)
                this._hp.text = character.CurrentHealth.ToString();

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
