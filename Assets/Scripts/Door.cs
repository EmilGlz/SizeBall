using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform doorMiddle;
    private SphereCollider sphereCollider;
    [SerializeField] Animator anim;
    private bool isOpened = false;
    private float Radius => GameController.Instance.DoorWinDistance;
    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    private void Start()
    {
        sphereCollider.radius = Radius;
    }
    public void OpenAnimation()
    {
        if (isOpened)
            return;
        isOpened = true;
        anim.SetTrigger("Open");
    }
}
