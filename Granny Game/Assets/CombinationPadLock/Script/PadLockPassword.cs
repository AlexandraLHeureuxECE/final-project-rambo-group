using System.Linq;

using UnityEngine;

public class PadLockPassword : MonoBehaviour
{
    MoveRuller _moveRull;

    public int[] _numberPassword = {0,0,0,0};

    private bool _passwordCorrect = false; // Track if the password is already correct
    public bool PasswordCorrect => _passwordCorrect; // Public property to check if password is correct


    private void Awake()
    {
        _moveRull = FindObjectOfType<MoveRuller>();
    }

    public void Password()
    {
        if (_moveRull._numberArray.SequenceEqual(_numberPassword))
        {

            if(!_passwordCorrect) {
                _passwordCorrect = true;
                Debug.Log("Password correct");
            }

            // Disable blinking and stop the emission material after the correct password is entered
            for (int i = 0; i < _moveRull._rullers.Count; i++)
            {
                var padLockEmissionColor = _moveRull._rullers[i].GetComponent<PadLockEmissionColor>();

                padLockEmissionColor._isSelect = false;  // Disable blinking
                padLockEmissionColor.BlinkingMaterial();  // Make sure you have a method that stops blinking
            }

        }
    }
}
