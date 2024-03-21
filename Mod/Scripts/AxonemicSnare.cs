using System;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
    [Serializable]
    public class AxonemicSnare : BaseMutation
    {
        public AxonemicSnare()
        {
            DisplayName = "Axonemic Snare";
            base.Type = "Mental";
        }

        public override string GetDescription()
        {
            return "You snare a line of space and ingest it.";
        }
    }
}