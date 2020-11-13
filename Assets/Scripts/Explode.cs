using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public GameObject Explosion;
    public ParticleSystem []Effects;
    public float lifeTime;
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Basket"){
           ActiveExplode();
        }
    }

    public void ActiveExplode(){
        var Ex = Instantiate(Explosion,transform.position,transform.rotation);
            foreach (var effect in Effects)
            {
                effect.transform.parent = null;
                effect.Stop();
                Destroy(effect.gameObject,1.0f);
            }
            Destroy(gameObject);
            Destroy(Ex,lifeTime);
    }


}
