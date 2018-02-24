using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace RecordProperty {
public class CollectionListViewModel : MonoBehaviour {
	public DataContext View;
	public CollectionListContext Context;
	public int testNum = 3;
	private bool m_IsVisible = false;

	void Awake(){
		Context = new CollectionListContext();
		Context.Msg = "Click the button to see the bind effect";
		View.SetContext(Context);
	}

	void Start(){
		for(int i=0;i<testNum;++i){
			Context.OnAddItemClick (null);
		}
	}
}

}
