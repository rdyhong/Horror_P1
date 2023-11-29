using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESoundTypeFoot
{
    Grass,
    Wood,
    Rock,
}

public class AudioMgr : Singleton<AudioMgr>
{
    

    [SerializeField] AudioClip[] _footGrass;
    [SerializeField] AudioClip[] _footRock;
    [SerializeField] AudioClip[] _footWood;

    public void PlayFootEffect(ESoundTypeFoot type, Vector3 pos)
    {
        AudioEffect eff = ResourcesMgr.Inst.Spawn<AudioEffect>(EResourcePath.Audio);
        eff.transform.position = pos;

        switch (type)
        {
            case ESoundTypeFoot.Wood:
                eff.PlayEffect(_footWood[0]);
                break;
            case ESoundTypeFoot.Grass:
                eff.PlayEffect(_footGrass[0]);
                break;
            case ESoundTypeFoot.Rock:
                eff.PlayEffect(_footRock[0]);
                break;
        }
    }
}
