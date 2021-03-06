//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Chambers.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CharacterAffliction
    {
        public int CharacterAfflictionID { get; set; }
        public int AfflictionID { get; set; }
        public int CharacterID { get; set; }
        public Nullable<int> Stage { get; set; }
        public string SelfVoteText { get; set; }
        public string OpponentVoteText { get; set; }
        public string ProgressionText { get; set; }
        public string FinaleText { get; set; }
        public string Descriptor { get; set; }
        public string VoteFailureReaction { get; set; }
    
        public virtual Affliction Affliction { get; set; }
        public virtual Character Character { get; set; }
    }
}
