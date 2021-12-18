using System;
using Assets.Scripts.DAO;

namespace Assets.Scripts.Model
{
    public class Status
    {
        public string Tag { get; }

        public int Duration { get; set; }

        public Action<CharacterModel, BattlePhase> Action { get; }

        public Status(StatusInfoBase statusInfo)
        {
            this.Tag = statusInfo.name;
            this.Duration = statusInfo.Duration;
            this.Action = statusInfo.ProcessStatus;
        }
    }
}
