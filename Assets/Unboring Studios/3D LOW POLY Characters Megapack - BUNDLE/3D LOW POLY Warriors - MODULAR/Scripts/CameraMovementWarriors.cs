using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnboringFuture
{
    namespace CharactersMegapack
    {
        public class CameraMovementWarriors : MonoBehaviour
        {
            public float movementSpeed = 0.1f;
            public float rotationSpeed = 5f;

            private float desiredRotation = 0f;
            private Quaternion newRotation;

            // Start is called before the first frame update
            void Start()
            {
                newRotation = transform.rotation;
            }

            // Update is called once per frame
            void Update()
            {
                float xAxisValue = Input.GetAxis("Horizontal") * movementSpeed;
                float yAxisValue = 0;
                float zAxisValue = Input.GetAxis("Vertical") * movementSpeed;

                if (Input.GetKeyUp(KeyCode.Q))
                {
                    desiredRotation = -45f;
                    newRotation *= Quaternion.Euler(0, desiredRotation, 0);
                }
                else if (Input.GetKeyUp(KeyCode.E))
                {
                    desiredRotation = 45f;
                    newRotation *= Quaternion.Euler(0, desiredRotation, 0);
                }
                else if (Input.GetKey(KeyCode.Space))
                {
                    yAxisValue = 0.5f * movementSpeed;
                }
                else if (Input.GetKey(KeyCode.LeftShift))
                {
                    yAxisValue = -0.5f * movementSpeed;
                }

                transform.Translate(new Vector3(xAxisValue, yAxisValue, zAxisValue));

                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
