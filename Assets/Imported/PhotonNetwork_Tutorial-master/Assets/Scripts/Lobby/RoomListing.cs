
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private Text _roomNameText;
    private Text RoomNameText
    {
        get { return _roomNameText; }
    }

    public string RoomName { get; private set; }
    public bool Updated { get; set; }

	private void Start()
    {
		GameObject PhotonController = GameObject.FindGameObjectWithTag ("PhotonController");
		Button button = GetComponent<Button>();

		if (PhotonController != null) {
			button.onClick.AddListener(() => StartCoroutine( PhotonController.GetComponent<photonConnect>().OnClickJoinRoom(RoomName)));
			button.onClick.AddListener(SetMap);
		}

        
	}

    private void OnDestroy()
    {
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }
	
	public void SetRoomNameText(string listedText,string room)
    {
		RoomName = room;
		RoomNameText.text = listedText;
    }

	void SetMap()
	{
		Camera.main.GetComponent<networkcamera> ().controller.lastmapname = RoomName;
	}
}
