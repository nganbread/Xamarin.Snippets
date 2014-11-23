using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Xamarin.Snippets.ObservableCollectionUITableView.Core
{
    /// <summary>
    /// A UITableView class that represents a single section containing all rows
    /// </summary>
    public abstract class ObservableCollectionUITableViewBase<T> : UITableViewWithSourceBase
    {
        private ObservableCollection<T> _collection;
        private readonly UITableViewRowAnimation _animationIn;
        private readonly UITableViewRowAnimation _animationOut;

        protected ObservableCollectionUITableViewBase(ObservableCollection<T> collection, UITableViewRowAnimation animationIn = UITableViewRowAnimation.Automatic, UITableViewRowAnimation animationOut = UITableViewRowAnimation.Automatic)
        {
            Collection = collection;
            _animationIn = animationIn;
            _animationOut = animationOut;
        }

        protected ObservableCollectionUITableViewBase(RectangleF frame, ObservableCollection<T> collection, UITableViewRowAnimation animationIn = UITableViewRowAnimation.Automatic, UITableViewRowAnimation animationOut = UITableViewRowAnimation.Automatic)
            : base(frame)
        {
            Collection = collection;
            _animationIn = animationIn;
            _animationOut = animationOut;
        }

        protected ObservableCollection<T> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;

                //When modifying the collection this makes sure that the UI is updated
                _collection.CollectionChanged += ObservableOnCollectionChanged;
                ReloadData();
            }
        }
        protected override int GetNumberOfSections()
        {
            return 1;
        }

        protected override int RowsInSection(int section)
        {
            return _collection.Count;
        }

        private void ObservableOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.NewItems != null)
            {
                InvokeOnMainThread(() =>
                    InsertRows(Enumerable
                        .Range(args.NewStartingIndex, args.NewItems.Count)
                        .Select(x => NSIndexPath.FromItemSection(x, 0)).ToArray(),
                        _animationIn));
            }
            if (args.OldItems != null)
            {
                InvokeOnMainThread(() =>
                    DeleteRows(Enumerable
                            .Range(args.OldStartingIndex, args.OldItems.Count)
                            .Select(x => NSIndexPath.FromItemSection(x, 0)).ToArray(),
                        _animationOut));

            }
        }
    }
}
