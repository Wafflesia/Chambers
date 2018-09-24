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
        public Party _party;
        public FinishedStoryViewModel _vm;

        public ActionResult Index()
        {
            var vm = new HomeViewModel();
            return View(vm);
        }

        public ActionResult Story(StoryViewModel model)
        {
            _party = MapStoryCharacters(model);
            var finished = false;
            var failures = 0;
            _vm = new FinishedStoryViewModel(_party);
            AddCharacterIntros();
            while (!finished)
            {
                WriteText("Once again, the buttons in front of [LAName] and [RAName] light up, beckoning for them to make a decision...");
                //Do the selection
                var votes = Vote(failures);

                foreach (var vote in votes)
                {
                    WriteText(vote.VoteText);
                }

                if (votes.All(v => v.IsLeftVote))
                {
                    _party.LeftArbiter.CurrentReluctance += _party.LeftArbiter.Character.Reluctance.Value;
                    _party.RightArbiter.CurrentReluctance = 0;
                    _party.LeftArbiter.CurrentStageNum++;
                    WriteText(ParseTeamText(_party.LeftArbiter.CurrentStage.ProgressionText, _party.LeftArbiter));
                }
                else if (votes.All(v => !v.IsLeftVote))
                {
                    _party.RightArbiter.CurrentReluctance += _party.RightArbiter.Character.Reluctance.Value;
                    _party.LeftArbiter.CurrentReluctance = 0;
                    _party.RightArbiter.CurrentStageNum++;
                    WriteText(ParseTeamText(_party.RightArbiter.CurrentStage.ProgressionText, _party.RightArbiter));
                }
                else
                {
                    _party.LeftArbiter.CurrentReluctance -= 10;
                    _party.RightArbiter.CurrentReluctance -= 10;
                    WriteText(ParseTeamText(_party.LeftArbiter.CurrentStage.VoteFailureReaction, _party.LeftArbiter));
                    WriteText(ParseTeamText(_party.RightArbiter.CurrentStage.VoteFailureReaction, _party.RightArbiter));

                    _party.LeftVictim.CurrentStageNum++;
                    _party.RightVictim.CurrentStageNum++;
                    WriteText(ParseTeamText(_party.LeftVictim.CurrentStage.ProgressionText, _party.LeftVictim));
                    WriteText(ParseTeamText(_party.RightVictim.CurrentStage.ProgressionText, _party.RightVictim));
                    
                }
                //Advance stages, write text results

                //Check for end condition
                finished = IsAnyCharacterFullyTransformed();
            }

            FinishStory();
            //Do finale stuff
            return View(_vm);
        }

        private void FinishStory()
        {
            WriteText("Suddenly the doors to the chambers hiss, the floors beneath them moving, depositing all of the changed adventurers outside.");
            if (_party.IsLoss)
            {
                WriteText(_party.LeftArbiter.CurrentStage.FinaleText);
                WriteText(_party.RightArbiter.CurrentStage.FinaleText);
                WriteText("[LAName] and [RAName] rush outside as best they can and watch with horror as their former comrades are deposited on the conveyor in front of them.");
                WriteText(_party.LeftVictim.CurrentStage.FinaleText);
                WriteText(_party.RightVictim.CurrentStage.FinaleText);
            }
            else
            {
                WriteText(_party.Arbiters.Where(a => a != _party.Loser).FirstOrDefault().CurrentStage.FinaleText);
                WriteText(_party.LeftVictim.CurrentStage.FinaleText);
                WriteText(_party.RightVictim.CurrentStage.FinaleText);
                WriteText(_party.Loser.CurrentStage.FinaleText);
            }
        }

        public void WriteText(string text)
        {
            text = FillText(text);
            AddText(_vm, text);
        }

        public string ParseTeamText(string text, StoryCharacter character)
        {
            if (character.Position == Position.LeftArbiter)
            {
                return text.Replace("[Name]", "[LAName]")
                    .Replace("[Title]", "[LATitle]")
                    .Replace("[PName]", "[LVName]")
                    .Replace("[PTitle]", "[LVTitle]")
                    .Replace("[OName]", "[RAName]")
                    .Replace("[OTitle]", "[RATitle]")
                    .Replace("[OVName]", "[RVName]")
                    .Replace("[OVTitle]", "[RVTitle]");
            }
            if (character.Position == Position.RightArbiter)
            {
                return text.Replace("[Name]", "[RAName]")
                    .Replace("[Title]", "[RATitle]")
                    .Replace("[PName]", "[RVName]")
                    .Replace("[PTitle]", "[RVTitle]")
                    .Replace("[OName]", "[LAName]")
                    .Replace("[OTitle]", "[LATitle]")
                    .Replace("[OVName]", "[LVName]")
                    .Replace("[OVTitle]", "[LVTitle]");
            }
            if (character.Position == Position.LeftVictim)
            {
                return text.Replace("[Name]", "[LVName]")
                    .Replace("[Title]", "[LVTitle]")
                    .Replace("[PName]", "[LAName]")
                    .Replace("[PTitle]", "[LATitle]")
                    .Replace("[OName]", "[RAName]")
                    .Replace("[OTitle]", "[RATitle]")
                    .Replace("[OVName]", "[RVName]")
                    .Replace("[OVTitle]", "[RVTitle]");
            }
            if (character.Position == Position.RightVictim)
            {
                return text.Replace("[Name]", "[RVName]")
                    .Replace("[Title]", "[RVTitle]")
                    .Replace("[PName]", "[RAName]")
                    .Replace("[PTitle]", "[RATitle]")
                    .Replace("[OName]", "[LAName]")
                    .Replace("[OTitle]", "[LATitle]")
                    .Replace("[OVName]", "[LVName]")
                    .Replace("[OVTitle]", "[LVTitle]");
            }
            return text;
        }

        public string FillText(string text)
        {
            return text.Replace("[LVName]", _party.LeftVictimName)
                       .Replace("[LAName]", _party.LeftArbiterName)
                       .Replace("[RVName]", _party.RightVictimName)
                       .Replace("[RAName]", _party.RightArbiterName)
                       .Replace("[LVTitle]", _party.LeftVictim.CurrentStage.Descriptor)
                       .Replace("[RVTitle]", _party.RightVictim.CurrentStage.Descriptor)
                       .Replace("[LATitle]", _party.LeftArbiter.CurrentStage.Descriptor)
                       .Replace("[RATitle]", _party.RightArbiter.CurrentStage.Descriptor);
            
        }

        public void AddText(FinishedStoryViewModel vm, string text)
        {
            vm.Story += text + "<br/><br/>";
        }

        private List<Vote> Vote(int failures)
        {
            var leftArbiterVote = new Vote();
            var rightArbiterVote = new Vote();

            var rnd = new Random();
            var leftVote = rnd.Next(1, 101);
            var rightVote = rnd.Next(1, 101);
            var leftModifiers = _party.LeftArbiter.Character.Selflessness - _party.LeftArbiter.CurrentReluctance + _party.LeftArbiter.CurrentCompromise;
            var rightModifiers = _party.RightArbiter.Character.Selflessness - _party.RightArbiter.CurrentReluctance + _party.RightArbiter.CurrentCompromise;
            if (leftModifiers < 10)
            {
                leftModifiers = 10;
            }
            if (leftModifiers > 90)
            {
                leftModifiers = 90;
            }
            if (rightModifiers < 10)
            {
                rightModifiers = 10;
            }
            if (rightModifiers > 90)
            {
                rightModifiers = 90;
            }

            if (leftVote <= _party.LeftArbiter.Character.Selflessness - _party.LeftArbiter.CurrentReluctance + _party.LeftArbiter.CurrentCompromise)
            {
                leftArbiterVote.IsLeftVote = true;
                leftArbiterVote.VoteText = ParseTeamText(_party.LeftArbiter.CurrentStage.SelfVoteText, _party.LeftArbiter);
            }
            else
            {
                leftArbiterVote.IsLeftVote = false;
                leftArbiterVote.VoteText = ParseTeamText(_party.LeftArbiter.CurrentStage.OpponentVoteText, _party.LeftArbiter);
            }
            if (rightVote <= _party.RightArbiter.Character.Selflessness - _party.RightArbiter.CurrentReluctance  + _party.RightArbiter.CurrentCompromise)
            {
                rightArbiterVote.IsLeftVote = false;
                rightArbiterVote.VoteText = ParseTeamText(_party.RightArbiter.CurrentStage.SelfVoteText, _party.RightArbiter);
            }
            else
            {
                rightArbiterVote.IsLeftVote = true;
                rightArbiterVote.VoteText = ParseTeamText(_party.RightArbiter.CurrentStage.OpponentVoteText, _party.RightArbiter);
            }

            var votes = new List<Vote>();
            votes.Add(leftArbiterVote);
            votes.Add(rightArbiterVote);
            _party.LeftArbiter.PreviousVote = leftArbiterVote;
            _party.RightArbiter.PreviousVote = rightArbiterVote;
            return votes;
        }

        private void AddCharacterIntros()
        {
            _vm.Story += _party.LeftArbiter.CurrentStage.ProgressionText;
            _vm.Story += _party.LeftVictim.CurrentStage.ProgressionText;
            _vm.Story += _party.RightVictim.CurrentStage.ProgressionText;
            _vm.Story += _party.RightArbiter.CurrentStage.ProgressionText;
        }

        private bool IsAnyCharacterFullyTransformed()
        {
            return (_party.Victims.Any(v => v.CurrentStageNum >= 10) || _party.Arbiters.Any(a => a.CurrentStageNum >= 5));
        }

        private Party MapStoryCharacters(StoryViewModel model)
        {
            var _db = new Entities();
            var victims = new List<StoryCharacter>();
            var arbiters = new List<StoryCharacter>();
            var party = new Party();

            victims.AddRange(new List<StoryCharacter>{
                new StoryCharacter()
                {
                    Character = model.LeftVictim,
                    Affliction = model.LeftVictimAffliction,
                    Stages = _db.GetCharacterStages(model.LeftVictim.CharacterId, model.LeftVictimAffliction.AfflicationId),
                    CurrentStageNum = 0,
                    Position = Position.LeftVictim
                },
                new StoryCharacter()
                {
                    Character = model.RightVictim,
                    Affliction = model.RightVictimAffliction,
                    Stages = _db.GetCharacterStages(model.RightVictim.CharacterId, model.RightVictimAffliction.AfflicationId),
                    CurrentStageNum = 0,
                    Position = Position.RightVictim
                }
            });

            arbiters.AddRange(new List<StoryCharacter>{
                new StoryCharacter()
                {
                    Character = model.LeftArbiter,
                    Affliction = model.LeftArbiterAfflication,
                    Stages = _db.GetCharacterStages(model.LeftArbiter.CharacterId, model.LeftArbiterAfflication.AfflicationId),
                    CurrentStageNum = 0,
                    CurrentReluctance = 0,
                    CurrentCompromise = 0,
                    Position = Position.LeftArbiter
                },
                new StoryCharacter()
                {
                    Character = model.RightArbiter,
                    Affliction = model.RightArbiterAffliction,
                    Stages = _db.GetCharacterStages(model.RightArbiter.CharacterId, model.RightArbiterAffliction.AfflicationId),
                    CurrentStageNum = 0,
                    CurrentReluctance = 0,
                    CurrentCompromise = 0,
                    Position = Position.RightArbiter
                }
                });
            party.Arbiters = arbiters;
            party.Victims = victims;
            return party;
        }
    }
}