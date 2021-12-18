using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class HealthPopup : MonoBehaviour
    {
        private const string PopupPrefabPath = "HealthPopup";

        private static GameObject healthPopupPrefab;

        private static GameObject HealthPopupPrefab => healthPopupPrefab ?? (healthPopupPrefab = Resources.Load(PopupPrefabPath) as GameObject);

        private static RectTransform parent;

        private static RectTransform Parent => parent ?? (parent = GameObject.Find("Canvas").GetComponent<RectTransform>());

        public static void Clear()
        {
            parent = null;
        }

        public Sprite[] frazi;
        public Image frazaIcon;

        [SerializeField]
        private float _speed = 1f;

        [SerializeField]
        private TMP_Text _text;

        public string Text
        {
            set { this._text.text = value; }
        }

        private Color OutlineColor
        {
            set { this._text.outlineColor = value; }
            get { return this._text.outlineColor; }
        }

        private Color Color
        {
            set { this._text.color = value; }
            get { return this._text.color; }
        }

        public static HealthPopup Instantiate(Vector3 position, string message, Color color, Color outlineColor/*, int sortOrder*/)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(position);

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(Parent, screenPoint, null, out Vector2 localPoint))
            {
                HealthPopup result = GameObject.Instantiate(HealthPopupPrefab).GetComponent<HealthPopup>();
                result.transform.SetParent(Parent, false);
                result.transform.localPosition = localPoint;

                result.Text = message;
                result.Color = color;
                result.OutlineColor = outlineColor;

                result.frazaIcon.sprite = result.frazi[Random.Range(0, result.frazi.Length)];

                //healthPopup.GetComponentInChildren<Canvas>().sortingOrder = sortOrder;

                GameObject.Destroy(result.gameObject, 2f);

                return result;
            }

            return null;
        }

        //public void SetToAnimationLayer()
        //{
        //    this.gameObject.transform.localScale = new Vector3(2.8F, 2.8F, 1);

        //    this.gameObject.SetLayer(CombatController.ANIMATION_LAYER);
        //    var canvas = this.GetComponentInChildren<Canvas>();

        //    canvas.sortingLayerName = CombatController.ANIMATION_LAYER;
        //    canvas.sortingOrder += 1000;
        //}

        private void Update()
        {
            this.transform.Translate(Vector3.up * this._speed * 100f * Time.deltaTime);
        }
    }
}
