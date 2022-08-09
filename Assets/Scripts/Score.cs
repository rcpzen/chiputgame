using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
  // Start is called before the first frame update
  public Text scoreText;
  public Text endGameScore;
    public Text inGameBest;
  public int eggValue;
  private int score;
    public float speedValue;
    public AudioSource basketSound,pointSound,bombSound;

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
            if (GameController.sound == 1)
            {
                bombSound.Play();
            }

        }
    else if (other.gameObject.tag == "GoldenEgg")
    {
      gameController.UpLife();
            if (GameController.sound == 1)
            {
                pointSound.Play();
            }

        }
        else if (other.gameObject.tag == "twoPoint")
        {
            if(gameController.minCreateTime > 0)
            {
                gameController.minCreateTime -= speedValue;
            }
            if(gameController.maxCreateTime > 0.2f)
            {
                gameController.maxCreateTime -= speedValue;

            }

            score += 2;
            UpdateScore();
            if (GameController.sound == 1)
            {
                pointSound.Play();
            }
        }
    else
        {
            if (gameController.minCreateTime > 0)
            {
                gameController.minCreateTime -= speedValue;
            }
            if (gameController.maxCreateTime > 0.2f)
            {
                gameController.maxCreateTime -= speedValue;

            }
            score += eggValue;
            if (GameController.sound == 1)
            {
                basketSound.Play();
            }
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
            if (GameController.sound == 1)
            {
                bombSound.Play();
            }
        }
   
  }
  void UpdateScore()
  {
    scoreText.text = score.ToString();
    endGameScore.text = score.ToString();
        
      
       
    }
}
