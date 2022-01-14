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

	public class Item : UIBehaviour, IDynamicScrollViewItem 
	{
		private readonly Color[] colors = new Color[] {
			Color.cyan,
			Color.green,
		};

		public Text  mapname;

		public Image background;

		public void onUpdateItem( int index ) {

			if( RealTimeInsertItemExample.data.Count > index ) {

				this.mapname.text          = RealTimeInsertItemExample.data[ index ].Mapname;
			}
		}
	}
}