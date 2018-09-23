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

        public HomeViewModel()
        {
            var _db = new Entities();
            this.Characters = _db.GetCharacters();
            this.ArbiterAfflictions = _db.GetRegularAfflictions();
            this.VictimAfflictions = _db.GetVictimAfflictions();
        }
    }
}