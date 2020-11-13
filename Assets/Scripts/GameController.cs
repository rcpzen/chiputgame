using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
  public Camera cam;
  // Start is called before the first frame update
  private float maxWidth;
  public GameObject[] eggs;
  public float timeleft;
  public GameObject GameOverCanvas;
  public GameObject GameCanvas;
  public GameObject StartCanvas;
  public GameObject Basket;
  public GameObject[] lifes;
  public Sprite newEgg;
  public Sprite noEgg;
  public int life;
  private bool playing;
  void Start()
  {
    if (cam == null)
    {
      cam = Camera.main;
    }
    playing = false;
    Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
    Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
    float eggWidth = eggs[0].GetComponent<Renderer>().bounds.extents.x;
    maxWidth = targetWidth.x - eggWidth;
  }
  void FixedUpdate()
  {
    if (playing)
    {
      timeleft -= Time.deltaTime;
      if (timeleft < 0)
      {
        timeleft = 0;
      }
    }
  }

  public void StartGame()
  {
    StartCanvas.SetActive(false);
    GameCanvas.SetActive(true);
    Basket.SetActive(true);
    playing = true;
    StartCoroutine(Spawn());

  }
  private IEnumerator Spawn()
  {
    yield return new WaitForSeconds(2.0f);
    while (playing)
    {

      GameObject egg = eggs[Random.Range(0, eggs.Length)];
      Vector3 spawnPosition = new Vector3(Random.Range(-maxWidth, maxWidth), transform.position.y, 0.0f);
      Quaternion spawnRotation = Quaternion.identity;
      Instantiate(egg, spawnPosition, spawnRotation);
      yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
    }

  }

  public void UpLife()
  {
    if (life > 3)
    {
      Debug.Log("giriyor");
      life = 3;
    }
    else
    {
      life += 1;
      lifes[life - 1].GetComponent<Image>().sprite = newEgg;
      // Debug.Log(life);
      // switch (life)
      // {
      //   case 2:
      //     lifes[0].GetComponent<Image>().sprite = newEgg;
      //     break;
      //   case 1:
      //     lifes[1].GetComponent<Image>().sprite = newEgg;
      //     break;
      //   case 0:
      //     lifes[2].GetComponent<Image>().sprite = newEgg;
      //     break;
      //   default:
      //     break;
      // }
    }
  }
  public void LossLife()
  {
    life -= 1;
    if (life == 0)
    {
      GameOver();
    }
    else
    {
      lifes[life].GetComponent<Image>().sprite = noEgg;

    }
    // switch (life)
    // {
    //   case 2:
    //     lifes[0].GetComponent<Image>().sprite = noEgg;
    //     break;
    //   case 1:
    //     lifes[1].GetComponent<Image>().sprite = noEgg;
    //     break;
    //   case 0:
    //     lifes[2].GetComponent<Image>().sprite = noEgg;
    //     GameOver();
    //     break;
    //   default:
    //     break;
    // }
  }
  public void GameOver()
  {
    playing = false;
    StopCoroutine(Spawn());
    Basket.SetActive(false);
    GameCanvas.SetActive(false);
    GameOverCanvas.SetActive(true);
  }
}


