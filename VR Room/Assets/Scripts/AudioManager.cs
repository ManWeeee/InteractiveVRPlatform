using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;


    void Start()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    public static async UniTaskVoid PlaySound(Transform position, AudioClip sound)
    {
        GameObject obj = new GameObject("TempAudio");
        AudioSource source = obj.AddComponent<AudioSource>();
        obj.transform.position = position.position;

        source.PlayOneShot(sound);
        await UniTask.Delay((int)(sound.length * 1000)); // Wait for the clip duration

        Destroy(obj); // Clean up after sound finishes
    }
}
