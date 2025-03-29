using System;
using UnityEngine;

namespace Gameplay.Core
{
    public class Vehicle : MonoBehaviour
    {
        public CarConfiguration CarConfiguration;
        
        private float horizontalInput, verticalInput;
        private float currentSteerAngle, currentbreakForce;
        private bool isBreaking;

        // Settings
        [SerializeField] private float motorForce, breakForce, maxSteerAngle;

        // Wheel Colliders
        [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
        [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

        // Wheels
        [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
        [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

        public float Time;
        public float Speed;

        private void FixedUpdate()
        {
            GetInput();
            HandleMotor();
            HandleSteering();
            UpdateWheels();
        }

        private void Update()
        {
            if (verticalInput > 0)
            {
                Time += UnityEngine.Time.deltaTime;
            }
            else
            {
                Time -= UnityEngine.Time.deltaTime;
            }
            
            Time = Mathf.Clamp(Time, 0, 1);
            
            Speed = CarConfiguration.AccelerationCurve.Evaluate(Time) * motorForce;
        }

        private void GetInput()
        {
            // Steering Input
            horizontalInput = UnityEngine.Input.GetAxis("Horizontal");

            // Acceleration Input
            verticalInput = UnityEngine.Input.GetAxis("Vertical");

            // Breaking Input
            isBreaking = UnityEngine.Input.GetKey(KeyCode.Space);
        }

        private void HandleMotor()
        {
            switch (CarConfiguration.DriveType)
            {
                case CarDriveType.Front:
                    frontLeftWheelCollider.motorTorque = verticalInput * Speed;
                    frontRightWheelCollider.motorTorque = verticalInput * Speed;
                    break;
                case CarDriveType.Rear:
                    rearLeftWheelCollider.motorTorque = verticalInput * Speed;
                    rearRightWheelCollider.motorTorque = verticalInput * Speed;
                    break;
                case CarDriveType.Both:
                    frontLeftWheelCollider.motorTorque = verticalInput * Speed;
                    frontRightWheelCollider.motorTorque = verticalInput * Speed;
                    rearLeftWheelCollider.motorTorque = verticalInput * Speed;
                    rearRightWheelCollider.motorTorque = verticalInput * Speed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            currentbreakForce = isBreaking ? breakForce : 0f;
            ApplyBreaking();
        }

        private void ApplyBreaking()
        {
            frontRightWheelCollider.brakeTorque = currentbreakForce;
            frontLeftWheelCollider.brakeTorque = currentbreakForce;
            rearLeftWheelCollider.brakeTorque = currentbreakForce;
            rearRightWheelCollider.brakeTorque = currentbreakForce;
        }

        private void HandleSteering()
        {
            currentSteerAngle = maxSteerAngle * horizontalInput;
            frontLeftWheelCollider.steerAngle = currentSteerAngle;
            frontRightWheelCollider.steerAngle = currentSteerAngle;
        }

        private void UpdateWheels()
        {
            UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
            UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
            UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
            UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        }

        private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
        {
            wheelCollider.GetWorldPose(out var pos, out var rot);
            wheelTransform.rotation = rot;
            wheelTransform.position = pos;
        }
    }
}