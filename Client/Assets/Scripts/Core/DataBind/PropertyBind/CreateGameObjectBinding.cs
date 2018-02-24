using UnityEngine;
using System.Collections;
namespace RecordProperty
{
public class CreateGameObjectBinding : RecordProperty.PropertyBind
{
	public GameObject Template;

	protected override void OnChange(){
		base.OnChange();
		ClearGameObject ();
		var newValue = GetIntValue();
		ApplyNewValue(newValue);
	}

	protected virtual void ApplyNewValue(int newValue){	
		for(int i=0;i<newValue;++i){
			GameObject instance = (GameObject)Instantiate(Template);
			instance.SetActive (true);
			instance.transform.SetParent(transform);
			instance.transform.localScale = transform.localScale;
			instance.name = i.ToString();
		}
	}

	protected virtual void ClearGameObject(){
		for(int i=0;i<transform.childCount;++i){
			DestroyObject(transform.GetChild(i) ,0.5f);
		}
	}
}

}
