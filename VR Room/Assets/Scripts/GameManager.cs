using UnityEngine;
using System.Collections.Generic;
using Tutorials;
public class GameManager : MonoBehaviour {
    private TutorialManager tutorialManager;

    void Start() {
        tutorialManager = new TutorialManager();
    }

    void Update() {
        tutorialManager.Update();
    }
}