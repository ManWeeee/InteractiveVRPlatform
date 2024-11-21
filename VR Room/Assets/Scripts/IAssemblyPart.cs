using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IAssemblyPart
    {
        public Task StartAssemble();

        public Task StartDisassemble();
    }
}