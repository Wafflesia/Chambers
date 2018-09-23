using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chambers.Models
{
    public class Party
    {
        public List<StoryCharacter> Arbiters { get; set; }
        public List<StoryCharacter> Victims { get; set; }
    }

    public class StoryCharacter
    {
        public Character Character { get; set; }
        public Affliction Affliction { get; set; }
        public int CurrentStage { get; set; }
        public Position Position { get; set; }
    }

    public enum Position
    {
        LeftArbiter=1,
        LeftVictim=2,
        RightVictim=3,
        RightArbiter=4
    }
}