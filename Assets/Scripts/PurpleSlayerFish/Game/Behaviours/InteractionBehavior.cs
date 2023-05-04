using System;
using PurpleSlayerFish.Core.Behaviours;
using PurpleSlayerFish.Core.Services.Tooltips;
using PurpleSlayerFish.Game.Controllers;
using UnityEngine;
using Zenject;

namespace PurpleSlayerFish.Game.Processors.InteractionProcessor
{
    public class InteractionBehavior : AbstractBehaviour
    {
        public AInteraction Interaction;
        
        public override void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}