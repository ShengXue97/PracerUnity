/**
 * RealTimeInsertItemExample.cs
 *
 * @author mosframe / https://github.com/mosframe
 *
 */

namespace Mosframe {

    using System;
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// RealTimeInsertItemExample
    /// </summary>
    public class RealTimeInsertItemExample : MonoBehaviour {

        public class CustomData {

            public string   Mapname;
			public string   Username;
			public string    Rating;
			public string      NumberOfRatings;
        }

        public static List<CustomData> data = new List<CustomData>();



        public DynamicScrollView    scrollView;
        public  int                 dataIndex    = 1;
        public  string              dataName     = "Insert01";



        private void Start() {

        }

		public void AddItem(int index,string IMapname,string IUsername,string IRating,string INumberOFRatings)
		{
			this.insertItem( index, new CustomData(){Mapname=IMapname, Username=IUsername,Rating=IRating,NumberOfRatings=INumberOFRatings} );
		}

		public void AddEditorItem(int index,string IMapname)
		{
			this.insertItem( index, new CustomData(){Mapname=IMapname} );
		}

        public void insertItem( int index, CustomData data ) {

            // TODO : set custom data
            RealTimeInsertItemExample.data.Insert( index, data );

            this.scrollView.totalItemCount = RealTimeInsertItemExample.data.Count;
        }

		public void clearList()
		{
			RealTimeInsertItemExample.data.Clear ();
			this.scrollView.totalItemCount = 0;
			Camera.main.GetComponent<networkcamera>().LoadPanel.SetActive (true);
		}

		public void clearEditorList()
		{
			RealTimeInsertItemExample.data.Clear ();
			this.scrollView.totalItemCount = 0;
			Camera.main.GetComponent<CameraFollow>().LoadPanel.SetActive (true);
		}
    }

    #if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(RealTimeInsertItemExample))]
    public class RealTimeAddItemExampleEditor : UnityEditor.Editor {

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if( Application.isPlaying ) {

                if( GUILayout.Button("Insert") ) {

                    var example = (RealTimeInsertItemExample)this.target;
                    //example.insertItem(example.dataIndex, new RealTimeInsertItemExample.CustomData(){name=example.dataName,value=100} );
                }
            }
        }
    }

    #endif
}