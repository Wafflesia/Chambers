using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chambers.Custom.Extensions;
using Chambers.ViewModels;
using Chambers.Models;

namespace Chambers.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var vm = new HomeViewModel();
            return View(vm);
        }

        public ActionResult Story(StoryViewModel model)
        {
            var party = MapStoryCharacters(model);
            var finished = false;
            while (!finished)
            {

            }
            return View();
        }

        private Party MapStoryCharacters(StoryViewModel model)
        {
            var victims = new List<StoryCharacter>();
            var arbiters = new List<StoryCharacter>();
            var party = new Party();

            victims.AddRange(new List<StoryCharacter>{
                new StoryCharacter()
                {
                    Character = model.LeftVictim,
                    Affliction = model.LeftVictimAffliction,
                    CurrentStage = 0,
                    Position = Position.LeftVictim
                },
                new StoryCharacter()
                {
                    Character = model.RightVictim,
                    Affliction = model.RightVictimAffliction,
                    CurrentStage = 0,
                    Position = Position.RightVictim
                }
            });

            arbiters.AddRange(new List<StoryCharacter>{
                new StoryCharacter()
                {
                    Character = model.LeftArbiter,
                    Affliction = model.LeftArbiterAfflication,
                    CurrentStage = 0,
                    Position = Position.LeftArbiter
                },
                new StoryCharacter()
                {
                    Character = model.RightArbiter,
                    Affliction = model.RightArbiterAffliction,
                    CurrentStage = 0,
                    Position = Position.RightArbiter
                }
                });
            party.Arbiters = arbiters;
            party.Victims = victims;
            return party;
        }
    }
}