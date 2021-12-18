using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.DAO
{
    public abstract class StatusInfoBase : ScriptableObject
    {
        public Sprite Sprite;

        public int Duration = -1;

        public abstract void ProcessStatus(CharacterModel owner, BattlePhase battlePhase);
    }
}
