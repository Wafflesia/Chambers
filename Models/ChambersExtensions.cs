using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Chambers.Models
{
    public partial class Entities : DbContext
    {
        public List<Character> GetCharacters()
        {
            return Characters.ToList();
        }

        public List<Affliction> GetVictimAfflictions()
        {
            return Afflictions.Where(a => a.IsAdvancedIllness).ToList();
        }

        public List<Affliction> GetRegularAfflictions()
        {
            return Afflictions.Where(a => !a.IsAdvancedIllness).ToList();
        }

        public List<CharacterAffliction> GetCharacterStages(int characterID, int afflictionID)
        {
            var stages = CharacterAfflictions.Where(c => c.CharacterID == characterID && c.AfflictionID == afflictionID).ToList();
            if (!stages.Any())
            {
                stages = GetDefaultStages(afflictionID);
            }
            return stages.OrderBy(s => s.Stage).ToList();
        }

        public List<CharacterAffliction> GetDefaultStages(int afflictionID)
        {
            return CharacterAfflictions.Where(c => c.AfflictionID == afflictionID).ToList();
        }
    }
}