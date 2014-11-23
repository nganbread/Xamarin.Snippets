using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Xamarin.Snippets.KeyboardAdjustingUITableViewController
{
    internal class TextFieldTableViewSource : UITableViewSource
    {
        private readonly IList<UITableViewCell> _data;

        public TextFieldTableViewSource()
        {
            _data = Enumerable.Range(0, 100).Select(BuildTableCell).ToList();
        }

        private static UITableViewCell BuildTableCell(int index)
        {
            return new UITableViewCell
            {
                TextLabel = { Text = index.ToString()},
                SelectionStyle = UITableViewCellSelectionStyle.None,
                AccessoryView = new UITextField
                {
                    Frame = new RectangleF(0,0, 250, 100),
                    Placeholder = "Write Something Here",
                }
            };
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            return _data.ElementAt(indexPath.Item);
        }

        public override int RowsInSection(UITableView tableview, int section)
        {
            return _data.Count;
        }

        public override int NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public void EndEditing()
        {
            foreach (var cell in _data)
            {
                (cell.AccessoryView as UITextField).EndEditing(true);
            }
        }
    }
}