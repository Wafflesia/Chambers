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
    }
}