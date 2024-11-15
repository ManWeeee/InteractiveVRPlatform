using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public interface ICommand
    {
        public void Execute();
        public void Undo();
    }
}