using SceneManagement;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private SceneLoader m_loader;
    public override void InstallBindings()
    {
        
        if (m_loader == null)
        {
            Debug.LogError("SceneLoader is not assigned to BootstrapInstaller");
        }
        else
        {
            Debug.Log("SceneLoader is assigned: " + m_loader.name);
        }
        Debug.Log("Installing bindings in BootstrapInstaller");
        Container.BindInstance(m_loader);
    }
}