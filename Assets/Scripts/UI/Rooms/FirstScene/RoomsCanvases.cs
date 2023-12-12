using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsCanvases : MonoBehaviour
{
    [SerializeField] private CreateOrJoinRoomCanvas _createOrJoinRoomCanvas;
    public CreateOrJoinRoomCanvas CreateOrJoinRoomCanvas {get { return _createOrJoinRoomCanvas;}}

    [SerializeField] private CurrentRoomCanvas _currentRoomCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas {get { return _currentRoomCanvas; } }

    
    // [SerializeField] private CharacterSelection _characterSelectionCanvas;
    // public CharacterSelection CharacterSelectionCanvas {get { return _characterSelectionCanvas; } }


    private void Awake()
    {
        FirstInitialize();
    }

    private void FirstInitialize()
    {
        CreateOrJoinRoomCanvas.FirstInitialize(this);
        CurrentRoomCanvas.FirstInitialize(this);
     //   CharacterSelectionCanvas.FirstInitialize(this);
    }
}
