using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : BossBase
{   
    public override IEnumerator WaveManager()
    {
        while (isWaving)
        {
            isWaving = false;
            phase = Random.Range(0, 2);
            PhaseFunc(phase);
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            isWaving = true;
            yield return new WaitForSeconds(1.2f);
        }
        yield return null;
    }
}
