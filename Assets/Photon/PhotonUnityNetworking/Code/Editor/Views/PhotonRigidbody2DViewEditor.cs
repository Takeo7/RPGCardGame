using UnityEditor;
using UnityEngine;


namespace Photon.Pun
{
    [CustomEditor(typeof (PhotonRigidbody2DView))]
    public class PhotonRigidbody2DViewEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                EditorGUILayout.HelpBox("Editing is disabled in play mode.", MessageType.Info);
                return;
            }

            PhotonRigidbody2DView view = (PhotonRigidbody2DView)target;

            view.m_SynchronizeVelocity = PhotonGUI.ContainerHeaderToggle("Synchronize Velocity", view.m_SynchronizeVelocity);
            view.m_SynchronizeAngularVelocity = PhotonGUI.ContainerHeaderToggle("Synchronize Angular Velocity", view.m_SynchronizeAngularVelocity);
        }
    }
}