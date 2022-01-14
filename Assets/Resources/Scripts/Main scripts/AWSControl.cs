using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using UnityEngine;
using Random = System.Random;
using System.Collections;

public class AWSControl : MonoBehaviour {
	public CloudStorageAccount StorageAccount;
	public string value;
	public string result="";
	// Use this for initialization
	void Start () {
		//BlobStorageTest ();
	}

	// Update is called once per frame
	void Update () {

	}
	public void AWSUpload(string dest,string text)
	{
		StartCoroutine( AzureUpload(dest,text));
	}
	public void AWSDownload(string id,string dest)
	{
		StartCoroutine(AzureDownload (id,dest));
	}
	public void AWSList(string dest,int ListID)
	{
		StartCoroutine(AzureList (dest,ListID));
	}
	public void AWSDelete(string dest,bool IsChat)
	{
		StartCoroutine(AzureDelete (dest,IsChat));
	}

	public IEnumerator AzureDelete(string dest,bool IsChat)
	{

		// Delete a blob

		StorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=http;AccountName=xue;AccountKey=Xp5+gHw5N4e9mAhYFpNS8WNNbFJ/XfuhOlhSTGSLsOzySDlsIOoeucjdixRidL+1JX6NkEka9Umq4QVHka9xFg==;EndpointSuffix=core.windows.net");

		CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();


		CloudBlobContainer container = blobClient.GetContainerReference("xue");

		CloudBlockBlob blockBlob = container.GetBlockBlobReference(dest);


		try
		{
			if (IsChat)
			{
				blockBlob.DeleteAsync();
			}
			else{
				foreach (IListBlobItem blob in container.GetDirectoryReference(dest).ListBlobs(true))
				{
					if (blob.GetType() == typeof(CloudBlob) || blob.GetType().BaseType == typeof(CloudBlob))
					{
						((CloudBlob)blob).DeleteIfExists();
					}
				}
			}
			result="DeleteTrue";
			//print("Deleted blob");
		}
		catch (StorageException)
		{
			result="DeleteFalse";
			//print ("Error deleting");
		}
		yield break;
	}
	public IEnumerator AzureList(string dest,int ListID)
	{
		result = "";

		StorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=http;AccountName=xue;AccountKey=Xp5+gHw5N4e9mAhYFpNS8WNNbFJ/XfuhOlhSTGSLsOzySDlsIOoeucjdixRidL+1JX6NkEka9Umq4QVHka9xFg==;EndpointSuffix=core.windows.net");

		CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();


		CloudBlobContainer container = blobClient.GetContainerReference("xue");

		try
		{
			// List all the blobs in the container 
			BlobContinuationToken token = null;
			var list =container.ListBlobs(dest,true);
			string temp="";
			foreach (IListBlobItem blob in list)
			{
				//Not listing parent directory
				if (blob.Uri.Segments[blob.Uri.Segments.Length-2]+blob.Uri.Segments[blob.Uri.Segments.Length-1]!=dest+'/')
				{
					//LISTID 0 : NORMAL LISTING
					if (ListID==0){
						if (temp!="")
						{
							temp+="+"+WWW.UnEscapeURL(blob.Uri.ToString());
						}
						else{
							temp+=WWW.UnEscapeURL(blob.Uri.ToString());
						}
					}
					//LISTID 1 : CHAT
					else if (ListID==1)
					{
						string LastModified=(blob as CloudBlockBlob).Properties.LastModified.ToString();
						string finalSeconds=ConvertLastModifiedToSecocnds(LastModified);
						string unescapeduri=WWW.UnEscapeURL(blob.Uri.ToString());
						var bloburi=unescapeduri.Split('/');
						string blobdest= bloburi [bloburi.Length - 2] + bloburi [bloburi.Length - 1];

						if (temp!="")
						{
							temp+="+"+finalSeconds+"/"+blobdest;
						}
						else{
							temp+=finalSeconds+"/"+ blobdest;
							}
					}
					//LISTID 2 : LEVEL LISTING FOR LATEST
					else if (ListID==2)
					{
						string LastModified=(blob as CloudBlockBlob).Properties.LastModified.ToString();
						string finalSeconds=ConvertLastModifiedToSecocnds(LastModified);
						string unescapeduri=WWW.UnEscapeURL(blob.Uri.ToString());

						if (temp!="")
						{
							temp+="+"+finalSeconds+"/"+unescapeduri;
						}
						else{
							temp+=finalSeconds+"/"+ unescapeduri;
						}
					}
				}
			}
			if (ListID==0 && temp=="")
			{
				temp="empty";
			}
			result=temp;
			//print("Listed: "+result);
		}
		catch (StorageException)
		{
			result="none";
			print ("Error listing");
		}

		yield break;
	}

	private IEnumerator AzureDownload(string id,string dest)
	{
		result = "";

		// Download a blob to your file system

		StorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=http;AccountName=xue;AccountKey=Xp5+gHw5N4e9mAhYFpNS8WNNbFJ/XfuhOlhSTGSLsOzySDlsIOoeucjdixRidL+1JX6NkEka9Umq4QVHka9xFg==;EndpointSuffix=core.windows.net");

		CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();


		CloudBlobContainer container = blobClient.GetContainerReference("xue");

		CloudBlockBlob blockBlob = container.GetBlockBlobReference(dest);
		//print ("Downloading" + dest);

		try
		{
			string text=blockBlob.DownloadText();
			result=id+"|"+ text;
			print("Downloaded: "+text);
		}
		catch (StorageException ex)
		{
			result="none";
			
			print ("Error downloading");

		}
		yield break;
	}
	private IEnumerator AzureUpload(string dest,string text)
	{
		result = "";

		// Create a blob client for interacting with the blob service.
		StorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=http;AccountName=xue;AccountKey=Xp5+gHw5N4e9mAhYFpNS8WNNbFJ/XfuhOlhSTGSLsOzySDlsIOoeucjdixRidL+1JX6NkEka9Umq4QVHka9xFg==;EndpointSuffix=core.windows.net");

		CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();


		CloudBlobContainer container = blobClient.GetContainerReference("xue");

		CloudBlockBlob blockBlob = container.GetBlockBlobReference(dest);
		//print ("Uploading" + dest + ";" + text);
		try
		{
			blockBlob.UploadText(text);
			result="true";
			//print("Uploaded");
		}
		catch (StorageException)
		{
			result="false";
			//print ("Error uploading");
		}

		yield break;
	}

	private string ConvertLastModifiedToSecocnds(string lastmodified)
	{
		string oneline=lastmodified.Split('\n')[0];
		string onlytime=oneline.Split(' ')[1];
		var timesplit=onlytime.Split(':');

		int hour=int.Parse(timesplit[0]);
		int minute=int.Parse(timesplit[1]);
		int second=int.Parse(timesplit[2]);

		string onlydate=oneline.Split(' ')[0];
		var datesplit=onlydate.Split('/');

		int day=int.Parse(datesplit[0]);
		int month=int.Parse(datesplit[1]);
		int year=int.Parse(datesplit[2]);

		string finalSeconds = (second + (minute * 60) + (hour * 3600) + (day*86400) + (month*2.628e+6) + (year*3.154e+7)).ToString();
		return finalSeconds;
	}
}
