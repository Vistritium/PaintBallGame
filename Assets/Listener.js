#pragma strict

function Start () {

}

function Update () {

}

function MoveRightJS(value: String){
	var res = float.Parse(value);
	transform.Translate(res, 0, 0);
}