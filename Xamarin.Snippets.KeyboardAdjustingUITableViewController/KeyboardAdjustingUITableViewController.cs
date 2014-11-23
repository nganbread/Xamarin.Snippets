using System;
using MonoTouch.UIKit;
using Xamarin.Snippets.KeyboardAdjustingUITableViewController.Core;

namespace Xamarin.Snippets.KeyboardAdjustingUITableViewController
{
    internal class KeyboardAdjustingUITableViewController : KeyboardAdjustingUITableViewControllerBase
    {
        private TextFieldTableViewSource _source;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Cancel, CloseKeyboard), true);

            TableView = new UITableView
            {
                Source = _source = new TextFieldTableViewSource()
            };
        }

        private void CloseKeyboard(object sender, EventArgs e)
        {
            _source.EndEditing();
        }
    }
}
