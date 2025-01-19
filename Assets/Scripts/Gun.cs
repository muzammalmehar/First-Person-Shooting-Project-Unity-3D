using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Transform muzzleTransform;
    public float fireRate = 0.1f;
    public int maxAmmo = 30;
    public int burstCount = 3; // Number of bullets to fire in a burst
    public float burstDelay = 0.1f; // Time between each bullet in a burst
    public float reloadTime = 2f;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public ParticleSystem muzzleFlash;
    public float recoilAmount = 1f;
    public float recoilDuration = 0.1f;
    public float range = 100f;
    public int damageAmount = 10;
    public GameObject bulletPrefab;
    public GameObject bulletHolePrefab;
    public Transform bulletSpawnPoint; // Position to spawn the bullet
    public int totalAmmo = 180;  // Total ammo pool
    public int currentAmmoInMag;
    public int magazineCapacity = 30;
    public Text ammoText;
    public Text totalAmmoText; // Reference to UI Text for displaying total ammo

    private int currentAmmo;
    private bool isReloading = false;
    private bool isFiring = false; // Flag to indicate if the gun is currently firing
    private AudioSource audioSource;
    private Vector3 originalPosition;

    void Start()
    {
        currentAmmoInMag = magazineCapacity;
        audioSource = GetComponent<AudioSource>();
        originalPosition = transform.localPosition;
        UpdateAmmoText();
        UpdateTotalAmmoText();
    }

    void Update()
    {
        if (isReloading) return;

        if (currentAmmoInMag <= 0)
        {
            if (totalAmmo > 0) // Only reload if total ammo is available
            {
                StartCoroutine(Reload());
            }
            else
            {
                Debug.Log("Ammo is empty!"); // Print "Ammo is empty" message
            }
            return; // Stop further processing in Update() when out of ammo
        }

        if (Input.GetKeyDown(KeyCode.M) && !isFiring) // Check for automatic fire input only if not already firing
        {
            StartCoroutine(AutomaticFire());
        }

        if (Input.GetKeyDown(KeyCode.B) && !isFiring) // Check for burst fire input only if not already firing
        {
            StartCoroutine(BurstFire());
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
        UpdateAmmoText();
    }

    void Fire()
    {
        if (currentAmmoInMag > 0) // Check if there's ammo in the magazine
        {
            currentAmmoInMag--;
            Vector3 spawnPosition = bulletSpawnPoint != null ? bulletSpawnPoint.position : muzzleTransform.position;
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, muzzleTransform.rotation);
            RaycastHit hit;
            if (Physics.Raycast(muzzleTransform.position, muzzleTransform.forward, out hit, range))
            {
                Debug.Log("Hit " + hit.collider.name);
                Health target = hit.collider.GetComponent<Health>();
                if (target != null)
                {
                    target.TakeDamage(damageAmount);
                }
                Instantiate(bulletHolePrefab, hit.point, Quaternion.LookRotation(hit.normal));
            }
            PlaySound(fireSound);
            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
            }
            else
            {
                Debug.LogWarning("Muzzle flash is not assigned.");
            }
            StartCoroutine(ApplyRecoil());
            UpdateAmmoText();
        }
    }

    IEnumerator ApplyRecoil()
    {
        Vector3 recoilPosition = originalPosition + Vector3.back * recoilAmount;
        float elapsedTime = 0f;
        while (elapsedTime < recoilDuration)
        {
            transform.localPosition = Vector3.Lerp(originalPosition, recoilPosition, elapsedTime / recoilDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = recoilPosition;
        elapsedTime = 0f;
        while (elapsedTime < recoilDuration)
        {
            transform.localPosition = Vector3.Lerp(recoilPosition, originalPosition, elapsedTime / recoilDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        isFiring = false;
        PlaySound(reloadSound);
        yield return new WaitForSeconds(reloadTime);
        int ammoToLoad = Mathf.Min(magazineCapacity - currentAmmoInMag, totalAmmo);
        currentAmmoInMag += ammoToLoad;
        totalAmmo -= ammoToLoad;
        isReloading = false;
        UpdateAmmoText();
        UpdateTotalAmmoText();
    }

    IEnumerator AutomaticFire()
    {
        isFiring = true;
        while (Input.GetButton("Fire3") && currentAmmoInMag > 0)
        {
            Fire();
            yield return new WaitForSeconds(fireRate);
        }
        isFiring = false;
    }

    IEnumerator BurstFire()
    {
        isFiring = true;
        for (int i = 0; i < burstCount && currentAmmoInMag > 0; i++)
        {
            Fire();
            yield return new WaitForSeconds(burstDelay);
        }
        isFiring = false;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Audio clip is not assigned.");
        }
    }

    void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            ammoText.text = "" + currentAmmoInMag;
        }
        else
        {
            Debug.LogWarning("Ammo text element is not assigned.");
        }
    }

    void UpdateTotalAmmoText()
    {
        if (totalAmmoText != null)
        {
            totalAmmoText.text = "" + totalAmmo;
        }
        else
        {
            Debug.LogWarning("Total ammo text element is not assigned.");
        }
    }
}
