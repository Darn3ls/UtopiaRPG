using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utopia
{
    public class CameraManager : MonoBehaviour
    {
        InputManager inputManager;
        public Transform targetTransform; //The object the camera will follow
        public Transform cameraPivot; //The object the camera uses to pivot (look up and down)
        private Vector3 cameraFollowVelocity = Vector3.zero;
        public float cameraFollowSpeed = 0.2f;
        float cameraLookSpeed = 1;
        float cameraPivotSpeed = 1;

        public float lookAngle; //camera looking up and down
        public float pivotAngle; //camera looking left and right

        float minimumPivotAngle = -35f;
        float maximumPivotAngle = 35f;

        private void Awake()
        {
            targetTransform = FindObjectOfType<PlayerManager>().transform;
            inputManager = FindObjectOfType<InputManager>();
        }

        public void HandleAllCameraMovement()
        {
            FollowTarget();
            RotateCamera();
        }

        private void FollowTarget()
        {
            Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
            transform.position = targetPosition;
        }

        private void RotateCamera()
        {
            lookAngle = lookAngle + (inputManager.cameraHorizontalInput * cameraLookSpeed);
            pivotAngle = pivotAngle - (inputManager.cameraVerticalInput * cameraPivotSpeed);
            //Manage the limit to rotation on vertical axis (pivotAngle)
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

            Vector3 rotation = Vector3.zero;
            rotation.y = lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            transform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;
            targetRotation = Quaternion.Euler(rotation);
            cameraPivot.localRotation = targetRotation;
        }
    }
}
