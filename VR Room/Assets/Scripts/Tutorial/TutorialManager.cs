using System;
using System.Collections.Generic;
using UnityEngine;
namespace Tutorials {
    public class TutorialManager : MonoBehaviour {
        [SerializeField]
        private Stack<TutorialStep> m_steps = new Stack<TutorialStep>();
        [SerializeField]
        private TutorialStep m_currentStep = null;

        public TutorialStep CurrentStep => m_currentStep;
        public bool IsTutorialComplete => m_currentStep == null;
        public Action TutorialStepChanged;

        private void Start() {
            m_currentStep = null;
            Container.Register(this);
        }

        public void StartTutorial(IEnumerable<TutorialStep> tutorialSteps) {
            m_steps.Clear();
            foreach(var step in tutorialSteps) {
                m_steps.Push(step);
            }
            NextStep();
        }

        public void Update() {
            if(m_currentStep == null)
                return;

            if(m_currentStep.IsCompleted) {
                NextStep();
            }
        }

        private void NextStep() {
            m_currentStep = (m_steps.Count > 0) ? m_steps.Pop() : null;
            if(m_currentStep != null) {
                TutorialStepChanged?.Invoke();
            }
        }
    }
}