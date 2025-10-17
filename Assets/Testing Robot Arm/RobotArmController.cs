using UnityEngine;
using UnityEngine.InputSystem;

public class RobotArmController : MonoBehaviour
{
    [Header("Pillar")]
    public Transform pillarJoint;
    public float anglesPillar = 90f;
    [Header("Arm")]
    public Transform armJoint;
    public float anglesArm = 45f;
    [Header("Hand")]
    public Transform handJoint;
    public float anglesHand = 45f;
    [Header("Values")]
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotación Pilar (Eje Y)
        // var keyboard = Keyboard.current;
        // if (keyboard == null) return;

        // if (keyboard.leftArrowKey.isPressed || keyboard.aKey.isPressed)
        // {
        //     RotateJointAndClamp(pillarJoint, Vector3.up, 1, -anglesPillar, anglesPillar);
        // }
        // if (keyboard.rightArrowKey.isPressed || keyboard.dKey.isPressed)
        // {
        //     RotateJointAndClamp(pillarJoint, Vector3.up, -1, -anglesPillar, anglesPillar);
        // }

        // // Rotación brazo (Eje Z)
        // if (keyboard.upArrowKey.isPressed || keyboard.wKey.isPressed)
        // {
        //     RotateJointAndClamp(armJoint, Vector3.forward, 1, -anglesArm, anglesArm);
        // }
        // if (keyboard.downArrowKey.isPressed || keyboard.sKey.isPressed)
        // {
        //     RotateJointAndClamp(armJoint, Vector3.forward, -1, -anglesArm, anglesArm);
        // }

        // // Rotación mano (Eje Z)
        // if (keyboard.qKey.isPressed)
        // {
        //     RotateJointAndClamp(handJoint, Vector3.forward, 1, -anglesHand, anglesHand);
        // }
        // if (keyboard.eKey.isPressed)
        // {
        //     RotateJointAndClamp(handJoint, Vector3.forward, -1, -anglesHand, anglesHand);
        // }
    }

    private void RotateJointAndClamp(Transform joint, Vector3 axis, int direction, float minAngle, float maxAngle)
    {
        float angle = direction * rotationSpeed * Time.deltaTime;

        // Se aplica la rotación
        joint.Rotate(axis, angle, Space.Self);

        // Obtener el angulo de Eular Actual del eje
        Vector3 currentAngles = joint.localEulerAngles;
        float targetAngle = 0f;

        if (axis == Vector3.up)
        {
            targetAngle = currentAngles.y;
        }
        else if (axis == Vector3.forward)
        {
            targetAngle = currentAngles.z;
        }

        // Normalizar el ángulo de 0/360 a -180/180 (por defecto Unity usa 0-360)
        if (targetAngle > 180)
        {
            targetAngle -= 360;
        }

        // Restricciones
        float clampedAngle = Mathf.Clamp(targetAngle, minAngle, maxAngle);

        // Reaplicar el ángulo restringido
        if (axis == Vector3.up)
        {
            joint.localRotation = Quaternion.Euler(currentAngles.x, clampedAngle, currentAngles.z);
        }
        else if (axis == Vector3.forward)
        {
            joint.localRotation = Quaternion.Euler(currentAngles.x, currentAngles.y, clampedAngle);
        }
    }

    public void PanelMove(string action, float speed)
    {
        switch (action)
        {
            case "PillarLeft":
                RotateJointAndClamp(pillarJoint, Vector3.up, 1, -anglesPillar, anglesPillar);
                break;
            case "PillarRight":
                RotateJointAndClamp(pillarJoint, Vector3.up, - 1, -anglesPillar, anglesPillar);
                break;
            case "ArmUp":
                RotateJointAndClamp(armJoint, Vector3.forward, 1, -anglesArm, anglesArm);
                break;
            case "ArmDown":
                RotateJointAndClamp(armJoint, Vector3.forward, -1, -anglesArm, anglesArm);
                break;
            case "HandLeft":
                RotateJointAndClamp(handJoint, Vector3.forward, 1, -anglesHand, anglesHand);
                break;
            case "HandRight":
                RotateJointAndClamp(handJoint, Vector3.forward, -1, -anglesHand, anglesHand);
                break;
            default:
                Debug.LogWarning("Acción de panel no reconocida: " + action);
                break;
        }
    }
}
