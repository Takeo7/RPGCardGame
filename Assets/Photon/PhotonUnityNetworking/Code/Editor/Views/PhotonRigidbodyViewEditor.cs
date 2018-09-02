using UnityEditor;
using UnityEngine;


namespace Photon.Pun
{
    [CustomEditor(typeof (PhotonRigidbodyView))]
    public class PhotonRigidbodyViewEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                EditorGUILayout.HelpBox("Editing is disabled in play mode.", MessageType.Info);
                return;
            }

            PhotonRigidbodyView view = (PhotonRigidbodyView)target;

            view.m_SynchronizeVelocity = PhotonGUI.ContainerHeaderToggle("Synchronize Velocity", view.m_SynchronizeVelocity);
            view.m_SynchronizeAngularVelocity = PhotonGUI.ContainerHeaderToggle("Synchronize Angular Velocity", view.m_SynchronizeAngularVelocity);
        }
    }
}