using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DebugUI : MonoBehaviour
{
    [Header("State Of Player")]
    public PlayerState ps;
    public Text PlayerMovementSpeed;
    public Text PlayerDamage;
    public Text NumberOfJumps;
    public Text NumberOfDashes;
    public Text MaxAmmo;
    public Text FireRate;
    public Text ReloadTime;
    public Text SpreadFactor;
    public Text RecoilAmount;
    public Text InvOnDash;
    public Text DashIsParry;
    public Text AmmoOnParry;
    public Text SlowOnParry;
    private void Update() { 
        StateOfPlayer();
    }
    void StateOfPlayer()
    {
        PlayerMovementSpeed.text = "Move: " + ps.moveSpeed.ToString("0.00");
        PlayerDamage.text = "DMG: " + ps.damage.ToString();
        NumberOfJumps.text = "Jumps: " + (ps.numOfExtraJump + 1).ToString();
        NumberOfDashes.text = "Dash: " + ps.numOfDash.ToString();
        MaxAmmo.text = "MaxAmmo: " + ps.maxAmmo.ToString();
        FireRate.text = "FireRate: " + ps.fireRate.ToString("0.00");
        ReloadTime.text = "Reload: " + ps.reloadTime.ToString("0.00");
        SpreadFactor.text = "Spread: " + ps.spreadFactor.ToString("0.000");
        RecoilAmount.text = "Recoil: " + (-ps.recoilAmount).ToString("0.000");
        InvOnDash.text = "INVdash: " + ps.invulnerableOnDash.ToString();
        DashIsParry.text = "DParry: " + ps.dashIsParry.ToString();
        AmmoOnParry.text = "AParry: " + ps.ammoOnParry.ToString();
        SlowOnParry.text = "SParry: " + ps.slowOnParry.ToString();
    }
}
