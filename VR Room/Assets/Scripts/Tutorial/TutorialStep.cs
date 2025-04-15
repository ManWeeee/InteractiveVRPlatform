using System;
using Unity.VisualScripting;

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

        public override bool Equals(object obj) {
            if(obj is TutorialStep otherStep) {
                return Condition.Equals(otherStep.Condition);
            }
            return false;
        }
        public override int GetHashCode() {
            return Condition != null ? Condition.GetHashCode() : 0;
        }
    }
}