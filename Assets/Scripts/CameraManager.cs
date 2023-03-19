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
        public Transform cameraTransform; //The transform of the actual camera object in the scene
        public LayerMask collisionLayers;
        private float defaultPosition;
        private Vector3 cameraFollowVelocity = Vector3.zero;
        private Vector3 cameraVectorPosition;
        public float cameraCollisionOffset; //How much the camera will jump away of object its colliding with
        public float minimumCollisionOffset;
        public float cameraCollisionRadius;
        public float cameraFollowSpeed = 0.2f;
        float cameraLookSpeed = 0.65f;
        float cameraPivotSpeed = 0.65f;

        public float lookAngle; //camera looking up and down
        public float pivotAngle; //camera looking left and right

        float minimumPivotAngle = -15f;
        float maximumPivotAngle = 35f;

        private void Awake()
        {
            targetTransform = FindObjectOfType<PlayerManager>().transform;
            inputManager = FindObjectOfType<InputManager>();
            cameraTransform = Camera.main.transform;
            defaultPosition = cameraTransform.localPosition.z;
        }

        public void HandleAllCameraMovement()
        {
            FollowTarget();
            RotateCamera();
            HandleCameraCollisions();
        }

        private void FollowTarget()
        {
            Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
            transform.position = targetPosition;
        }

        private void RotateCamera()
        {
            Vector3 rotation;
            Quaternion targetRotation;

            lookAngle = lookAngle + (inputManager.cameraHorizontalInput * cameraLookSpeed);
            pivotAngle = pivotAngle - (inputManager.cameraVerticalInput * cameraPivotSpeed);
            //Manage the limit to rotation on vertical axis (pivotAngle)
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

            rotation = Vector3.zero;
            rotation.y = lookAngle;
            targetRotation = Quaternion.Euler(rotation);
            transform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;
            targetRotation = Quaternion.Euler(rotation);
            cameraPivot.localRotation = targetRotation;
        }

        private void HandleCameraCollisions()
        {
            float targetPosition = defaultPosition;
            RaycastHit hit;
            Vector3 direction = cameraTransform.position - cameraPivot.position;
            direction.Normalize();

            if(Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
            {
                float distance = Vector3.Distance(cameraPivot.position, hit.point);
                targetPosition =- (distance - cameraCollisionOffset);
            }
            if(Mathf.Abs(targetPosition) < minimumCollisionOffset)
            {
                targetPosition =- minimumCollisionOffset;
            }

            cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.01f);
            cameraTransform.localPosition = cameraVectorPosition;
        }
    }
}
