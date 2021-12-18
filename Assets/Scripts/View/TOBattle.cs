using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSG;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Assets.Scripts.View
{
    internal class TOBattle : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        public GameObject UI;

        public VideoPlayer VideoPlayer;

        public AudioSource AudioSource;

        public static readonly IPromiseTimer PromiseTimer = new PromiseTimer();
        private void Update()
        {
            PromiseTimer.Update(Time.deltaTime);
        }

        private void Start()
        {
            this.UI.SetActive(false);

            PromiseTimer.WaitFor((float)this.VideoPlayer.clip.length).Done(() =>
            {
                VideoPlayer.enabled = false;

                AudioSource.Play();
                this.UI.SetActive(true);
            });
        }

        private bool isInit;
        private void Awake()
        {
            if (this.isInit == false)
            {
                SceneManager.LoadScene(1, LoadSceneMode.Additive);
                this.isInit = true;
            }

            this._button.onClick.RemoveAllListeners();
            this._button.onClick.AddListener(() => this.GoToBattle());
        }

        private void GoToBattle()
        {
            Fader.instance.GoFade(() => SceneManager.LoadScene(2));
        }
    }
}
