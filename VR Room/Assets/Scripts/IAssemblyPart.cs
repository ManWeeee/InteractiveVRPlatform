using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IAssemblyPart
    {
        public void StartAssemble();

        public void StartDisassemble();
    }
}