function Start ()
{
  textMesh = gameObject.GetComponent(MeshRenderer);
}

//----------------------------------------------------------------------------------
function Update ()
{
  // if the danger zone then printing message to go away from the are
  if (Vector3.Distance(transform.position, player.position) < showOnDistance)
    textMesh.enabled = true;
  //else not enabling the text message
    else
     textMesh.enabled = false;

}
