using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private GameObject blue_hearts;
    [SerializeField] private GameObject yellow_hearts;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private Sprite empty_heart;
    [SerializeField] private Image Blue_PBar;
    [SerializeField] private Image Yellow_PBar;
    [SerializeField] private Image Blue_BarBack;
    [SerializeField] private Image Yellow_BarBack;
    [SerializeField] private Text victory_text;
    [SerializeField] private Button restart_Button;
    [SerializeField] private Text time_text;
    [SerializeField] private Image BlueBoots;
    [SerializeField] private Image YellowBoots;
    [SerializeField] private Image BlueGloves;
    [SerializeField] private Image YellowGloves;
    [SerializeField] private Image BlueShield;
    [SerializeField] private Image YellowShield;
    [SerializeField] private Text BlueExtraBallsText;
    [SerializeField] private Text YellowExtraBallsText;
    //[SerializeField] private Obstacle_Generator generator;

    private List<GameObject> blue_hearts_list;
    private List<GameObject> yellow_hearts_list;

    private Animator BlueBootsAnimator;
    private Animator YellowBootsAnimator;
    private Animator BlueGlovesAnimator;
    private Animator YellowGlovesAnimator;
    private Animator BlueShieldAnimator;
    private Animator YellowShieldAnimator;


    void Start()
    {
        Messenger<int>.AddListener(GameEvent.BLUE_HURT, blue_remove_heart);
        Messenger<int>.AddListener(GameEvent.YELLOW_HURT, yellow_remove_heart);
        Messenger<double>.AddListener(GameEvent.BLUE_BAR, Blue_charge);
        Messenger<double>.AddListener(GameEvent.YELLOW_BAR, Yellow_charge);
        Messenger.AddListener(GameEvent.BLUE_DIES, Yellow_wins);
        Messenger.AddListener(GameEvent.YELLOW_DIES, Blue_wins);
        Messenger<string>.AddListener(GameEvent.SPEED_POWERUP_ADD, SpeedPU);
        Messenger<string, float>.AddListener(GameEvent.SPEED_POWERUP_REMOVE, RemoveSpeedPU);
        Messenger<string>.AddListener(GameEvent.RELOAD_POWERUP_ADD, ReloadPU);
        Messenger<string, float>.AddListener(GameEvent.RELOAD_POWERUP_REMOVE, RemoveReloadPU);
        Messenger<string>.AddListener(GameEvent.SHIELD_POWERUP_ADD, ShieldPU);
        Messenger<string, float>.AddListener(GameEvent.SHIELD_POWERUP_REMOVE, RemoveShieldPU);
        Messenger<string>.AddListener(GameEvent.SHIELD_POWERUP_REMOVE_INSTANT, RemoveShieldPUInstant);
        Messenger<string, int>.AddListener(GameEvent.EXTRA_BALL_POWERUP_CHANGE, ExtraBallPU);
        Messenger<int>.AddListener(GameEvent.TIME, time_set);
        Messenger.AddListener(GameEvent.FIVE_SECONDS_LEFT, textAnimation);
        Messenger.AddListener(GameEvent.SUDDEN_DEATH, sudden_death);

        BlueBootsAnimator = BlueBoots.GetComponent<Animator>();
        YellowBootsAnimator = YellowBoots.GetComponent<Animator>();
        BlueGlovesAnimator = BlueGloves.GetComponent<Animator>();
        YellowGlovesAnimator = YellowGloves.GetComponent<Animator>();
        BlueShieldAnimator = BlueShield.GetComponent<Animator>();
        YellowShieldAnimator = YellowShield.GetComponent<Animator>();

        blue_hearts_list = new List<GameObject>();
        foreach (Transform child in blue_hearts.transform)
        {
            blue_hearts_list.Add(child.gameObject);
        }

        yellow_hearts_list = new List<GameObject>();
        foreach (Transform child in yellow_hearts.transform)
        {
            yellow_hearts_list.Add(child.gameObject);
        }
    }

    public void blue_remove_heart(int health)
    {
        blue_hearts_list[health].GetComponent<Image>().sprite = empty_heart;
    }
    public void yellow_remove_heart(int health)
    {
        yellow_hearts_list[health].GetComponent<Image>().sprite = empty_heart;
    }

    public void Blue_charge(double charge)
    {
        Blue_PBar.fillAmount = (float)charge;
    }

    public void Yellow_charge(double charge)
    {
        Yellow_PBar.fillAmount = (float)charge;
    }

    public void Blue_wins()
    {
        victory_text.text = "Blue Wins!";
        restart_Button.gameObject.SetActive(true);
    }

    public void Yellow_wins()
    {
        victory_text.text = "Yellow Wins!";
        restart_Button.gameObject.SetActive(true);
    }

    public void time_set(int t)
    {
        if (t >= 0)
        {
            time_text.text = t.ToString();
        }
    }

    private void textAnimation()
    {
        time_text.GetComponent<Animator>().SetBool("FiveSecondsLeft", true);
    }

    public void sudden_death()
    {
        time_text.GetComponent<Animator>().SetBool("FiveSecondsLeft", false);
        Blue_PBar.color = Color.red;
        Yellow_PBar.color = Color.red;
        Yellow_BarBack.color = Color.red;
        Blue_BarBack.color = Color.red;

        AudioSource.enabled = true;
        Explosion.SetActive(true);
        time_text.text = "SUDDEN DEATH";
    }

    public void restart()
    {
        Messenger.Broadcast(GameEvent.END);
        Messenger<int>.RemoveListener(GameEvent.BLUE_HURT, blue_remove_heart);
        Messenger<int>.RemoveListener(GameEvent.YELLOW_HURT, yellow_remove_heart);
        Messenger<double>.RemoveListener(GameEvent.BLUE_BAR, Blue_charge);
        Messenger<double>.RemoveListener(GameEvent.YELLOW_BAR, Yellow_charge);
        Messenger.RemoveListener(GameEvent.BLUE_DIES, Yellow_wins);
        Messenger.RemoveListener(GameEvent.YELLOW_DIES, Blue_wins);
        Messenger<string>.RemoveListener(GameEvent.SPEED_POWERUP_ADD, SpeedPU);
        Messenger<string, float>.RemoveListener(GameEvent.SPEED_POWERUP_REMOVE, RemoveSpeedPU);
        Messenger<string>.RemoveListener(GameEvent.RELOAD_POWERUP_ADD, ReloadPU);
        Messenger<string, float>.RemoveListener(GameEvent.RELOAD_POWERUP_REMOVE, RemoveReloadPU);
        Messenger<string>.RemoveListener(GameEvent.SHIELD_POWERUP_ADD, ShieldPU);
        Messenger<string, float>.RemoveListener(GameEvent.SHIELD_POWERUP_REMOVE, RemoveShieldPU);
        Messenger<string>.RemoveListener(GameEvent.SHIELD_POWERUP_REMOVE_INSTANT, RemoveShieldPUInstant);
        Messenger<string, int>.RemoveListener(GameEvent.EXTRA_BALL_POWERUP_CHANGE, ExtraBallPU);
        Messenger<int>.RemoveListener(GameEvent.TIME, time_set);
        Messenger.AddListener(GameEvent.SUDDEN_DEATH, sudden_death);
        Messenger.RemoveListener(GameEvent.SUDDEN_DEATH, sudden_death);
        //Messenger<int, int>.RemoveListener(GameEvent.ROW_COL_OC, generator.changeMatBool);
        SceneManager.LoadScene("FirstScene");
    }

    private void SpeedPU(string crab)
    {
        if (crab == CrabType.CRAB_BLUE)
        {
            BlueBootsAnimator.SetBool("Enabled", true);
        } else if (crab == CrabType.CRAB_YELLOW)
        {
            YellowBootsAnimator.SetBool("Enabled", true);
        }
    }

    private void RemoveSpeedPU(string crab, float timeFlicking)
    {
        if (crab == CrabType.CRAB_BLUE)
        {
            StartCoroutine(FlickUIPowerUp(BlueBootsAnimator, timeFlicking));
        }
        else if (crab == CrabType.CRAB_YELLOW)
        {
            StartCoroutine(FlickUIPowerUp(YellowBootsAnimator, timeFlicking));
        }
    }

    private void ReloadPU(string crab)
    {
        if (crab == CrabType.CRAB_BLUE)
        {
            BlueGlovesAnimator.SetBool("Enabled", true);
        }
        else if (crab == CrabType.CRAB_YELLOW)
        {
            YellowGlovesAnimator.SetBool("Enabled", true);
        }
    }

    private void RemoveReloadPU(string crab, float timeFlicking) {
        if (crab == CrabType.CRAB_BLUE)
        {
            StartCoroutine(FlickUIPowerUp(BlueGlovesAnimator, timeFlicking));
        }
        else if (crab == CrabType.CRAB_YELLOW)
        {
            StartCoroutine(FlickUIPowerUp(YellowGlovesAnimator, timeFlicking));
        }
    }

    private void ShieldPU(string crab)
    {
        if (crab == CrabType.CRAB_BLUE)
        {
            BlueShieldAnimator.SetBool("Enabled", true);
        }
        else if (crab == CrabType.CRAB_YELLOW)
        {
            YellowShieldAnimator.SetBool("Enabled", true);
        }
    }

    private void RemoveShieldPU(string crab, float timeFlicking)
    {
        if (crab == CrabType.CRAB_BLUE)
        {
            StartCoroutine(FlickUIPowerUp(BlueShieldAnimator, timeFlicking));
        }
        else if (crab == CrabType.CRAB_YELLOW)
        {
            StartCoroutine(FlickUIPowerUp(YellowShieldAnimator, timeFlicking));
        }
    }

    private void RemoveShieldPUInstant(string crab)
    {
        if (crab == CrabType.CRAB_BLUE)
        {
            BlueShieldAnimator.SetBool("Enabled", false);
            BlueShieldAnimator.SetBool("Flick", false);
        } else if (crab == CrabType.CRAB_YELLOW) {
            YellowShieldAnimator.SetBool("Enabled", false);
            YellowShieldAnimator.SetBool("Flick", false);
        }
    }

    private IEnumerator FlickUIPowerUp(Animator anim, float timeFlicking)
    {
        anim.SetBool("Flick", true);
        anim.SetBool("Enabled", false);

        yield return new WaitForSeconds(timeFlicking);

        if (!anim.GetBool("Enabled"))
        {
            anim.SetBool("Flick", false);
        }
    }

    private void ExtraBallPU(string crab, int numberOfBalls)
    {
        if (crab == CrabType.CRAB_BLUE)
        {
            BlueExtraBallsText.text = numberOfBalls.ToString();
        } else if (crab == CrabType.CRAB_YELLOW)
        {
            YellowExtraBallsText.text = numberOfBalls.ToString();
        }
    }

    private void RemoveExtraBallPU(string crab, int numberOfBalls)
    {
        if (crab == CrabType.CRAB_BLUE)
        {
            BlueExtraBallsText.text = numberOfBalls.ToString();
        }
        else if (crab == CrabType.CRAB_YELLOW)
        {
            YellowExtraBallsText.text = numberOfBalls.ToString();
        }
    }


}
