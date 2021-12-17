﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DAO
{
    public enum Side
    {
        Player = 0,
        Enemy = 1,
    }

    public enum AnimationType
    {
        Attack = 0,
        Defense = 1,
    }

    [CreateAssetMenu]
    public class CharacterInfo : ScriptableObject
    {
        public CharacterView View;

        public int MaxHealth = 100;

        public Side Side;

        public int Initiative = 1;

        public List<SkillInfo> Skills;
    }
}
