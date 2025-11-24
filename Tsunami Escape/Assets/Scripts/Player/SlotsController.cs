using JetBrains.Annotations;
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
    private bool TestY;
    private bool TestZ;
    public RectTransform PositionOne;
    public RectTransform PositionTwo;
    public RectTransform PositionThree;
   

    private void Start()
    {
        PositionOne.localScale.Set(1, 1, 1);
        PositionTwo.localScale.Set(1, 1, 1);
        PositionThree.localScale.Set(1, 1, 1);
        PositionOne.sizeDelta.Set(100, 100);
        PositionTwo.sizeDelta.Set(100, 100);
        PositionThree.sizeDelta.Set(100, 100);
        anim = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartSpin()
    {
        anim.SetBool("Spinning", true);
    }

    public void StopSpin()
    {
        anim.SetBool("Spinning", false);
        anim.SetBool("Spinned", true);
    }

    public void UpgradeOne()
    {
        X = Random.Range(1, 6);
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
            Y = Random.Range(1, 6);
        } while (X == Y);
        if (Y == 1) Instantiate(BoostOne, PositionTwo);
        if (Y == 2) Instantiate(BoostTwo, PositionTwo);
        if (Y == 3) Instantiate(BoostThree, PositionTwo);
        if (Y == 4) Instantiate(BoostFour, PositionTwo);
        if (Y == 5) Instantiate(BoostFive, PositionTwo);
        if (Y == 6) Instantiate(BoostSix, PositionTwo);
        Debug.Log(Y);
    }

    public void UpgradeThree()
    {
        do
        {
            Z = Random.Range(1, 6);
        }
        while (X == Z || Y == Z);
        if (Z == 1) Instantiate(BoostOne, PositionThree);
        if (Z == 2) Instantiate(BoostTwo, PositionThree);
        if (Z == 3) Instantiate(BoostThree, PositionThree);
        if (Z == 4) Instantiate(BoostFour, PositionThree);
        if (Z == 5) Instantiate(BoostFive, PositionThree);
        if (Z == 6) Instantiate(BoostSix, PositionThree);
        Debug.Log(Z);
        
    }
}
