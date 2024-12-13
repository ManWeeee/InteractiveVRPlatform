using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "ScriptableObject/PartInfo", fileName = "New Part Info", order = 1)]
    public class PartInfo : ScriptableObject
    {
        [SerializeField] private AudioClip m_disassembleSound;
        [SerializeField] private AudioClip m_assembleSound;
        [SerializeField] private Mesh m_partMesh;

        public AudioClip DisassembleAudioClip
        {
            get => m_disassembleSound;
            set => m_disassembleSound = value;
        }
        public AudioClip AssembleAudioClip
        {
            get=> m_assembleSound;
            set => m_assembleSound = value;
        }
        public Mesh PartMesh
        {
            get => m_partMesh;
            private set => m_partMesh = value;
        }
    }
}