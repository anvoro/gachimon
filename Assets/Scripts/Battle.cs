using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.DAO;
using Assets.Scripts.Model;
using Assets.Scripts.View;
using RSG;
using UnityEngine;
using UnityEngine.UI;
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

        public static bool IsBusy { get; private set; }

        private readonly List<CharacterModel> _charactersInBattle = new List<CharacterModel>();
        
        private readonly Queue<CharacterModel> _characterActionQueue = new Queue<CharacterModel>();

        private CharacterModel _currentCharacter;

        private readonly Dictionary<CharacterModel, CharacterView> _viewByModel =
            new Dictionary<CharacterModel, CharacterView>();

        [SerializeField] private List<CharacterInfo> _gachi;
        [SerializeField] private List<CharacterInfo> _enemies;

        [SerializeField] private Transform[] _partySpawnPoints;
        [SerializeField] private Transform[] _enemySpawnPoints;

        [Header("UI")]
        [SerializeField]
        private Camera _combatCamera;
        [SerializeField]
        private RectTransform _cameraRectTransform;
        [SerializeField]
        private SkillsPanel SkillsPanel;

        private void Start()
        {
            List<CharacterModel> party = new List<CharacterModel>();

            foreach (CharacterInfo info in this._gachi)
            {
                party.Add(new CharacterModel(info));
            }

            this.SetupBattle(party);
        }

        private void SetupBattle(List<CharacterModel> party)
        {
            this._charactersInBattle.Clear();

            for (int i = 0; i < party.Count; i++)
            {
                CharacterView gachiMan = Instantiate(party[i].View, this._partySpawnPoints[i]);
                this._charactersInBattle.Add(party[i]);

                this._viewByModel.Add(party[i], gachiMan);
            }

            for (int i = 0; i < this._enemies.Count; i++)
            {
                CharacterModel enemy = new CharacterModel(this._enemies[i]);

                 CharacterView enemyViev = Instantiate(enemy.View, this._enemySpawnPoints[i]);
                 this._charactersInBattle.Add(enemy);

                 this._viewByModel.Add(enemy, enemyViev);
            }

            foreach (var pair in this._viewByModel)
            {
                pair.Value.Init(pair.Key, this._cameraRectTransform, this._combatCamera);
                pair.Value.OnCharacterSelected += OnCharacterSelected;
            }

            this.BeginRound();

            void OnCharacterSelected(CharacterModel target)
            {
                if(IsBusy == true)
                    return;

                if (this._currentCharacter.SelectedSkill.IsAvailable(this._currentCharacter, target) == false)
                    return;

                IsBusy = true;

                this._currentCharacter.SelectedSkill.Cast(this._currentCharacter, target);

                this._viewByModel[this._currentCharacter].PlayAnimation(this._currentCharacter.SelectedSkill.AnimationType)
                    .Done(() =>
                    {
                        this.EndRound();

                        IsBusy = false;
                    });
            }
        }

        private void EndRound()
        {
            this._currentCharacter.Clear();

            if (this._characterActionQueue.Count > 0)
            {
                this.BeginTurn();
            }
            else
            {
                this.BeginRound();
            }
        }

        private void BeginRound()
        {
            foreach (CharacterModel c in this._charactersInBattle.OrderBy(_ => _.Initiative))
            {
                this._characterActionQueue.Enqueue(c);
            }

            this.BeginTurn();
        }

        private void BeginTurn()
        {
            if (this._currentCharacter != null)
            {
                this._viewByModel[this._currentCharacter].ActivateTurnMark = false;
            }

            this._currentCharacter = this._characterActionQueue.Dequeue();

            this._viewByModel[this._currentCharacter].ActivateTurnMark = true;

            this.SkillsPanel.Init(this._currentCharacter);

            if(this._currentCharacter.IsActive == false)
                this.EndRound();
        }
    }
}
