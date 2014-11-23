using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Xamarin.Snippets.InputAccessoryUITableViewController
{
    internal class TextTableViewSource : UITableViewSource
    {
        private readonly IList<string> _data;

        public TextTableViewSource(IList<string> data)
        {
            _data = data;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            return new UITableViewCell
            {
                TextLabel = { Text = _data.ElementAt(indexPath.Item) }
            };
        }

        public override int RowsInSection(UITableView tableview, int section)
        {
            return _data.Count;
        }

        public override int NumberOfSections(UITableView tableView)
        {
            return 1;
        }
    }
}