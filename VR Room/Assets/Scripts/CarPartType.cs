using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    [System.Flags]
    public enum CarPartType
    {
        None,
        Wheel,
        Brake,
        Battery,
        //All = Wheel | Brake | Battery
    }

    public static class CarPartTypeExtension 
    {
        public static CarPartType GetAllPartTypeExceptNone()
        {
            return Enum.GetValues(typeof(CarPartType)).Cast<CarPartType>().Where(option => option != CarPartType.None).Aggregate((current, next) => current | next);
        }
    }

}