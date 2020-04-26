/*
 * Player.cs
 * Created by: Ambrosia
 * Created on: 8/2/2020 (dd/mm/yy)
 * Created for: having a generalised manager for the seperate Player scripts
 */

using UnityEngine;

[RequireComponent (typeof (PlayerUIController),
    typeof (PlayerPikminManager),
    typeof (PlayerMovementController))]
public class Player : MonoBehaviour, IHealth {
    //[Header("Components")]
    PlayerMovementController _MovementController;
    PlayerPikminManager _PikminManager;

    [Header ("Settings")]
    [SerializeField] float _MaxHealth = 100;
    [SerializeField] float _CurrentHealth = 100;

    void OnEnable () {
        // Singleton Pattern!
        if (Globals._Player == null)
            Globals._Player = this;
        else
            Destroy (gameObject);
    }

    void Awake () {
        _MovementController = GetComponent<PlayerMovementController> ();
        _PikminManager = GetComponent<PlayerPikminManager> ();

        // Resets the health back to the max if changed in the editor
        _CurrentHealth = _MaxHealth;
    }

    void Update () {
        if (Input.GetKeyDown (KeyCode.Alpha8)) {
            PlayerStats.PrintAll ();
        }

        // Handle health-related functions
        if (_CurrentHealth <= 0)
            Die ();

        // Handle exiting the game/program
        if (Input.GetButtonDown ("Start Button")) {
            print ($"{Globals._OnionManager.IsOnionUnlocked(Colour.Red)}");
            Debug.Break ();
            Application.Quit ();
        }
    }

    void Die () {
        Debug.Log ("Player is dead!");
        Debug.Break ();
    }

    #region Health Implementation

    // 'Getter' functions
    public float GetHealth () => _CurrentHealth;
    public float GetMaxHealth () => _MaxHealth;
    // 'Setter' functions
    public void GiveHealth (float give) => _CurrentHealth += give;
    public void TakeHealth (float take) => _CurrentHealth -= take;
    public void SetHealth (float set) => _CurrentHealth = set;

    #endregion

    #region Global Getters
    public PlayerMovementController GetMovementController () => _MovementController;
    public PlayerPikminManager GetPikminManager () => _PikminManager;
    #endregion
}