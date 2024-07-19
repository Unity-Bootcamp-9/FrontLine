using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    
    private void Start()
    {
        player = Camera.main.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        Debug.Log(Managers.Instance.weaponDatas[0].weaponPrefab);
    }

    public void SetWeapon()
    {
        string path = Managers.Instance.weaponDatas[0].weaponPrefab;

        GameObject gunPrefab = Resources.Load<GameObject>(path);

        if (gunPrefab != null)
        {
            GameObject newGun = Instantiate(gunPrefab);

            Weapon weapon = gunPrefab.GetComponent<Weapon>();

            weapon.Initialize(Managers.Instance.weaponDatas[0]);

            newGun.name = "blaster";

            newGun.transform.parent = player.transform; 
            newGun.transform.localPosition = Vector3.zero;
            newGun.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.LogError("Failed to load gun prefab.");
        }
    }
}
