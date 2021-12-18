using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.DAO
{
    [CreateAssetMenu(menuName = "Status/IconStatus")]
    internal class IconStatus : StatusInfoBase
    {
        public override void ProcessStatus(CharacterModel owner, BattlePhase battlePhase)
        {
        }
    }
}
