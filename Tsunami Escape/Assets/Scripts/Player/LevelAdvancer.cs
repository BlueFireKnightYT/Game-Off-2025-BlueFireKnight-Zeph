using UnityEngine;

public class LevelAdvancer : MonoBehaviour
{
    [SerializeField] private Transform Player;
    void Update()
    {
        transform.position = new Vector2(0, Player.transform.position.y);
    }
}
