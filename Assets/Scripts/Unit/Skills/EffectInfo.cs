using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectInfo 
{
    private int duration = 0;

    public int DURATION
    {
        get { return duration; }
        set { duration = value; }
    }

    private int mod = 0;
    public int MOD
    {
        get { return mod; }
    }
    private EStatToEffect stat = EStatToEffect.NOTSET;
    public EStatToEffect STAT
    {
        get { return stat; }
    }

    public EffectInfo(int dur, int mod , EStatToEffect statToEffect) 
    {
        this.duration = dur;
        
        this.mod = mod;
        this.stat = statToEffect;
    }

}
