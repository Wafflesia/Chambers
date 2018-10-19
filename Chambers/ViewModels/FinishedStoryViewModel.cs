using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chambers.Models;

namespace Chambers.ViewModels
{
    public class FinishedStoryViewModel
    {
        public string Story;

        public FinishedStoryViewModel(Party party)
        {
            this.Story = "The four people come to in a strange set of chambers. " + party.LeftArbiterName + " and " + party.RightArbiterName + " find themselves in strange chambers separated by glass, the only notable features in each being two large buttons. Below them, " + party.LeftVictimName +  " slowly wakes up in heavy restraints.<br/><br/>";
            Story += "\"Good evening!\" A voice cries out, seemingly out of nowhere. \"Today, with the help of our volunteers, we'll finally decide which is better - " + party.LeftArbiter.Affliction.Name + " or " + party.RightArbiter.Affliction.Name + "!\" Just watch out, because if you can't agree, we have surprises in store for your friends!\" <br/><br/>";
            Story += "With that, the buttons in front of the two contestants light up and the game begins...<br/>";
        }
    }

    
}