using UnityEngine;

public class ButtonInteractable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) // Solo funciona con el Player
        {
            other.GetComponent<PlayerMovement>().Interact(); // Activa animación Interact
            Debug.Log("Botón presionado!");
        }
    }
}
