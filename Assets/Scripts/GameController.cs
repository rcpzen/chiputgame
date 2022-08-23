using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using GoogleMobileAds.Api;

public class GameController : MonoBehaviour
{
  public Camera cam;
    public Text bestScoreText,gameOverBestScoreText,inGameBestScoreText,scoreText;
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
    public float minCreateTime;
    public float maxCreateTime;
  private bool playing;
    public Sprite closedButton,openButton;
    public Button musicButton, soundButton;
    public Button inMusicButton, inSoundButton;
    public AudioSource buttonClickSound, gameOverSound,chickenSound;
    public static int music, sound;

    public string appID = "ca-app-pub-3789690953881934~7586025873";
    public string bannerID = "ca-app-pub-3789690953881934/2333699192";
    public string interstitialID = "ca-app-pub-3789690953881934/7156696758";
    private BannerView bannerADS;
    private InterstitialAd InterstitialADS;
    static int adsCount = 1;
    private bool internetConnection = false;

    private void Update()
    {

    }

    

    void AdsGet()
    {
        bannerADS = new BannerView(appID, AdSize.Banner, AdPosition.Top);
        AdRequest request = new AdRequest.Builder().Build();
        bannerADS.LoadAd(request);


        InterstitialADS = new InterstitialAd(appID);
        AdRequest requestInter = new AdRequest.Builder().Build();
        InterstitialADS.LoadAd(requestInter);
    }
    void bannerShow()
    {
        if (internetConnection)
        {
            bannerADS.Show();
        }
    }

    public void InterstitialShow()
    {
        if (InterstitialADS.IsLoaded())
        {
            InterstitialADS.Show();
        }
    }

    void Start()
  {
        MobileAds.Initialize(initStatus => { });
        AdsGet();

        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }

        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetInt("music", 1);
        }
        if (!PlayerPrefs.HasKey("sound"))
        {
            PlayerPrefs.SetInt("sound", 1);
        }



        music = PlayerPrefs.GetInt("music");
        sound = PlayerPrefs.GetInt("sound");


        if (music == 1)
        {
            musicButton.GetComponent<Image>().sprite = openButton;
            inMusicButton.GetComponent<Image>().sprite = openButton;
        }
        else
        {
            musicButton.GetComponent<Image>().sprite = closedButton;
            inMusicButton.GetComponent<Image>().sprite = closedButton;

        }
        if (sound == 1)
        {
            soundButton.GetComponent<Image>().sprite = openButton;
            inSoundButton.GetComponent<Image>().sprite = openButton;
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = closedButton;
            inSoundButton.GetComponent<Image>().sprite = closedButton;

        }

        bestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
        inGameBestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
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
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            internetConnection = false;
        }
        else
        {
            internetConnection = true;
        }

  }

  public void StartGame()
  {
    bannerShow();
    StartCanvas.SetActive(false);
    GameCanvas.SetActive(true);
    Basket.SetActive(true);
    playing = true;
        if(sound == 1)
        {
            buttonClickSound.Play();
        }
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
      yield return new WaitForSeconds(Random.Range(minCreateTime, maxCreateTime));
    }

  }
  

  public void UpLife()
  {
        Debug.Log(life.ToString());
    if (life == 3)
    {
            return;
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
        if (sound == 1)
        {
            gameOverSound.Play();
        }
        if(int.Parse(scoreText.text) > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", int.Parse(scoreText.text));
        }
    playing = false;
    StopCoroutine(Spawn());
    Basket.SetActive(false);
    GameCanvas.SetActive(false);
    GameOverCanvas.SetActive(true);
        gameOverBestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();

        adsCount++;
        if (internetConnection && adsCount%5==0)
        {

            InterstitialShow();
        }
  }

    public void MusicChange()
    {
        if (musicButton.GetComponent<Image>().sprite == closedButton)
        {
            musicButton.GetComponent<Image>().sprite = openButton;
            inMusicButton.GetComponent<Image>().sprite = openButton;
            PlayerPrefs.SetInt("music", 1);
        }
        else
        {
            musicButton.GetComponent<Image>().sprite = closedButton;
            inMusicButton.GetComponent<Image>().sprite = closedButton;
            PlayerPrefs.SetInt("music", 0);
        }

        music = PlayerPrefs.GetInt("music");

    }
    public void SoundChange()
    {
        if (soundButton.GetComponent<Image>().sprite == closedButton)
        {
            soundButton.GetComponent<Image>().sprite = openButton;
            inSoundButton.GetComponent<Image>().sprite = openButton;
            PlayerPrefs.SetInt("sound", 1);
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = closedButton;
            inSoundButton.GetComponent<Image>().sprite = closedButton;
            PlayerPrefs.SetInt("sound", 0);
        }

        sound = PlayerPrefs.GetInt("sound");
    }
    public IEnumerator chickSound()
    {
        yield return new WaitForSeconds(0.2f);
        if(sound == 1)
        {

            chickenSound.Play();
        }

    }
}


