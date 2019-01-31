using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> effects;

    
    public void GenerateEffect(string effectName,Vector3 eulerRotation,Transform parent)
    {
        GameObject effect = effects.Find(x => x.name.ToLower().Equals(effectName.ToLower()));

        if(effect)
        {
            GameObject eff = Instantiate(effect) as GameObject;
            eff.transform.parent = parent;
            eff.transform.localPosition = new Vector3(0, 0, 0);
            eff.transform.localRotation = Quaternion.Euler(eulerRotation);
            Destroy(eff, eff.GetComponentInChildren<ParticleSystem>().main.startLifetime.constant + 2f);
        }
    }
    
}
