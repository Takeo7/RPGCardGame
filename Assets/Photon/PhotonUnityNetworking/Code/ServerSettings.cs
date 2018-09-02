// ----------------------------------------------------------------------------
// <copyright file="PunClasses.cs" company="Exit Games GmbH">
//   PhotonNetwork Framework for Unity - Copyright (C) 2018 Exit Games GmbH
// </copyright>
// <summary>
// ScriptableObject defining a server setup. An instance is created as <b>PhotonServerSettings</b>.
// </summary>
// <author>developer@exitgames.com</author>
// ----------------------------------------------------------------------------

namespace Photon.Pun
{
    using System;
    using System.Collections.Generic;
    using ExitGames.Client.Photon;
    using Photon.Realtime;
    using UnityEngine;

    /// <summary>
    /// Collection of connection-relevant settings, used internally by PhotonNetwork.ConnectUsingSettings.
    /// </summary>
    [Serializable]
	public class ServerSettings : ScriptableObject
    {
        public AppSettings AppSettings;
        public bool StartInOfflineMode;

        public PunLogLevel PunLogging = PunLogLevel.ErrorsOnly;
		public bool EnableSupportLogger;
	    public bool RunInBackground = true;

        public List<string> RpcList = new List<string>();   // set by scripts and or via Inspector

        [HideInInspector]
        public bool DisableAutoOpenWizard;

        public void UseCloud(string cloudAppid, string code = "")
        {
            this.AppSettings.AppIdRealtime = cloudAppid;
            this.AppSettings.Server = null;
            this.AppSettings.FixedRegion = string.IsNullOrEmpty(code) ? null : code;
            Debug.Log("this.AppSettings.IsBestRegion: " + this.AppSettings.IsBestRegion);
        }

        /// <summary>Checks if a string is a Guid by attempting to create one.</summary>
        /// <param name="val">The potential guid to check.</param>
        /// <returns>True if new Guid(val) did not fail.</returns>
        public static bool IsAppId(string val)
        {
            try
            {
                new Guid(val);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the best region code in preferences.
        /// This composes the PhotonHandler, since its Internal and can not be accessed by the custom inspector
        /// </summary>
        /// <value>The best region code in preferences.</value>
        public static string BestRegionSummaryInPreferences
        {
            get { return PhotonNetwork.BestRegionSummaryInPreferences; }
        }

        public static void ResetBestRegionCodeInPreferences()
	    {
		    PhotonNetwork.BestRegionSummaryInPreferences = null;
	    }

        public override string ToString()
        {
            return "ServerSettings: " + this.AppSettings.ToStringFull();
        }
    }
}