using StarterAssets;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    [SerializeField] private CharacterController _charController;

    [SerializeField] private float _originalHeight = 2;
    [SerializeField] private float _crouchedHeight = 1;
    [SerializeField] private bool _isCrouched = false; 
    
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] private bool _isInventoryOn = false;
    [SerializeField] FirstPersonController _firstPersonController;
    [SerializeField] StarterAssetsInputs _starterAssetsInputs;
    
    void Start()
    {
        _originalHeight = _charController.height;
        UpdateInventory(false);
    }

    private void OnCrouch()
    {
        if (_isCrouched)
        {
            _isCrouched = false;
            _charController.height = _originalHeight;
            Debug.Log("Normal Height");
        }
        else
        {
            _isCrouched = true;
            _charController.height = _crouchedHeight;
            Debug.Log("Crouch Height");
        }
    }

    private void OnInventory()
    {
        if (_isInventoryOn)
        {
            UpdateInventory(false);
        }
        else
        {
            UpdateInventory(true);
        }
    }

    private void UpdateInventory(bool isOn)
    {
        _isInventoryOn = isOn;
        _canvasGroup.alpha = isOn ? 1 : 0;
        _charController.enabled = !isOn;
        if (isOn)
        {
            _firstPersonController.ChangePlayerStateUI();
            _starterAssetsInputs.SetCursorState(false);
        }
        else
        {
            _firstPersonController.ChangePlayerStateGameplay();
            _starterAssetsInputs.SetCursorState(true);
        }
    }
}
