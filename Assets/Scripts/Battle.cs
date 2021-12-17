using System;
using System.Collections.Generic;
using System.Linq;
using RSG;
using UnityEngine;

namespace Assets.Scripts
{
    public class Battle : MonoBehaviour
    {
        private readonly List<Character> _charactersInBattle = new List<Character>();
        
        private readonly Queue<Character> _characterActionQueue = new Queue<Character>();

        private Character _currentCharacter;

        [SerializeField] private List<Character> _gachi;
        [SerializeField] private List<Character> _enemies;

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

        private void SetupBattle(List<Character> party)
        {
            this._charactersInBattle.Clear();

            for (int i = 0; i < party.Count; i++)
            {
                Character gachiMan = Instantiate(party[i], this._partySpawnPoints[i]);
                this._charactersInBattle.Add(gachiMan);
            }

            for (int i = 0; i < this._enemies.Count; i++)
            {
                 Character enemy = Instantiate(this._enemies[i], this._enemySpawnPoints[i]);
                 this._charactersInBattle.Add(enemy);
            }

            foreach (Character character in this._charactersInBattle)
            {
                character.Init(this._cameraRectTransform, this._combatCamera);

                character.OnCharacterSelected += с =>
                {
                    if(с == this._currentCharacter)
                        return;

                    //todo запилить блокер ввода на время анимации
                    this._currentCharacter.PlayAnimation(AnimationType.Attack)
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
            foreach (Character c in this._charactersInBattle.OrderBy(_ => _.Initiative))
            {
                this._characterActionQueue.Enqueue(c);
            }

            this.BeginTurn();
        }

        private void BeginTurn()
        {
            if (this._currentCharacter != null)
            {
                this._currentCharacter.ActivateTurnMark = false;
            }

            this._currentCharacter = this._characterActionQueue.Dequeue();

            this._currentCharacter.ActivateTurnMark = true;
        }
    }
}
