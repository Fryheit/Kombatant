using System;
using System.Windows;
using System.Windows.Media;
using Buddy.Overlay;
using ff14bot;

namespace Kombatant.Helpers
{
    /// <summary>
    /// Helper class used to streamline calling RebornBuddy's OverlayManager.
    /// </summary>
    internal class OverlayHelper
    {
        #region Singleton

        private static OverlayHelper _overlayHelper;
        internal static OverlayHelper Instance => _overlayHelper ?? (_overlayHelper = new OverlayHelper());

        #endregion

        private readonly OverlayManager _overlayManager;

        private OverlayHelper()
        {
            _overlayManager = Core.OverlayManager;
        }

        /// <summary>
        /// Adds a message to the client's screen.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fontColor"></param>
        /// <param name="shadowColor"></param>
        /// <param name="timeToDisplay"></param>
        internal void AddToast(string message, Color fontColor, Color shadowColor, TimeSpan timeToDisplay)
        {
            _overlayManager.AddToast(() => message, timeToDisplay, fontColor,
                shadowColor, new FontFamily(@"Droid Sans"), FontWeights.ExtraBold, 52);
        }

        /// <summary>
        /// Overload of AddToast to omit the TimeSpan.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fontColor"></param>
        /// <param name="shadowColor"></param>
        internal void AddToast(string message, Color fontColor, Color shadowColor)
        {
            AddToast(message, fontColor, shadowColor, TimeSpan.FromSeconds(1));
        }
    }
}