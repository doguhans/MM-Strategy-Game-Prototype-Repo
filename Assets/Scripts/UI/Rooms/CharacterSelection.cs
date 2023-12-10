using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    private List<GameObject> models;

    private int selectionIndex = 0;

    [SerializeField]private CreateRoomMenu _createRoomMenu;

    [SerializeField] private RoomListingsMenu _roomListingsMenu;
    private RoomsCanvases _roomsCanvases;
    public void FirstInitialize(RoomsCanvases canvases) 
    {
        _roomsCanvases = canvases;
        _createRoomMenu.FirstInitialize(canvases);
        _roomListingsMenu.FirstInitialize(canvases);
    }

    void Start()
    {
        models = new List<GameObject>();
        foreach(Transform t in transform)
        {
            models.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }

        models[selectionIndex].SetActive(true);

    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButton(0))
        transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X")* -1, 0.0f));
    }

    public void Select (int index)
    {
        if(index == selectionIndex)
            return;

        if(index <0 || index >= models.Count)
            return;    

        models[selectionIndex].SetActive(false);
        selectionIndex = index;
        models[selectionIndex].SetActive(true);    
    }
}
