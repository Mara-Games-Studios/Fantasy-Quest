﻿using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace DI.Project.Services
{
    public struct BubbleConfig
    {
        public bool CanShow;
        public DialogueBubble.Type BubbleType;
        public List<Sprite> EmoteIcons;
    }

    public class BubbleEventSystem
    {
        [Preserve]
        public BubbleEventSystem() { }

        public Action<BubbleConfig> OnTriggerBubble;
    }
}
