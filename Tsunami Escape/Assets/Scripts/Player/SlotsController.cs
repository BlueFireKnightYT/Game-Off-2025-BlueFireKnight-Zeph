using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SlotsController : MonoBehaviour
{
    private Animator anim;
    public GameObject BoostOne;
    public GameObject BoostTwo;
    public GameObject BoostThree;
    public GameObject BoostFour;
    public GameObject BoostFive;
    public GameObject BoostSix;
    private float X;
    private float Y;
    private float Z;
    private float lastX;
    private float lastY;
    private float lastZ;
    private bool TestY;
    private bool TestZ;
    public RectTransform PositionOne;
    public RectTransform PositionTwo;
    public RectTransform PositionThree;
    public PlayerCollision PC;
    public PlayerInputs pi;
    public GameObject LootUI;

    private void Awake()
    {
        Debug.Log("SlotsController Awake running on: " + gameObject.name + ", LootUI = " + LootUI, this);
        anim = GetComponent<Animator>();
    }

    public void ClearAllSlots()
    {
        ClearSlot(PositionOne);
        ClearSlot(PositionTwo);
        ClearSlot(PositionThree);
    }

    private void ClearSlot(RectTransform slot)
    {
        foreach (Transform child in slot)
        {
            Destroy(child.gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartSpin()
    {
        anim.SetBool("Spinning", true);
        X = 0;
        Y = 0;
        Z = 0;
    }

    public void StopSpin()
    {
        anim.SetBool("Spinning", false);
        anim.SetBool("Spinned", true);
    }

    public void UpgradeOne()
    {
        do
        {
            X = Random.Range(1, 7);
        } while (X == lastX || X == lastY || X == lastZ);   
        if (X == 1) Instantiate(BoostOne, PositionOne);
        if (X == 2) Instantiate(BoostTwo, PositionOne);
        if (X == 3) Instantiate(BoostThree, PositionOne);
        if (X == 4) Instantiate(BoostFour, PositionOne);
        if (X == 5) Instantiate(BoostFive, PositionOne);
        if (X == 6) Instantiate(BoostSix, PositionOne);
    }

    public void UpgradeTwo()
    { 
        do
        {
            Y = Random.Range(1, 7);
        } while (X == Y || Y == lastY || Y == lastX || Y == lastZ);
        if (Y == 1) Instantiate(BoostOne, PositionTwo);
        if (Y == 2) Instantiate(BoostTwo, PositionTwo);
        if (Y == 3) Instantiate(BoostThree, PositionTwo);
        if (Y == 4) Instantiate(BoostFour, PositionTwo);
        if (Y == 5) Instantiate(BoostFive, PositionTwo);
        if (Y == 6) Instantiate(BoostSix, PositionTwo);
    }

    public void UpgradeThree()
    {
        do
        {
            Z = Random.Range(1, 7);
        }
        while (X == Z || Y == Z || Z == lastZ || Z == lastX || Z == lastY);
        if (Z == 1) Instantiate(BoostOne, PositionThree);
        if (Z == 2) Instantiate(BoostTwo, PositionThree);
        if (Z == 3) Instantiate(BoostThree, PositionThree);
        if (Z == 4) Instantiate(BoostFour, PositionThree);
        if (Z == 5) Instantiate(BoostFive, PositionThree);
        if (Z == 6) Instantiate(BoostSix, PositionThree);
    }

    public void ClickUpgradeDoubleJump()
    {
        Time.timeScale = 1;
        LootUI = GameObject.Find("ArenaLootMenu");
        pi = GameObject.Find("Player").GetComponent<PlayerInputs>();
        pi.DoubleJump = true;
        Debug.Log("SlotsController Awake running on: " + gameObject.name + ", LootUI = " + LootUI, this);
        LootUI.SetActive(false);
        if(X == 1) lastX = 1;
        if (Y == 1) lastY = 1;
        if (Z == 1) lastZ = 1;
    }

    public void ClickUpgradeGlider()
    {
        Time.timeScale = 1;
        LootUI = GameObject.Find("ArenaLootMenu");
        Debug.Log("SlotsController Awake running on: " + gameObject.name + ", LootUI = " + LootUI, this);
        LootUI.SetActive(false);
        if (X == 2) lastX = 2;
        if (Y == 2) lastY = 2;
        if (Z == 2) lastZ = 2;
    }

    public void ClickUpgradeExtraLife()
    {
        Time.timeScale = 1;
        PC = GameObject.Find("Player").GetComponent<PlayerCollision>();
        LootUI = GameObject.Find("ArenaLootMenu");
        LootUI.SetActive(false);
        PC.HasExtraLife = true;
        if (X == 3) lastX = 3;
        if (Y == 3) lastY = 3;
        if (Z == 3) lastZ = 3;
    }

    public void ClickUpgradePotionStrength()
    {
        Time.timeScale = 1;
        LootUI = GameObject.Find("ArenaLootMenu");
        Debug.Log("SlotsController Awake running on: " + gameObject.name + ", LootUI = " + LootUI, this);
        PC = GameObject.Find("Player").GetComponent<PlayerCollision>();
        PC.SlowTimeAmount = 4;
        PC.AntiGravity = 2;
        LootUI.SetActive(false);
        if (X == 4) lastX = 4;
        if (Y == 4) lastY = 4;
        if (Z == 4) lastZ = 4;

    }

    public void ClickUpgradeSurfboardPlus()
    {
        Time.timeScale = 1;
        LootUI = GameObject.Find("ArenaLootMenu");
        PC = GameObject.Find("Player").GetComponent<PlayerCollision>();
        PC.SurfDuration = 4;
        LootUI.SetActive(false);
        if (X == 5) lastX = 5;
        if (Y == 5) lastY = 5;
        if (Z == 5) lastZ = 5;
    }

    public void ClickUpgradeCoinsPlus()
    {
        Time.timeScale = 1;
        LootUI = GameObject.Find("ArenaLootMenu");
        PC = GameObject.Find("Player").GetComponent<PlayerCollision>();
        Debug.Log("SlotsController Awake running on: " + gameObject.name + ", LootUI = " + LootUI, this);
        PC.BaseCoinValue = 5;
        LootUI.SetActive(false);
        if (X == 6) lastX = 6;
        if (Y == 6) lastY = 6;
        if (Z == 6) lastZ = 6;
    }
}
