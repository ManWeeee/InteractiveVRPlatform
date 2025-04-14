using System;

namespace Tutorials {
    [Serializable]
    public class TutorialStep {
        public string Description { get; }
        public ITutorialCondition Condition { get; }

        public TutorialStep(string description, ITutorialCondition condition) {
            Description = description;
            Condition = condition;
        }

        public bool IsCompleted => Condition.IsMet();
    }
}