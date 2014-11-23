using System;
using System.Collections.ObjectModel;
using MonoTouch.UIKit;

namespace Xamarin.Snippets.ObservableCollectionUITableView
{
    internal class ObservableCollectionUITableViewController : UITableViewController
    {
        private ObservableCollection<string> _data;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.SetRightBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Add, Add), true);

            _data = new ObservableCollection<string>();
            TableView = new ObservableCollectionUITableView(_data);
        }

        private void Add(object sender, EventArgs e)
        {
            _data.Add(Guid.NewGuid().ToString());
        }
    }
}