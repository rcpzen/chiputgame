using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
  // Start is called before the first frame update
  public Text scoreText;
  public Text endGameScore;
  public int eggValue;
  private int score;

  public GameController gameController;
  public GameObject Explode;
  void Start()
  {
    score = 0;
    UpdateScore();
  }


  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag == "Bomb")
    {
      var bomb = Instantiate(Explode, transform.position, transform.rotation);
      Destroy(bomb, 2.0f);
      if (score == 0)
      {
        score = 0;
        UpdateScore();
      }
      else
      {
        score -= eggValue;
        UpdateScore();
      }
    }
    else if (other.gameObject.tag == "GoldenEgg")
    {
      gameController.UpLife();
    }
    else
    {
      score += eggValue;
      UpdateScore();
    }

  }

  void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Bomb")
    {
      if (score == 0)
      {
        score = 0;
        UpdateScore();
      }
      else
      {
        score -= eggValue;
        UpdateScore();
      }
    }
   
  }
  void UpdateScore()
  {
    scoreText.text = score.ToString();
    endGameScore.text = score.ToString();
  }
}
