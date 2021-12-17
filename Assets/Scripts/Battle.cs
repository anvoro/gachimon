using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.DAO;
using Assets.Scripts.Model;
using RSG;
using UnityEngine;
using CharacterInfo = Assets.Scripts.DAO.CharacterInfo;

namespace Assets.Scripts
{
    public class Battle : MonoBehaviour
    {
        public static readonly IPromiseTimer PromiseTimer = new PromiseTimer();
        private void Update()
        {
            PromiseTimer.Update(Time.deltaTime);
        }

        public static IPromise WaitWithDelay(float duration)
        {
            return PromiseTimer.WaitFor(duration).Then(() => PromiseTimer.WaitFor(.2f));
        }

        private readonly List<CharacterView> _charactersInBattle = new List<CharacterView>();
        
        private readonly Queue<CharacterView> _characterActionQueue = new Queue<CharacterView>();

        private CharacterView _currentCharacterView;

        [SerializeField] private List<CharacterInfo> _gachi;
        [SerializeField] private List<CharacterInfo> _enemies;

        [SerializeField] private Transform[] _partySpawnPoints;
        [SerializeField] private Transform[] _enemySpawnPoints;

        [Header("UI")]
        [SerializeField]
        private Camera _combatCamera;
        [SerializeField]
        private RectTransform _cameraRectTransform;

        private void Start()
        {
            SetupBattle(this._gachi);
        }

        private void SetupBattle(List<CharacterModel> party)
        {
            this._charactersInBattle.Clear();

            for (int i = 0; i < party.Count; i++)
            {
                CharacterView gachiMan = Instantiate(party[i], this._partySpawnPoints[i]);
                this._charactersInBattle.Add(gachiMan);
            }

            for (int i = 0; i < this._enemies.Count; i++)
            {
                 CharacterView enemy = Instantiate(this._enemies[i], this._enemySpawnPoints[i]);
                 this._charactersInBattle.Add(enemy);
            }

            foreach (CharacterView character in this._charactersInBattle)
            {
                character.Init(this._cameraRectTransform, this._combatCamera);

                character.OnCharacterSelected += с =>
                {
                    if(с == this._currentCharacterView)
                        return;

                    //todo запилить блокер ввода на время анимации
                    this._currentCharacterView.PlayAnimation(AnimationType.Attack)
                        .Done(() =>
                        {

                            if (this._characterActionQueue.Count > 0)
                            {
                                this.BeginTurn();
                            }
                            else
                            {
                                this.BeginRound();
                            }
                        });
                };
            }

            this.BeginRound();
        }

        private void BeginRound()
        {
            foreach (CharacterView c in this._charactersInBattle.OrderBy(_ => _.Initiative))
            {
                this._characterActionQueue.Enqueue(c);
            }

            this.BeginTurn();
        }

        private void BeginTurn()
        {
            if (this._currentCharacterView != null)
            {
                this._currentCharacterView.ActivateTurnMark = false;
            }

            this._currentCharacterView = this._characterActionQueue.Dequeue();

            this._currentCharacterView.ActivateTurnMark = true;
        }
    }
}
