using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private Animator animator; // Reference to the Animator component
    private bool isDead = false; // Flag to prevent multiple death triggers

    private CharacterController characterController; // Reference to CharacterController or any other movement script
    private PlayerController playerController; // Reference to your player control script

    public Slider healthSlider; // Reference to the UI Slider for health

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>(); // Get the Animator component
        characterController = GetComponent<CharacterController>(); // Get the CharacterController component
        playerController = GetComponent<PlayerController>(); // Get the PlayerController component

        // Initialize the health slider
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return; // If already dead, ignore further damage

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        // Update the health slider
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    void Die()
    {
        if (isDead) return; // Prevent multiple death triggers

        isDead = true; // Set the dead flag

        // Trigger the death animation
        if (animator != null)
        {
            animator.SetTrigger("Die"); // Assuming "Die" is the name of the death trigger parameter in the Animator controller
        }

        // Disable character movement and other components
        DisableCharacter();
    }

    void DisableCharacter()
    {
        if (characterController != null)
        {
            characterController.enabled = false;
        }

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Disable other components or scripts as needed
    }
}
