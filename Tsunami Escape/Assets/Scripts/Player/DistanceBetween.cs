using TMPro;
using UnityEngine;

public class DistanceBetween : MonoBehaviour
{
    public Transform object1;
    public Transform object2;
    public TextMeshProUGUI text;

    private void Update()
    {
        float distanceY = Mathf.Abs(object1.transform.position.y - object2.transform.position.y);

        int distance = Mathf.RoundToInt(distanceY); 

        string distanceTxt = distance.ToString();
        text.text = distanceTxt; 
    }
}
