using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chambers.ViewModels
{
    public class CreateStageViewModel
    {
        public int CharacterID { get; set; }
        public int AfflictionID { get; set; }
        public string AfflictionName { get; set; }
        public string CharacterName { get; set; }
        public string Descriptor { get; set; }
        public string ProgressionText { get; set; }
        public string OVText { get; set; }
        public string SVText { get; set; }
        public string FinaleText { get; set; }
        public bool IsVictimAffliction { get; set; }
        public int StageNum { get; set; }
    }
}