using System.Collections.ObjectModel;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Snippets.ObservableCollectionUITableView.Core;

namespace Xamarin.Snippets.ObservableCollectionUITableView
{
    internal class ObservableCollectionUITableView : ObservableCollectionUITableViewBase<string>
    {
        private readonly NSString _reuseIdentifier = new NSString("UITableViewCellReuseIdentifier");

        public ObservableCollectionUITableView(ObservableCollection<string> collection)
            : base(collection, UITableViewRowAnimation.Left)
        {
            
        }

        protected override void RegisterTypesForReuse()
        {
            RegisterClassForCellReuse(typeof(UITableViewCell), _reuseIdentifier);
        }

        protected override UITableViewCell GetCell(NSIndexPath indexPath)
        {
            var cell = DequeueReusableCell(_reuseIdentifier);
            cell.TextLabel.Text = Collection[indexPath.Item];
            return cell;
        }

        protected override float GetHeightForCellRow(NSIndexPath indexPath)
        {
            return 50;
        }
    }
}