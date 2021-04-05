using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(KillSelf(lifetime));
    }

    // Update is called once per frame
    
    IEnumerator KillSelf(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
