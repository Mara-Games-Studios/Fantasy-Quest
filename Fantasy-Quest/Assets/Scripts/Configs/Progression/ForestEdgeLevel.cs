namespace Configs.Progression
{
    internal class ForestEdgeLevel
    {
        public bool FirstDialoguePassed = false;
        public bool BagTaken = false;
        public bool ExplanationListened = false;
        public bool MouseInHayGamePassed = false;
        public bool MonsterCutsceneTriggered = false;
        public bool SquirrelGamePassed = false;
        public bool EggTakenByCymon = false;
        public bool AcornTakenByCymon = false;
        public bool AltarGamePassedCorrectly = false;
        public bool AllItemTaken => EggTakenByCymon && AcornTakenByCymon;
    }
}
