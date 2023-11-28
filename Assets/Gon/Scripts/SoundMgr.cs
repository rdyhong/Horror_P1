using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : Singleton<SoundMgr>
{
    public enum ESoundTypeFoot
    {
        Grass,
        Wood,
        Rock,

    }

    [SerializeField] AudioClip[] _footGrass;
    [SerializeField] AudioClip[] _footRock;
    [SerializeField] AudioClip[] _footWood;

    public void PlayFootEffect(ESoundTypeFoot type, Vector3 pos)
    {
        type = ESoundTypeFoot.Wood;
        AudioEffect eff = ResourcesMgr.Inst.Spawn<AudioEffect>(EResourcePath.Audio);
        eff.transform.position = pos;

        switch (type)
        {
            case ESoundTypeFoot.Wood:
                eff.PlayEffect(_footWood[0]);
                break;
            case ESoundTypeFoot.Grass:
                break;
            case ESoundTypeFoot.Rock:
                break;
        }
    }
}
