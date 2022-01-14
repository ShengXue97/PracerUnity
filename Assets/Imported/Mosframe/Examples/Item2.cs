/**
 * Item2.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */

namespace Mosframe {

    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class Item2 : UIBehaviour, IDynamicScrollViewItem 
    {
	    private readonly Color[] colors = new Color[] {
		    Color.cyan,
		    Color.green,
	    };

	    public Text  mapname;
		public Text username;
		public Text rating;
		public Text numberofratings;

	    public Image background;

        public void onUpdateItem( int index ) {

            if( RealTimeInsertItemExample.data.Count > index ) {

				this.mapname.text          = RealTimeInsertItemExample.data[ index ].Mapname;
				this.username.text         = RealTimeInsertItemExample.data[ index ].Username;
				this.rating.text           = RealTimeInsertItemExample.data[ index ].Rating.ToString();
				this.numberofratings.text  = RealTimeInsertItemExample.data[ index ].NumberOfRatings.ToString();
		        //this.background.color   = this.colors[Mathf.Abs(index) % this.colors.Length];
            }
        }
    }
}