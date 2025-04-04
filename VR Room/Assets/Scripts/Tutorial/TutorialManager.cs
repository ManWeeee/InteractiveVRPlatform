using Assets.Scripts;
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
        }
    }
}