using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RifleStateManager : MonoBehaviour
{
    public bool infiniteAmmo;
    //States
    RifleBaseState currentState;
    public RifleActiveState rifleActive = new RifleActiveState();
    public RifleReloadState rifleReload = new RifleReloadState();
    [Header("Audio")]
    public AudioManager audioSystem;
    [Header("Scripts")]
    public EffectOnHit Effect;
    [Header("PlayerState Scriptable Object")]
    public PlayerState ps;
    [Header("Recoil")]
    public int rifleGunShotAmmount;
    public float spreadFactor;    
    public float timePressed;
    public float cameraResetTime;
    public float recoilReset;      
    [Header("Animation")]
    public float reloadTime;
    public GameObject RifleReload;
    public GameObject RifleShoot;
    public GameObject RifleMag;    
    public Transform MainCamera;
    [Header("UI")]
    public TextMeshProUGUI AmmoCounterRifle;
    public GameObject crosshair;
    // Start is called before the first frame update
    void Start(){
        audioSystem = FindObjectOfType<AudioManager>();
        Effect = GetComponent<EffectOnHit>();
        AmmoCounterRifle.text = ps.magAmmo.ToString();
        currentState = rifleActive;
        currentState.EnterState(this);
        infiniteAmmo = false;
        rifleGunShotAmmount = 0;
        spreadFactor = ps.spreadFactor;
        timePressed = 0f;
        cameraResetTime = 15f;
        recoilReset = 8f;        
        reloadTime = 0f;
    }
    void Update(){
        if (ps.parry)
        {
            RifleReload.transform.localPosition = new Vector3(-0.15f, 0f, 0f);
            RifleReload.transform.Rotate(new Vector3(0f, -6f, 0f));
        }
        currentState.UpdateState(this);        
    }
    public void SwitchState(RifleBaseState rifle){
        currentState = rifle;
        rifle.EnterState(this);
    }    
    public void ReCalibrateSpreadFactor() { 
        spreadFactor = ps.spreadFactor;
    }
    public void ReCalibrateMoveSpeed() {
        ps.adsMoveSpeed = ps.normalMoveSpeed / 1.5f;
    }
}
