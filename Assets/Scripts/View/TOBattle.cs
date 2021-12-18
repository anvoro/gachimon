using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    internal class TOBattle : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        private void Awake()
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);

            this._button.onClick.AddListener(() => this.GoToBattle());
        }

        private void GoToBattle()
        {
            Fader.instance.GoFade(() => SceneManager.LoadScene(2));
        }
    }
}
