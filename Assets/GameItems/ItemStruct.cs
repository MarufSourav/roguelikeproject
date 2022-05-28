using UnityEngine;
[System.Serializable]
public class ItemStruct
{
    public string Name;
    public GameObject item;
    public Sprite itemSprite;
    public int numOfExtraJump;//
    public int numOfDash;//
    public bool invulnerableOnDash;//
    public float normalMoveSpeed;//
    public int magAmmo;//
    public int maxAmmo;//
    public float fireRate;//
    public float reloadTime;//
    public float spreadFactor;//
    public float recoilAmount;//
    public float damage;//
    public bool dashIsParry;//
    public bool ammoOnParry;//
    public bool slowOnParry;//
}
