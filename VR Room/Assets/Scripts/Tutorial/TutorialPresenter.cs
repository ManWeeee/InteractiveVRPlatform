using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Tutorials {
    public class TutorialPresenter : UiInstance {
        [SerializeField]
        private TutorialManager m_tutorialManager;
        [SerializeField]
        private TutorialStepGroup m_tutorialStep;
        [SerializeField]
        private TextMeshProUGUI m_tutorialText;

        private void Start() {
            if(Container.TryGetInstance<TutorialManager>(out m_tutorialManager)) {
                m_tutorialManager.TutorialStepChanged += OnTutorialStepChanged;
                OnTutorialStepChanged();
            }
            else {
                Debug.LogError($"Unable to get instance of type {m_tutorialManager.GetType()} in {this.GetType()}");
            }
        }

        public void Update() {
            UpdateUi();
        }

        public override void UpdateUi() {
            if(m_tutorialStep == null) {
                return;
            }
            m_tutorialText.text = m_tutorialStep.ToString();
        }

        private void OnTutorialStepChanged() {
            m_tutorialStep = m_tutorialManager.CurrentStep;
        }
    }
}
