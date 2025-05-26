using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour
{
    public Camera firstPersonCam;
    public Camera thirdPersonCam;

    public TurretYController turretY;
    public TurretXController turretX;

    public float sensFirstPerson = 5f;
    public float sensThirdPerson = 5f;

    private bool isFirstPerson = true;
    private BtrInputActions btrInputActions;

    void Awake(){
        btrInputActions = new BtrInputActions();
        btrInputActions.BTR.Switch_camera.performed += OnSwitchView;
        turretY.ChangeCameraSensetivity(sensFirstPerson);
        turretX.ChangeCameraSensetivity(sensFirstPerson);
    }

    // Update is called once per frame
    void OnEnable(){
        btrInputActions.Enable();
    }
    void OnDisable(){
        btrInputActions.Disable();
    }
    private void OnSwitchView(InputAction.CallbackContext context){
        isFirstPerson = !isFirstPerson;
        SetCameraState(isFirstPerson);
    }
    void SetCameraState(bool firstperson){
        firstPersonCam.enabled = firstperson;
        firstPersonCam.GetComponent<AudioListener>().enabled = firstperson;

        thirdPersonCam.enabled = !firstperson;
        thirdPersonCam.GetComponent<AudioListener>().enabled = !firstperson;
        float newSensitivity = firstperson ? sensFirstPerson : sensThirdPerson;
        turretY.ChangeCameraSensetivity(newSensitivity);
        turretX.ChangeCameraSensetivity(newSensitivity);
    }
}
