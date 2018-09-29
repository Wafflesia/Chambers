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
        public bool IsLoss
        {
            get
            {
                return Victims.All(v => v.CurrentStageNum >= 10);
            }
        }

        public StoryCharacter Loser
        {
            get
            {
                return Arbiters.Where(a => a.CurrentStageNum >= 5).FirstOrDefault();
            }
        }

        public string LeftArbiterName { 
            get
            {
                return LeftArbiter.Character.Name;
            }
        }
        public string RightArbiterName
        {
            get
            {
                return RightArbiter.Character.Name;
            }
        }
        public string LeftVictimName
        {
            get
            {
                return LeftVictim.Character.Name;
            }
        }
        public string RightVictimName
        {
            get
            {
                return RightVictim.Character.Name;
            }
        }
        public StoryCharacter LeftArbiter
        {
            get
            {
                return Arbiters.Where(a => a.Position == Position.LeftArbiter).FirstOrDefault();
            }
        }
        public StoryCharacter RightArbiter
        {
            get
            {
                return Arbiters.Where(a => a.Position == Position.RightArbiter).FirstOrDefault();
            }
        }
        public StoryCharacter LeftVictim
        {
            get
            {
                return Victims.Where(v => v.Position == Position.LeftVictim).FirstOrDefault();
            }
        }
        public StoryCharacter RightVictim
        {
            get
            {
                return Victims.Where(v => v.Position == Position.RightVictim).FirstOrDefault();
            }
        }
    }

    public class StoryCharacter
    {
        public Character Character { get; set; }
        public Affliction Affliction { get; set; }
        public List<CharacterAffliction> Stages { get; set; }
        public int CurrentStageNum { get; set; }
        public Position Position { get; set; }
        public int CurrentReluctance { get; set; }
        public int CurrentCompromise { get; set; }
        public Vote PreviousVote { get; set; }

        public CharacterAffliction CurrentStage {
            get
            {
                return Stages.Where(s => s.Stage.Value == CurrentStageNum).FirstOrDefault();
            }
        }
    }

    public enum Position
    {
        LeftArbiter=1,
        LeftVictim=2,
        RightVictim=3,
        RightArbiter=4
    }
}