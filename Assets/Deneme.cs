using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deneme : MonoBehaviour
{

    public const float pi = 3.1415f;
    public GameObject road;
    private GameManager gameManager;
    public Transform target;
    public float speed = 3f;
    public Transform paths;

    //private Tween tween;
    private float defaultSpeed = 0;
    private float speedTemp = 0;
    public List<Vector3> vectorList = new List<Vector3>();
    private bool slowed;
    private Vector3 lastPosition = new Vector3(0,0,0);
    private Vector3 rotationAngel = new Vector3(0,0,0);
    private Vector3 targetDirection;
    private Vector3 zero = new Vector3(0,0,0);
    private float timeUntilRotate = 0;


    public float moveSpeed = 5f;

    void Update()
    {
        timeUntilRotate += Time.deltaTime;
        // Get the input from the WASD keys (or arrow keys)
        float moveX = Input.GetAxis("Horizontal");  // A and D keys (Left and Right)
        float moveZ = Input.GetAxis("Vertical");    // W and S keys (Up and Down)

        // Create a movement vector
        Vector3 move = new Vector3(moveX, moveZ, 0);

        // Move the object by the move vector, adjusted by moveSpeed and deltaTime
        transform.Translate(move * moveSpeed * Time.deltaTime);

        if(timeUntilRotate >= 0.0033)
            { 
                
                //rotationAngel = new UnityEngine.Vector3(0,0, Mathf.Atan(GetTan(transform.position, lastPosition))*180/pi);
                // Debug.Log(rotationAngel);
                //transform.localRotation *= new Quaternion(0,0, rotationAngel.z, 1f);
                //new Vector3(0,0,Vector3.Angle(transform.position, lastPosition)* 10f)
                //Debug.Log(Vector3.Angle(new Vector3(25,25,25), new Vector3(25,50,50)));

                // Determine which direction to rotate towards
                rotationAngel = transform.position - lastPosition;
                targetDirection = new Vector3(0,0,Mathf.Atan2(rotationAngel.y, rotationAngel.x));

                //targetDirection = lastPosition - transform.position;

                // The step size is equal to speed times frame time.
                //float singleStep = speed * Time.deltaTime;

                // Rotate the forward vector towards the target direction by one step
                //Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection.normalized, 360, 0.0f);
                transform.rotation.eulerAngles.Set(0,0,targetDirection.z) ;

                // Draw a ray pointing at our target in
                //Debug.DrawRay(transform.position, newDirection, Color.red);

                // Calculate a rotation a step closer to the target and applies rotation to this object
                //Debug.Log(newDirection);
                Debug.Log(transform.rotation.eulerAngles);
                //transform.rotation = Quaternion.LookRotation(newDirection);

                lastPosition = transform.position;
                timeUntilRotate = 0;
                
            
        }
    }
}
