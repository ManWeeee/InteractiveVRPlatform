using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class CommandHandler : MonoBehaviour
    {
        private static CommandHandler m_instance;
        private Stack<ICommand> m_undoCommands = new Stack<ICommand>();
        private Stack<ICommand> m_redoCommands = new Stack<ICommand>();

        public static CommandHandler Instance => m_instance;

        public void Start()
        {
            if(m_instance)
                return;
            m_instance = this;
        } 

        public void ExecuteCommand(ICommand command)
        {
            m_undoCommands.Push(command);
            command.Execute();
        }

        public void UndoCommand()
        {
            if (m_redoCommands.Count < 0)
            {
                return;
            }

            var command = m_redoCommands.Pop();
            m_undoCommands.Push(command);
            command.Undo();
        }

        public void RedoCommand()
        {
            if (m_undoCommands.Count < 0)
            {
                return;
            }

            var command = m_undoCommands.Pop();
            m_redoCommands.Push(command);
            command.Execute();
        }
    }
}