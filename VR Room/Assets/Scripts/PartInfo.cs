using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "ScriptableObject/PartInfo", fileName = "New Part Info", order = 1)]
    public class PartInfo : ScriptableObject
    {
        [SerializeField] private AudioClip m_disassembleSound;
        [SerializeField] private AudioClip m_assembleSound;

        public AudioClip DisassembleAudioClip => m_disassembleSound;
        public AudioClip AssembleAudioClip => m_assembleSound;
    }
}