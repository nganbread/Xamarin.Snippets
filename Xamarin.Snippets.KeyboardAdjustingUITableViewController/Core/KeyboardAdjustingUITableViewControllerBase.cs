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
            //http://derpturkey.com/maintain-uitableview-scroll-position-with-keyboard-expansion/
            var change = notification.UserInfo;

            var beginFrame = change[UIKeyboard.FrameBeginUserInfoKey].AsRectangleF();
            var endFrame = change[UIKeyboard.FrameEndUserInfoKey].AsRectangleF();
            var delta = (beginFrame.Y - endFrame.Y);

            if (!(Math.Abs(delta) > 0.0)) return;

            // Construct the animation details
            var duration = change[UIKeyboard.AnimationDurationUserInfoKey].AsDouble();
            var curve = change[UIKeyboard.AnimationCurveUserInfoKey].AsInt();
            var options = (UIViewAnimationOptions)(curve << 16 | (int)UIViewAnimationOptions.BeginFromCurrentState);

            UIView.Animate(duration, 0, options, () =>
            {
                var newContentOffset = TableView.ContentOffset.Y + delta;
                if (newContentOffset > TableView.ContentSize.Height - endFrame.Y)
                {
                    //Dont push content all the way up if there is whitespace between the content and keyboard
                    newContentOffset = TableView.ContentSize.Height - endFrame.Y;
                }
                else if (newContentOffset < 0)
                {
                    //Dont push the content too far down
                    newContentOffset = 0;
                }
                TableView.ContentOffset = new PointF(0, newContentOffset);
            }, () => { });
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            NSNotificationCenter.DefaultCenter.RemoveObserver(UIKeyboard.WillChangeFrameNotification);
        }
    }
}
