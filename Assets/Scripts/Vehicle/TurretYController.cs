using UnityEngine;
using UnityEngine.InputSystem;

public class TurretYController : MonoBehaviour
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
    }

    private void OnEnable(){
        btrInputActions.Enable();
    }

    private void OnDisable(){
        btrInputActions.Disable();
    }

    private void Update(){
        lookInput = btrInputActions.BTR.Turret_axis.ReadValue<Vector2>();
        float horizontal = lookInput.x;
        turret.Rotate(0f, horizontal * rotationSpeed * Time.deltaTime, 0f);
    }
}
