﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.View
{
    internal class Fader : MonoBehaviour
    {
        public static Fader instance = null; // Экземпляр объекта

        public CanvasGroup CanvasGroup;

        // Метод, выполняемый при старте игры
        void Start()
        {
            // Теперь, проверяем существование экземпляра
            if (instance == null)
            { // Экземпляр менеджера был найден
                instance = this; // Задаем ссылку на экземпляр объекта
            }
            else if (instance == this)
            { // Экземпляр объекта уже существует на сцене
                Destroy(gameObject); // Удаляем объект
            }

            // Теперь нам нужно указать, чтобы объект не уничтожался
            // при переходе на другую сцену игры
            DontDestroyOnLoad(gameObject);

            // И запускаем собственно инициализатор
            InitializeManager();
        }

        // Метод инициализации менеджера
        private void InitializeManager()
        {
            CanvasGroup.alpha = 0;

            this.CanvasGroup.gameObject.SetActive(false);
        }

        public void GoFade(Action callback)
        {
            StartCoroutine(this.Fade(callback));
        }

        private IEnumerator Fade(Action callback)
        {
            this.CanvasGroup.gameObject.SetActive(true);

            float elapsedTime = 0f;

            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime;
                CanvasGroup.alpha = elapsedTime;
                yield return null;
            }

            callback.Invoke();

            elapsedTime = 0f;

            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime;
                CanvasGroup.alpha = 1 - elapsedTime;
                yield return null;
            }

            CanvasGroup.alpha = 0;

            this.CanvasGroup.gameObject.SetActive(false);
        }
    }
}
