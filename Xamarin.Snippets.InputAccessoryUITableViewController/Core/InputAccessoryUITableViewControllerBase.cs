using System;
using System.Drawing;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;

namespace Xamarin.Snippets.InputAccessoryUITableViewController.Core
{
    public abstract class InputAccessoryUITableViewControllerBase : UITableViewController
    {
        private UIToolbar _toolbar;
        private UITextField _textField;

        protected abstract string PlaceholderText { get; }

        protected abstract string ButtonText { get; }

        public override bool CanBecomeFirstResponder
        {
            get { return true; }
        }

        public override UIView InputAccessoryView
        {
            get { return _toolbar; }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            var padding = 5;
            var height = 50;

            var button = UIButton.FromType(UIButtonType.System);
            button.TouchUpInside += (sender, args) => InputSubmitted(_textField.Text);
            button.SetTitle(ButtonText, UIControlState.Normal);
            button.SizeToFit();
            button.Frame = new RectangleF(new PointF(View.Frame.Width - padding - button.Frame.Width, padding), new SizeF(button.Frame.Width, height - 2 * padding));

            _textField = new UITextField(new RectangleF(padding, padding, View.Frame.Width - 3 * padding - button.Frame.Width, height - 2 * padding))
            {
                Placeholder = PlaceholderText,
                BackgroundColor = UIColor.White,
                Layer =
                {
                    BorderColor = UIColor.Gray.CGColor,
                    BorderWidth = 1,
                    CornerRadius = 3
                }
            };

            _toolbar = new UIToolbar(new RectangleF(0, 0, 0, height));
            _toolbar.AddSubviews(_textField, button);
            
            ReloadInputViews();
        }

        protected void ClearTextAndDismissKeyboard()
        {
            _textField.Text = String.Empty;
            _textField.ResignFirstResponder();
            _textField.EndEditing(true);
        }

        protected abstract void InputSubmitted(string text);
    }
}
