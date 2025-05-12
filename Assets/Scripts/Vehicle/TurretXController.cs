using UnityEngine;
using UnityEngine.InputSystem;

public class TurretXController : MonoBehaviour
{
    [SerializeField] private Transform turret;
    [SerializeField] private float rotationSpeed = 5f;

    private BtrInputActions btrInputActions;
    private Vector2 lookInput;

    public void ChangeCameraSensetivity(float sensetivity){
        rotationSpeed = sensetivity;
    }

    private void Awake(){
        btrInputActions = new BtrInputActions();
        turret.localEulerAngles = new Vector3(
             turret.localEulerAngles.x,
            turret.localEulerAngles.y,
            turret.localEulerAngles.z
        );
    }

    private void OnEnable(){
        btrInputActions.Enable();
    }

    private void OnDisable(){
        btrInputActions.Disable();
    }
    
    float NormalizeAngle(float angle){
        if (angle > 180f){
            angle -= 360f;
        }
        return angle;
    }

    private void Update(){
        lookInput = btrInputActions.BTR.Turret_axis.ReadValue<Vector2>();
        float vertical = lookInput.y * rotationSpeed * Time.deltaTime;
        float current_angle = NormalizeAngle(turret.localEulerAngles.x);
        float new_angle = Mathf.Clamp(current_angle - vertical, -42f, 5f);
        
        turret.localEulerAngles = new Vector3(
            new_angle,
            turret.localEulerAngles.y,
            turret.localEulerAngles.z
        );
    }
}
