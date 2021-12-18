using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.View
{
    internal class BOYSITE : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Application.OpenURL("https://app.slack.com/client/T04HQTSBB/GK85S4736");
        }
    }
}
