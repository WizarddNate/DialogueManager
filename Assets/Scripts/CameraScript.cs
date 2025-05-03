using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothCam;
    public Vector3 minValues, maxValues;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       GetComponent<Camera>().orthographicSize = 3.25f; // Size u want to start with
    }

    void Update()
    {
        Follow();
    }

    void Follow()
    {
        //get position of the target (player)
        Vector3 targetPosition = player.position + offset;
        //Debug.Log("Player pos is: " + targetPosition);

        //limit camera to the min and max values
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValues.x, maxValues.x),
            Mathf.Clamp(targetPosition.y, minValues.y, maxValues.y),
            Mathf.Clamp(targetPosition.z, minValues.z, maxValues.z));


        //lock camera to target position without breaking bounds
        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothCam * Time.deltaTime);
        transform.position = smoothPosition;
    }
}
