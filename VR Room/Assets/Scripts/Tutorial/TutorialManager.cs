<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Tutorials {
    public class TutorialManager : MonoBehaviour {
        [SerializeField]
        private Dictionary<TutorialStep, TutorialStepGroup> m_steps = new ();
        [SerializeField]
        private TutorialStepGroup m_currentStep = null;
        private List<TutorialStepGroup> m_orderedSteps = new();
        private int m_currentIndex = -1;

        public TutorialStepGroup CurrentStep => m_currentStep;
        public bool IsTutorialComplete => m_currentStep == null;
        public Action TutorialStepChanged;

        private void Start() {
            m_currentStep = null;
            Container.Register(this);
        }

        private void StartTutorial() {
            NextStep();
        }

        public void SetTutorial(IEnumerable<ITutorialProvider> providers) {
            m_steps.Clear();
            m_orderedSteps.Clear();
            foreach(var provider in providers) {
                var step = provider.GetTutorialStep();
                if(m_steps.ContainsKey(step)) {
                    m_steps[step].AddStep(step);
                    continue;
                }

                var group = new TutorialStepGroup(step);
                m_steps.Add(step, group);
                m_orderedSteps.Add(group); // Keep order
            }
            m_currentIndex = -1;
            StartTutorial();
        }

        public void Update() {
            if(m_currentStep == null)
                return;

            if(m_currentStep.IsCompleted()) {
                NextStep();
            }
        }

        private void NextStep() {
            m_currentIndex++;
            if(m_currentIndex >= m_orderedSteps.Count) {
                m_currentStep = null;
                Debug.Log("There are no more steps in the tutorial");
                return;
            }

            m_currentStep = m_orderedSteps[m_currentIndex];

            Debug.Log($"Switched to step: {m_currentStep}");

            TutorialStepChanged?.Invoke();
        }
    }
    public class TutorialStepGroup {
        List<TutorialStep> tutorialSteps;
        public TutorialStepGroup(TutorialStep step) {
            tutorialSteps = new List<TutorialStep>();
            AddStep(step);
        }
        public void AddStep(TutorialStep step) {
            tutorialSteps.Add(step);
        }


        public int Completed { get; private set; }

        public int Count => tutorialSteps.Count;

        public TutorialStep TutorialStepType => tutorialSteps[0];

        public bool IsCompleted() {
            var completed = 0;
            foreach(var step in tutorialSteps) {
                if(step.IsCompleted) {
                    completed++;
                }
            }
            Completed = completed;
            return Completed == tutorialSteps.Count;
        }

        public override string ToString() {
            return $"{tutorialSteps.First().Description} - {Completed}/{tutorialSteps.Count}";
=======
﻿using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] CarPartType partToDoATutorialTo;

    private List<TutorialStep> steps;

    private void Start()
    {
        if(!Container.TryGetInstance<LevelInfoHolder>(out LevelInfoHolder levelInfoHolder))
        {
            Debug.LogError($"{this} was unnable to get {levelInfoHolder.GetType()}");
        }

        levelInfoHolder.LevelInfoChanged += SetTutorialSteps;
        SetTutorialSteps(levelInfoHolder.CurrentLevelInfo);
    }

    private void SetTutorialSteps(LevelInfo levelInfo)
    {
        partToDoATutorialTo = levelInfo.brokenPartType;
        List<CarPart> parts = FindObjectsOfType<CarPart>(false)
            .Where((part) => part.PartInfo.GetCarPartType == partToDoATutorialTo)
            .ToList();
        foreach (CarPart part in parts) {
            steps.Add(part.GetTutorialStep());
>>>>>>> 68120f30cf5cae8c3fd8d0ccd8b6259afe687662
        }
    }
}