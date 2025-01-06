using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class MenuUi : UiInstance
{
    [SerializeField] private Button m_undoButton;
    [SerializeField] private Button m_redoButton;
    private CommandHandler m_handler;
    private void Start()
    {
        m_handler = Container.GetInstance<CommandHandler>();
        m_undoButton.onClick.AddListener(m_handler.UndoCommand);
        m_redoButton.onClick.AddListener(m_handler.RedoCommand);
    }
}
