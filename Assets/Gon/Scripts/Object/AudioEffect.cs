using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : PoolObject
{
    [SerializeField] AudioSource _audioSource;

    public void PlayEffect(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
        Invoke(nameof(Recycle), clip.length);
    }

    public override void Recycle()
    {
        ResourcesMgr.Inst.Recycle(this.gameObject);
    }
}
