<<<<<<< HEAD
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
=======
﻿using System;
using System.Collections.Generic;

public class TutorialStep
{
    public string InstructionText { get; private set; }
    public List<TutorialCondition> Conditions { get; private set; }
    public Action OnStepStart { get; private set; }
    public Action OnStepComplete { get; private set; }

    public TutorialStep()
    {

    }

    public TutorialStep(string instructionText, List<TutorialCondition> conditions, Action onStepStart = null, Action onStepComplete = null)
    {
        InstructionText = instructionText;
        Conditions = conditions ?? new List<TutorialCondition>();
        OnStepStart = onStepStart;
        OnStepComplete = onStepComplete;
    }

    public bool IsCompleted()
    {
        foreach (var condition in Conditions)
        {
            if (!condition.IsCompleted())
                return false;
        }
        return true;
>>>>>>> 68120f30cf5cae8c3fd8d0ccd8b6259afe687662
    }
}