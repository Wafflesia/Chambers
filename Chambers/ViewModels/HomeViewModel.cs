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

        public int LeftArbiterID { get; set; }
        public int RightArbiterID { get; set; }
        public int LeftVictimID { get; set; }
        public int RightVictimID { get; set; }

        public int LeftArbiterAfflictionID { get; set; }
        public int RightArbiterAfflictionID { get; set; }
        public int LeftVictimAfflictionID { get; set; }
        public int RightVictimAfflictionID { get; set; }

        public Character LeftArbiter {
            get
            {
                var _db = new Entities();
                return _db.GetCharacters().Where(c => c.CharacterId == LeftArbiterID).FirstOrDefault();
            }
        }
        public Character RightArbiter
        {
            get
            {
                var _db = new Entities();
                return _db.GetCharacters().Where(c => c.CharacterId == RightArbiterID).FirstOrDefault();
            }
        }
        public Character LeftVictim
        {
            get
            {
                var _db = new Entities();
                return _db.GetCharacters().Where(c => c.CharacterId == LeftVictimID).FirstOrDefault();
            }
        }
        public Character RightVictim
        {
            get
            {
                var _db = new Entities();
                return _db.GetCharacters().Where(c => c.CharacterId == RightVictimID).FirstOrDefault();
            }
        }

        public Affliction LeftArbiterAffliction {
            get
            {
                var _db = new Entities();
                return _db.GetRegularAfflictions().Where(c => c.AfflicationId == LeftArbiterAfflictionID).FirstOrDefault();
            }
        }
        public Affliction RightArbiterAffliction
        {
            get
            {
                var _db = new Entities();
                return _db.GetRegularAfflictions().Where(c => c.AfflicationId == RightArbiterAfflictionID).FirstOrDefault();
            }
        }
        public Affliction LeftVictimAffliction
        {
            get
            {
                var _db = new Entities();
                return _db.GetVictimAfflictions().Where(c => c.AfflicationId == LeftVictimAfflictionID).FirstOrDefault();
            }
        }
        public Affliction RightVictimAffliction
        {
            get
            {
                var _db = new Entities();
                return _db.GetVictimAfflictions().Where(c => c.AfflicationId == RightVictimAfflictionID).FirstOrDefault();
            }
        }

        public bool UseSelflessness { get; set; }
        public bool UseFrustation { get; set; }

        public HomeViewModel()
        {
            var _db = new Entities();
            this.Characters = _db.GetCharacters();
            this.ArbiterAfflictions = _db.GetRegularAfflictions();
            this.VictimAfflictions = _db.GetVictimAfflictions();
        }
    }
}