using System;
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
    }
}