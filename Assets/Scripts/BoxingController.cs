using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingController : MonoBehaviour
{
    [SerializeField] ArduinoManager arduino;

    [Header("Boxing Attribute")]
    [SerializeField] GameObject boxingGloves;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float targetDodge;
    [SerializeField] float targetRotate;
    public Animator gloveAnimation;

    [Header ("Bool Condition")]
    [SerializeField] bool isLeft;
    [SerializeField] bool isRight;
    [SerializeField] bool isPunch;

    // Start is called before the first frame update
    void Start()
    {
        gloveAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (arduino.data.x == 1)
            isRight = true;
        else if (arduino.data.x == -1)
            isLeft = true;
        else
        {
            isLeft = false;
            isRight = false;
        }

        if (isRight)
        {
            boxingGloves.transform.localRotation = Quaternion.Euler(boxingGloves.transform.rotation.x, boxingGloves.transform.rotation.y, -targetRotate);
            boxingGloves.transform.position = Vector3.MoveTowards(boxingGloves.transform.position,
                                                                  new Vector3(targetDodge, boxingGloves.transform.position.y, boxingGloves.transform.position.z),
                                                                  moveSpeed * Time.deltaTime);
        }
        else if (isLeft)
        {
            boxingGloves.transform.localRotation = Quaternion.Euler(boxingGloves.transform.rotation.x, boxingGloves.transform.rotation.y, targetRotate);
            boxingGloves.transform.position = Vector3.MoveTowards(boxingGloves.transform.position,
                                                                  new Vector3(-targetDodge, boxingGloves.transform.position.y, boxingGloves.transform.position.z),
                                                                  moveSpeed * Time.deltaTime);
        }
        else
        {
            boxingGloves.transform.localRotation = Quaternion.Euler(boxingGloves.transform.rotation.x, boxingGloves.transform.rotation.y, 0);
            boxingGloves.transform.position = Vector3.MoveTowards(boxingGloves.transform.position,
                                                                  new Vector3(0, boxingGloves.transform.position.y, boxingGloves.transform.position.z),
                                                                  moveSpeed * Time.deltaTime);
        }

        if (arduino.data.y == 1)
            isPunch = true;
        else
            isPunch = false;

        gloveAnimation.SetBool("isPunch", isPunch);
    }
}
