using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chambers.Models;
using Chambers.Custom.Extensions;

namespace Chambers.ViewModels
{
    public class HomeViewModel
    {
        public List<Character> Characters { get; set; }
        public List<Affliction> ArbiterAfflictions { get; set; }
        public List<Affliction> VictimAfflictions { get; set; }
        
        public Character LeftArbiter { get; set; }
        public Character RightArbiter { get; set; }
        public Character LeftVictim { get; set; }
        public Character RightVictim { get; set; }

        public Affliction LeftArbiterAfflication { get; set; }
        public Affliction RightArbiterAffliction { get; set; }
        public Affliction LeftVictimAffliction { get; set; }
        public Affliction RightVictimAffliction { get; set; }

        public bool UseSelflessness { get; set; }
        public bool UseFrustation { get; set; }
    }
}