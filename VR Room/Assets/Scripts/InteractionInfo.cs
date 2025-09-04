using UnityEngine;

public class InteractionInfo : MonoBehaviour
{
    [SerializeField] private HoverMaterials m_materials;

    public HoverMaterials GetHoverMaterials => m_materials;
}
