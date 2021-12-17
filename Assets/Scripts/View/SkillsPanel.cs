using System;
using System.Collections.Generic;
using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class SkillsPanel : MonoBehaviour
    {
        private CharacterModel _model;

        [SerializeField] private List<SkillImage> _skillList;

        private void Awake()
        {
            foreach (SkillImage skillImage in this._skillList)
            {
                skillImage.OnClick += AssignSkill;
            }

            void AssignSkill(Skill skill)
            {
                this._model.SelectedSkill = skill;
            }
        }

        public void Init(CharacterModel model)
        {
            this._model = model;

            foreach (var image in this._skillList)
            {
                image.Clear();
            }

            for (int i = 0; i < model.Skills.Count; i++)
            {
                Skill skill = model.Skills[i];
                this._skillList[i].Init(skill, this._model.SelectedSkill == skill);
            }
        }
    }
}
