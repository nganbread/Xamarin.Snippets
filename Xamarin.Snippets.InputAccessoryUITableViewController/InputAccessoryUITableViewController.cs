using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using Xamarin.Snippets.InputAccessoryUITableViewController.Core;

namespace Xamarin.Snippets.InputAccessoryUITableViewController
{
    internal class InputAccessoryUITableViewController : InputAccessoryUITableViewControllerBase
    {
        private List<string> _data;

        protected override string PlaceholderText
        {
            get { return "Input Goes Here"; }
        }

        protected override string ButtonText
        {
            get { return "Input"; }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TableView = new UITableView
            {
                KeyboardDismissMode = UIScrollViewKeyboardDismissMode.Interactive,
                BackgroundColor = UIColor.White,
                Source = new TextTableViewSource(_data = new List<string>())
            };
        }

        protected override void InputSubmitted(string text)
        {
            if (String.IsNullOrWhiteSpace(text)) return;

            _data.Add(text);
            TableView.ReloadData();
            ClearTextAndDismissKeyboard();
        }
    }
}
