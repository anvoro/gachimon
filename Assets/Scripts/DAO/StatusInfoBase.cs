using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.DAO
{
    public abstract class StatusInfoBase : ScriptableObject
    {
        public int Duration = -1;

        public abstract void ProcessStatus(CharacterModel owner, BattlePhase battlePhase);
    }
}
