using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EggDetector : MonoBehaviour
{
  // Start is called before the first frame update

  public Animator animator;
  public GameObject explosion;
  public GameController gameController;
    public AudioSource eggBroke,bombSound;

  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Egg")
    {
      gameController.LossLife();
      other.gameObject.GetComponent<Animator>().enabled = true;
      other.gameObject.GetComponent<Rigidbody2D>().gravityScale = -0.1f;
      other.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
      Vector3 newPos = new Vector3(Random.Range(-5.0f, 5.0f), 5.0f, 0);
      other.gameObject.transform.position = Vector3.Lerp(other.gameObject.transform.position, newPos, Time.deltaTime * 2.0f);
      other.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            if (GameController.sound == 1)
            {
                eggBroke.Play();
                StartCoroutine(gameController.chickSound());
            }
            Destroy(other.gameObject, 4.0f);
    }
    else if (other.gameObject.tag == "GoldenEgg")
    {
            Destroy(other.gameObject);
    }
    else if (other.gameObject.tag == "twoPoint")
    {
            Destroy(other.gameObject);

        }
        else
    {
            if (GameController.sound == 1)
            {
                bombSound.Play();
            }
            var ex = Instantiate(explosion, other.gameObject.transform.position, other.gameObject.transform.rotation);
      Destroy(ex, 2.0f);
      Destroy(other.gameObject);
    }
  }

}
