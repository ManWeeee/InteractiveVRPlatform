using System.Collections.Generic;
using UnityEngine;
namespace Tutorials {
    public class TutorialManager : MonoBehaviour {
        [SerializeField]
        private Queue<TutorialStep> steps = new Queue<TutorialStep>();
        [SerializeField]
        private TutorialStep currentStep = null;

        public TutorialStep GetCurrentStep() => currentStep;
        public bool IsTutorialComplete => currentStep == null;
        private void Start() {
            currentStep = null;
            Container.Register(this);
        }

        public void StartTutorial(IEnumerable<TutorialStep> tutorialSteps) {
            steps.Clear();
            foreach(var step in tutorialSteps) {
                steps.Enqueue(step);
            }
            NextStep();
        }

        public void Update() {
            if(currentStep == null)
                return;

            if(currentStep.IsCompleted) {
                NextStep();
            }
        }

        private void NextStep() {
            currentStep = (steps.Count > 0) ? steps.Dequeue() : null;
        }
  
    }
}