using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Snippets.Common;

namespace Xamarin.Snippets.KeyboardAdjustingUITableViewController.Core
{
    public class KeyboardAdjustingUITableViewControllerBase : UITableViewController
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillChangeFrameNotification, Listen);
        }

        private void Listen(NSNotification notification)
        {
            var userInfo = notification.UserInfo;

            var beginFrame = userInfo[UIKeyboard.FrameBeginUserInfoKey].AsRectangleF();
            var endFrame = userInfo[UIKeyboard.FrameEndUserInfoKey].AsRectangleF();
            var delta = (endFrame.Y - beginFrame.Y);

            if (Math.Abs(delta) > 0.0)
            {
                var duration = userInfo[UIKeyboard.AnimationDurationUserInfoKey].AsDouble();
                var curve = userInfo[UIKeyboard.AnimationCurveUserInfoKey].AsInt();
                var options = (UIViewAnimationOptions)(curve << 16 | (int)UIViewAnimationOptions.BeginFromCurrentState);

                UIView.Animate(duration, 0, options, () =>
                {
                    TableView.ContentOffset = new PointF(0, TableView.ContentOffset.Y - delta);
                }, () => { });
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            NSNotificationCenter.DefaultCenter.RemoveObserver(UIKeyboard.WillChangeFrameNotification);
        }
    }
}
