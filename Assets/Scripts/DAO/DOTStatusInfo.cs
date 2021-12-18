using System;
using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.DAO
{
    [CreateAssetMenu(menuName = "Status/DOT")]
    public class DOTStatusInfo : StatusInfoBase
    {
        public int DOTDamage = 35;

        public override void ProcessStatus(CharacterModel owner, BattlePhase battlePhase)
        {
            switch (battlePhase)
            {
                case BattlePhase.TurnBegin:
                    owner.Hurt(this.DOTDamage);
                    break;

                case BattlePhase.TurnEnd:
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(battlePhase), battlePhase, null);
            }
        }
    }
}
