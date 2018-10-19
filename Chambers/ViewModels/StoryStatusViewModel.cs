using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chambers.Models;

namespace Chambers.ViewModels
{
    public class StoryStatusViewModel
    {
        public List<ArbiterAffliction> ArbiterAfflictions { get; set; }
        public List<VictimAffliction> VictimAfflictions { get; set; }

        public StoryStatusViewModel()
        {
            var _db = new Entities();
            var characters = _db.GetCharacters();
            var arbiterAfflictions = _db.GetRegularAfflictions();
            var victimAfflictions = _db.GetVictimAfflictions();

            ArbiterAfflictions = new List<ArbiterAffliction>();
            foreach (var affliction in arbiterAfflictions)
            {
                var arbiterAffliction = new ArbiterAffliction();
                arbiterAffliction.Affliction = affliction;

                arbiterAffliction.CharactersWithStages = new List<CharacterWithStages>();
                foreach (var character in characters)
                {
                    var characterWithStages = new CharacterWithStages();
                    characterWithStages.CharacterID = character.CharacterId;
                    characterWithStages.CharacterName = character.Name;
                    characterWithStages.AfflictionStage = new List<AfflictionStage>();
                    var characterStages = _db.GetCharacterStages(character.CharacterId, affliction.AfflicationId);
                    for (var i = 0; i <= 5; i++)
                    {
                        var afflictionStage = new AfflictionStage();
                        var stage = characterStages.Where(s => !string.IsNullOrEmpty(s.Descriptor) && !string.IsNullOrEmpty(s.FinaleText) && !string.IsNullOrEmpty(s.ProgressionText)).Where(c => c.Stage.Value == i).FirstOrDefault();
                        if (stage != null)
                        {
                            afflictionStage.AfflictionStageID = stage.CharacterAfflictionID;
                            afflictionStage.IsCompleted = true;
                        }
                        else
                        {
                            afflictionStage.IsCompleted = false;
                        }
                        characterWithStages.AfflictionStage.Add(afflictionStage);
                    }
                    arbiterAffliction.CharactersWithStages.Add(characterWithStages);
                }
                ArbiterAfflictions.Add(arbiterAffliction);
            }

            VictimAfflictions = new List<VictimAffliction>();
            foreach (var affliction in victimAfflictions)
            {
                var victimAffliction = new VictimAffliction();
                victimAffliction.Affliction = affliction;

                victimAffliction.CharactersWithStages = new List<CharacterWithStages>();
                foreach (var character in characters)
                {
                    var characterWithStages = new CharacterWithStages();
                    characterWithStages.CharacterID = character.CharacterId;
                    characterWithStages.CharacterName = character.Name;
                    characterWithStages.AfflictionStage = new List<AfflictionStage>();
                    var characterStages = _db.GetCharacterStages(character.CharacterId, affliction.AfflicationId);
                    for (var i = 0; i <= 10; i++)
                    {
                        var afflictionStage = new AfflictionStage();
                        var stage = characterStages.Where(s => !string.IsNullOrEmpty(s.Descriptor) && !string.IsNullOrEmpty(s.FinaleText) && !string.IsNullOrEmpty(s.ProgressionText)).Where(c => c.Stage.Value == i).FirstOrDefault();
                        if (stage != null)
                        {
                            afflictionStage.AfflictionStageID = stage.CharacterAfflictionID;
                            afflictionStage.IsCompleted = true;
                        }
                        else
                        {
                            afflictionStage.IsCompleted = false;
                        }
                        characterWithStages.AfflictionStage.Add(afflictionStage);
                    }
                    victimAffliction.CharactersWithStages.Add(characterWithStages);
                }
                VictimAfflictions.Add(victimAffliction);
            }
        }
    }

    public class ArbiterAffliction
    {
        public Affliction Affliction { get; set; }
        public List<CharacterWithStages> CharactersWithStages { get; set; }
    }

    public class VictimAffliction
    {
        public Affliction Affliction{ get; set; }
        public List<CharacterWithStages> CharactersWithStages { get; set; }
    }

    public class CharacterWithStages
    {
        public int CharacterID { get; set; }
        public string CharacterName { get; set; }
        public List<AfflictionStage> AfflictionStage { get; set; }
    }

    public class AfflictionStage
    {
        public int AfflictionStageID { get; set; }
        public bool IsCompleted { get; set; }
    }
}