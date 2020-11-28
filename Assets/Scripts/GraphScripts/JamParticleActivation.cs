using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamParticleActivation : MonoBehaviour
{
    public ParticleSystem teslaCoil;
    public ParticleSystem bolt;
    public ParticleSystem boltBlast;
    public ParticleSystem electricBlastFlash;
    public ParticleSystem jammedRoom;

    bool jammed;

    private void OnEnable()
    {
        teslaCoil.Play();
        bolt.Play();
        StartCoroutine(WaitForBlast()); //Je dois utiliser ça parce que Input marche pas xD
    }

    IEnumerator WaitForBlast()
    {
        yield return new WaitForSeconds(5);
        Blast();
    }

    // Placeholder logic:
    private void Blast()
    {
        teslaCoil.Stop();
        bolt.Stop();

        boltBlast.Play();
        electricBlastFlash.Play();
        jammedRoom.Play();

        jammed = true;
    }
}
