using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    internal class CUMMINGSOON : MonoBehaviour
    {
        public Button Button;

        private void Awake()
        {
            Button.onClick.AddListener(() => Fader.instance.ShowMessage("CUMING SOON", null));
        }
    }
}
