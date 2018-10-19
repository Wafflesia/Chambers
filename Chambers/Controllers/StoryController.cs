using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chambers.Models;
using Chambers.ViewModels;

namespace Chambers.Controllers
{
    public class StoryController : Controller
    {
        // GET: Story
        public ActionResult Index()
        {
            var vm = new StoryStatusViewModel();
            return View(vm);
        }

        public ActionResult Edit(int characterAfflictionID)
        {
            var vm = new EditStageViewModel(characterAfflictionID);
            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(EditStageViewModel model)
        {
            var _db = new Entities();
            _db.UpdateCharacterAffliction(model);
            return RedirectToAction("Index");
        }

        public ActionResult Create(int characterID, int afflictionID, int stageNum)
        {
            var _db = new Entities();
            var vm = new CreateStageViewModel();
            vm.CharacterID = characterID;
            vm.CharacterName = _db.Characters.Where(c => c.CharacterId == characterID).FirstOrDefault().Name;
            vm.AfflictionID = afflictionID;
            vm.StageNum = stageNum;
            var affliction = _db.Afflictions.Where(a => a.AfflicationId == afflictionID).FirstOrDefault();
            vm.IsVictimAffliction = affliction.IsAdvancedIllness;
            vm.AfflictionName = affliction.Name;
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(CreateStageViewModel model)
        {
            var _db = new Entities();
            var affliction = _db.Afflictions.Where(a => a.AfflicationId == model.AfflictionID).FirstOrDefault();
            var character = _db.Characters.Where(c => c.CharacterId == model.CharacterID).FirstOrDefault();
            var characterAffliction = new CharacterAffliction()
            {
                AfflictionID = model.AfflictionID,
                CharacterID = model.CharacterID,
                Descriptor = model.Descriptor,
                FinaleText = model.FinaleText,
                OpponentVoteText = model.OVText,
                SelfVoteText = model.SVText,
                Stage = model.StageNum,
                ProgressionText = model.ProgressionText,
                Character = character,
                Affliction = affliction
            };
            _db.CharacterAfflictions.Add(characterAffliction);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}