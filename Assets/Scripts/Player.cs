using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Player : MonoBehaviour
{
    [Header("Changeable Variables")]
    
    [SerializeField] private float speed = 50f;
    [SerializeField] private float rollAmount = 10f;
    [SerializeField] private float rollAmountVert = 10f;

    private PlayerInput playerControls;
    private Vector3 movement;
    private float moveHorizontal, moveVertical;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
        playerControls = new PlayerInput();
    }

    private void Start()
    {
        rb.velocity = Vector3.zero;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
        Move();
    }

    private void FixedUpdate()
    {
        
    }

    private void PlayerInput()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        movement = new Vector3(moveHorizontal, 0f , moveVertical);       

    }

    private void Move()
    {
        //rb.MovePosition(movement * (speed * Time.deltaTime));
        //rb.velocity = -movement * speed;
        transform.Translate((-movement * speed * Time.deltaTime) , Space.World);

        float tilt = moveHorizontal * -rollAmount;
        float tilt2 = moveVertical * -rollAmountVert;

        rb.rotation = Quaternion.Euler(tilt, 90.0f, tilt2);

        Debug.Log($"Velocity = {movement.x} , {movement.y} , {movement.z} ");
    }

}
