using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private GameObject blue_hearts;
    [SerializeField] private GameObject yellow_hearts;
    [SerializeField] private Sprite empty_heart;
    [SerializeField] private Image Blue_PBar;
    [SerializeField] private Image Yellow_PBar;
    [SerializeField] private Text victory_text;
    [SerializeField] private Button restart_Button;
    [SerializeField] private Image BlueBoots;
    [SerializeField] private Image YellowBoots;
    [SerializeField] private Image BlueGloves;
    [SerializeField] private Image YellowGloves;
    [SerializeField] private Image BlueArmor;
    [SerializeField] private Image YellowArmor;
    [SerializeField] private Text BlueExtraBallsText;
    [SerializeField] private Text YellowExtraBallsText;
    //[SerializeField] private Obstacle_Generator generator;

    private List<GameObject> blue_hearts_list;
    private List<GameObject> yellow_hearts_list;

    private Animator BlueBootsAnimator;
    private Animator YellowBootsAnimator;
    private Animator BlueGlovesAnimator;
    private Animator YellowGlovesAnimator;
    private Animator BlueArmorAnimator;
    private Animator YellowArmorAnimator;


    void Start()
    {
        Messenger<int>.AddListener(GameEvent.BLUE_HURT, blue_remove_heart);
        Messenger<int>.AddListener(GameEvent.YELLOW_HURT, yellow_remove_heart);
        Messenger<double>.AddListener(GameEvent.BLUE_BAR, Blue_charge);
        Messenger<double>.AddListener(GameEvent.YELLOW_BAR, Yellow_charge);
        Messenger.AddListener(GameEvent.BLUE_DIES, Yellow_wins);
        Messenger.AddListener(GameEvent.YELLOW_DIES, Blue_wins);
        Messenger<string>.AddListener(GameEvent.SPEED_POWERUP_ADD, BootsPU);
        Messenger<string, float>.AddListener(GameEvent.SPEED_POWERUP_REMOVE, RemoveBootsPU);
        Messenger<string>.AddListener(GameEvent.RELOAD_POWERUP_ADD, ReloadPU);
        Messenger<string, float>.AddListener(GameEvent.RELOAD_POWERUP_REMOVE, RemoveReloadPU);
        Messenger<string>.AddListener(GameEvent.SHIELD_POWERUP_ADD, ShieldPU);
        Messenger<string, float>.AddListener(GameEvent.SHIELD_POWERUP_REMOVE, RemoveShieldPU);
        Messenger<string>.AddListener(GameEvent.SHIELD_POWERUP_REMOVE_INSTANT, RemoveShieldPUInstant);
        Messenger<string, int>.AddListener(GameEvent.EXTRA_BALL_POWERUP_CHANGE, ExtraBallPU);

        BlueBootsAnimator = BlueBoots.GetComponent<Animator>();
        YellowBootsAnimator = YellowBoots.GetComponent<Animator>();
        BlueGlovesAnimator = BlueGloves.GetComponent<Animator>();
        YellowGlovesAnimator = YellowGloves.GetComponent<Animator>();
        BlueArmorAnimator = BlueArmor.GetComponent<Animator>();
        YellowArmorAnimator = YellowArmor.GetComponent<Animator>();

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

    public void restart()
    {
        Messenger.Broadcast(GameEvent.END);
        Messenger<int>.RemoveListener(GameEvent.BLUE_HURT, blue_remove_heart);
        Messenger<int>.RemoveListener(GameEvent.YELLOW_HURT, yellow_remove_heart);
        Messenger<double>.RemoveListener(GameEvent.BLUE_BAR, Blue_charge);
        Messenger<double>.RemoveListener(GameEvent.YELLOW_BAR, Yellow_charge);
        Messenger.RemoveListener(GameEvent.BLUE_DIES, Yellow_wins);
        Messenger.RemoveListener(GameEvent.YELLOW_DIES, Blue_wins);
        Messenger<string>.RemoveListener(GameEvent.SPEED_POWERUP_ADD, BootsPU);
        Messenger<string, float>.RemoveListener(GameEvent.SPEED_POWERUP_REMOVE, RemoveBootsPU);
        Messenger<string>.RemoveListener(GameEvent.RELOAD_POWERUP_ADD, ReloadPU);
        Messenger<string, float>.RemoveListener(GameEvent.RELOAD_POWERUP_REMOVE, RemoveReloadPU);
        Messenger<string>.RemoveListener(GameEvent.SHIELD_POWERUP_ADD, ShieldPU);
        Messenger<string, float>.RemoveListener(GameEvent.SHIELD_POWERUP_REMOVE, RemoveShieldPU);
        Messenger<string>.RemoveListener(GameEvent.SHIELD_POWERUP_REMOVE_INSTANT, RemoveShieldPUInstant);
        Messenger<string, int>.AddListener(GameEvent.EXTRA_BALL_POWERUP_CHANGE, ExtraBallPU);
        //Messenger<int, int>.RemoveListener(GameEvent.ROW_COL_OC, generator.changeMatBool);
        SceneManager.LoadScene("FirstScene");
    }

    private void BootsPU(string crab)
    {
        if (crab == CrabType.CRAB_BLUE)
        {
            BlueBootsAnimator.SetBool("Enabled", true);
        } else if (crab == CrabType.CRAB_YELLOW)
        {
            YellowBootsAnimator.SetBool("Enabled", true);
        }
    }

    private void RemoveBootsPU(string crab, float timeFlicking)
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
            BlueArmorAnimator.SetBool("Enabled", true);
        }
        else if (crab == CrabType.CRAB_YELLOW)
        {
            YellowArmorAnimator.SetBool("Enabled", true);
        }
    }

    private void RemoveShieldPU(string crab, float timeFlicking)
    {
        if (crab == CrabType.CRAB_BLUE)
        {
            StartCoroutine(FlickUIPowerUp(BlueArmorAnimator, timeFlicking));
        }
        else if (crab == CrabType.CRAB_YELLOW)
        {
            StartCoroutine(FlickUIPowerUp(YellowArmorAnimator, timeFlicking));
        }
    }

    private void RemoveShieldPUInstant(string crab)
    {
        if (crab == CrabType.CRAB_BLUE)
        {
            BlueArmorAnimator.SetBool("Enabled", false);
            BlueArmorAnimator.SetBool("Flick", false);
        } else if (crab == CrabType.CRAB_YELLOW) {
            YellowArmorAnimator.SetBool("Enabled", false);
            YellowArmorAnimator.SetBool("Flick", false);
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
