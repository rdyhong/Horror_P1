using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public enum ESoundTypeFoot
{
    Grass,
    Wood,
    Rock,
}
public enum ESoundType
{
    Door_Open,
    Door_Close,

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

    public void PlayEffect(Vector3 pos)
    {
        AudioEffect eff = ResourcesMgr.Inst.Spawn<AudioEffect>(EResourcePath.Audio);
        eff.transform.position = pos;
    }
}
