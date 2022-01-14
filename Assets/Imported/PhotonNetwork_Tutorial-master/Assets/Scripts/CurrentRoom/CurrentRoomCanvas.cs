using UnityEngine;
using UnityEngine.UI;

public class CurrentRoomCanvas : MonoBehaviour
{
	private ExitGames.Client.Photon.Hashtable m_roomCustomProperties = new ExitGames.Client.Photon.Hashtable();
	public Button StartButton;

	void Update()
	{
		if (PhotonNetwork.inRoom) {
			if (!PhotonNetwork.isMasterClient) {
				StartButton.interactable = false;
			} else {
				StartButton.interactable = true;
			}
		}
	}

    public void OnClickStartSync()
	{
		if (!PhotonNetwork.isMasterClient)
			return;

		PhotonNetwork.room.IsOpen = false;
		PhotonNetwork.room.IsVisible = false;

		m_roomCustomProperties ["started"] = "true";
		PhotonNetwork.room.SetCustomProperties (m_roomCustomProperties);

	}

    public void OnClickStartDelayed()
    {
        if (!PhotonNetwork.isMasterClient)
            return;

        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;

		m_roomCustomProperties["started"] ="true" ;
		PhotonNetwork.room.SetCustomProperties (m_roomCustomProperties);

    }
}
