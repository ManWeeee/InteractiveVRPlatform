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
            foreach(var provider in providers) {
                var step = provider.GetTutorialStep();
                if(m_steps.ContainsKey(step)){
                    m_steps[step].AddStep(step);
                    continue;
                }
                m_steps.Add(step, new TutorialStepGroup(step));
            }
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
            m_currentStep = (m_steps.Count > 0) ? m_steps.First().Value : null;
            foreach(var step in m_steps) {
                Debug.Log(step.Key);
            }
            if(m_currentStep != null) {
                TutorialStepChanged?.Invoke();
            }
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
        }
    }
}