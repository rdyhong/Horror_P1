using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour, IPoolObject
{
    [SerializeField] AudioSource _audioSource;

    public void PlayEffect(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
        Invoke(nameof(Recycle), clip.length);
    }

    public void Spawn()
    {
    }
    public void Recycle()
    {
        ResourcesMgr.Inst.Recycle(this.gameObject);
    }

}
