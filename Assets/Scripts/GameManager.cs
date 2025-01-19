using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /*public static GameManager instance;

    public Gun gun; // Reference to the Gun script
    public int remainingAmmoInMagazine { get; private set; }
    public int maxAmmoInMagazine = 30; // Adjust as needed
    public Text ammoText; // Reference to UI Text for displaying ammo count
    public Text totalAmmo;
    public int remainingTotalAmmo;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance of GameManager exists
        }
    }

    void Start()
    {
        remainingTotalAmmo = 180;
        remainingAmmoInMagazine = maxAmmoInMagazine; // Initialize remaining ammo
        UpdateAmmoText(); // Update UI Text initially
    }

    public void UpdateAmmo(int amount)
    {
        remainingAmmoInMagazine += amount; // Add or subtract ammo based on amount
        remainingAmmoInMagazine = Mathf.Clamp(remainingAmmoInMagazine, 0, maxAmmoInMagazine); // Clamp between 0 and max ammo
        UpdateAmmoText(); // Update UI Text after ammo change
    }

    void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            ammoText.text = "" + remainingAmmoInMagazine;
        }
        else
        {
            Debug.LogWarning("Ammo text element is not assigned.");
        }
    }


    void UpdateTotalAmmo()
    {
        totalAmmo.text = remainingTotalAmmo.ToString();
    }
    void Update()
    {
        // Check if Gun script is assigned and update remaining ammo count accordingly
        if (gun != null)
        {
            remainingAmmoInMagazine = gun.currentAmmoInMag;
            UpdateAmmoText(); // Update UI Text
            remainingTotalAmmo = gun.totalAmmo;
            UpdateTotalAmmo();
        }
    }*/

    public void LoadHome()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
}
