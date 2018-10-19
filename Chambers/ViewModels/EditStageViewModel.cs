using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chambers.Models;

namespace Chambers.ViewModels
{
    public class EditStageViewModel
    {
        public int CharacterAfflictionID { get; set; }
        public string AfflictionName { get; set; }
        public string CharacterName { get; set; }
        public string Descriptor { get; set; }
        public string ProgressionText { get; set; }
        public string OVText { get; set; }
        public string SVText { get; set; }
        public string FinaleText { get; set; }
        public int StageNum { get; set; }
        public bool IsVictimAffliction { get; set; }

        public EditStageViewModel() { }

        public EditStageViewModel(int characterAfflictionID)
        {
            var _db = new Entities();
            var stage = _db.GetCharacterStage(characterAfflictionID);
            this.AfflictionName = stage.Affliction.Name;
            this.CharacterName = stage.Character.Name;
            this.Descriptor = stage.Descriptor;
            this.FinaleText = stage.FinaleText;
            this.IsVictimAffliction = stage.Affliction.IsAdvancedIllness;
            this.OVText = stage.OpponentVoteText;
            this.SVText = stage.SelfVoteText;
            this.ProgressionText = stage.ProgressionText;
            this.StageNum = stage.Stage.Value;
        }
    }
}