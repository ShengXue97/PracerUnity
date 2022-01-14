using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class RoomLayoutGroup : MonoBehaviour
{

	[SerializeField]
	private GameObject _roomListingPrefab;
	private GameObject RoomListingPrefab
	{
		get { return _roomListingPrefab; }
	}

	private List<RoomListing> _roomListingButtons = new List<RoomListing>();
	private List<RoomListing> RoomListingButtons
	{
		get { return _roomListingButtons; }
	}

	void OnEnable()
    {
		StartCoroutine (UpdateRooms (0.5f));
	}

	private IEnumerator UpdateRooms(float time)
	{
		yield return new WaitForSeconds(time);

		// Update rooms every 0.5s
		RoomInfo[] rooms = PhotonNetwork.GetRoomList();
		foreach (RoomInfo room in rooms) {
			/*
			if (!room.Name.Contains ("LevelEditor")) {
				RoomReceived (room);
			}
			*/
			RoomReceived (room);
		}
		RemoveOldRooms();
		StartCoroutine (UpdateRooms (0.5f));
	}

	private void OnReceivedRoomListUpdate()
	{
		RoomInfo[] rooms = PhotonNetwork.GetRoomList();
		foreach (RoomInfo room in rooms) {
			/*
			if (!room.Name.Contains ("LevelEditor")) {
				RoomReceived (room);
			}
			*/
			RoomReceived (room);
		}
		RemoveOldRooms();
	}

	private void RoomReceived(RoomInfo room)
	{
		int index = RoomListingButtons.FindIndex(x => x.RoomName == room.Name);
		if (index == -1)
		{
			if (room.IsVisible && room.PlayerCount < room.MaxPlayers)
			{
				GameObject roomListingObj = Instantiate(RoomListingPrefab);
				roomListingObj.transform.SetParent(transform, false);

				RoomListing roomListing = roomListingObj.GetComponent<RoomListing>();
				RoomListingButtons.Add(roomListing);

				index = (RoomListingButtons.Count - 1);
			}
		}

		if (index != -1)
		{
			RoomListing roomListing = RoomListingButtons[index];

			var split = room.Name.Split ('/');
			string mapname = split [split.Length - 1];
			string mapusername = split [split.Length - 3];
			string mapnamewithoutTXT = mapname.Substring (0, mapname.Length - 4);

			roomListing.SetRoomNameText(mapnamewithoutTXT + "<color=#FFA53F> by: </color>" + mapusername,room.Name);
			roomListing.Updated = true;
		}
	}

	private void RemoveOldRooms()
	{
		List<RoomListing> removeRooms = new List<RoomListing>();

		foreach (RoomListing roomListing in RoomListingButtons)
		{
			if (!roomListing.Updated)
				removeRooms.Add(roomListing);
			else
				roomListing.Updated = false;
		}

		foreach (RoomListing roomListing in removeRooms)
		{
			GameObject roomListingObj = roomListing.gameObject;
			RoomListingButtons.Remove(roomListing);
			Destroy(roomListingObj);
		}
	}

}
