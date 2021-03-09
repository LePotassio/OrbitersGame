using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float smoothSpeed;

    void FixedUpdate()
    {
        // Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        // transform.position += movement * Time.deltaTime * moveSpeed;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Go to the left relative to rotation
            //Vector3 direction = transform.up;

            //Vector3 desiredPosition = transform.position - direction * moveSpeed;

            //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            //transform.GetComponent<Rigidbody2D>().AddForce(-Vector3.right * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);


            Vector2 addVel = new Vector2(transform.right.x * moveSpeed * Time.fixedDeltaTime, transform.right.y * moveSpeed * Time.fixedDeltaTime);
            transform.GetComponent<Rigidbody2D>().velocity -= addVel;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Vector3 direction = transform.up;

            //Vector3 desiredPosition = transform.position + direction * moveSpeed;

            //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            //transform.GetComponent<Rigidbody2D>().AddForce(Vector3.right * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);


            Vector2 addVel = new Vector2(transform.right.x * moveSpeed * Time.fixedDeltaTime, transform.right.y * moveSpeed * Time.fixedDeltaTime);
            transform.GetComponent<Rigidbody2D>().velocity += addVel;
        }
    }
}
