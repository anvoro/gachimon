using System;
using Assets.Scripts.DAO;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class Status
    {
        public Sprite Sprite { get; }

        public string Tag { get; }

        public int Duration { get; set; }

        public Action<CharacterModel, BattlePhase> Action { get; }

        public Status(StatusInfoBase statusInfo)
        {
            this.Tag = statusInfo.name;
            this.Duration = statusInfo.Duration;
            this.Action = statusInfo.ProcessStatus;
            this.Sprite = statusInfo.Sprite;
        }
    }
}
