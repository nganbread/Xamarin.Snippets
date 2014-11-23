using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Xamarin.Snippets.ObservableCollectionUITableView.Core
{
    /// <summary>
    /// Most of the methods are actually found on UITableView
    /// However, by using a Source we get the ability to use
    /// other features such as custom cell and header heights
    /// </summary>
    public abstract class UITableViewWithSourceBase : UITableView
    {
        protected UITableViewWithSourceBase()
        {
            RegisterTypesForReuse();
            Source = new DelegatedDataSource(RowsInSection, GetCell, GetHeightForCellRow, GetHeightForSectionHeader, GetNumberOfSections, GetViewForSectionHeader, RowWasSelected, GetViewForSectionFooter, GetHeightForSectionFooter);
        }
        protected UITableViewWithSourceBase(RectangleF frame)
            : base(frame)
        {
            RegisterTypesForReuse();
            Source = new DelegatedDataSource(RowsInSection, GetCell, GetHeightForCellRow, GetHeightForSectionHeader, GetNumberOfSections, GetViewForSectionHeader, RowWasSelected, GetViewForSectionFooter, GetHeightForSectionFooter);
        }

        protected UITableViewWithSourceBase(RectangleF frame, UITableViewStyle style)
            : base(frame, style)
        {
            RegisterTypesForReuse();
            Source = new DelegatedDataSource(RowsInSection, GetCell, GetHeightForCellRow, GetHeightForSectionHeader, GetNumberOfSections, GetViewForSectionHeader, RowWasSelected, GetViewForSectionFooter, GetHeightForSectionFooter);
        }

        protected virtual UIView GetViewForSectionHeader(int section)
        {
            return null;
        }

        protected virtual UIView GetViewForSectionFooter(int section)
        {
            return null;
        }

        protected virtual float GetHeightForSectionHeader(int section)
        {
            return default(float);
        }

        protected virtual float GetHeightForSectionFooter(int section)
        {
            return default(float);
        }

        protected virtual void RowWasSelected(int section, int item)
        {
        }

        protected abstract int GetNumberOfSections();
        protected abstract void RegisterTypesForReuse();
        protected abstract int RowsInSection(int section);
        protected abstract UITableViewCell GetCell(NSIndexPath indexPath);
        protected abstract float GetHeightForCellRow(NSIndexPath indexPath);

        internal class DelegatedDataSource : UITableViewSource
        {
            private readonly Func<int, int> _rowsInSection;
            private readonly Func<NSIndexPath, UITableViewCell> _getCell;
            private readonly Func<NSIndexPath, float> _getHeightForRow;
            private readonly Func<int, float> _getHeightForHeader;
            private readonly Func<int, float> _getHeightForFooter;
            private readonly Func<int> _numberOfSections;
            private readonly Func<int, UIView> _getViewForHeader;
            private readonly Func<int, UIView> _getViewForFooter;
            private readonly Action<int, int> _rowSelected;

            public DelegatedDataSource(Func<int, int> rowsInSection, Func<NSIndexPath, UITableViewCell> getCell, Func<NSIndexPath, float> getHeightForRow, Func<int, float> getHeightForHeader, Func<int> numberOfSections, Func<int, UIView> getViewForHeader, Action<int, int> rowSelected, Func<int, UIView> getViewForFooter, Func<int, float> getHeightForFooter)
            {
                _rowsInSection = rowsInSection;
                _getCell = getCell;
                _getHeightForRow = getHeightForRow;
                _getHeightForHeader = getHeightForHeader;
                _numberOfSections = numberOfSections;
                _getViewForHeader = getViewForHeader;
                _rowSelected = rowSelected;
                _getViewForFooter = getViewForFooter;
                _getHeightForFooter = getHeightForFooter;
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                _rowSelected(indexPath.Section, indexPath.Item);
            }

            public override float GetHeightForHeader(UITableView tableView, int section)
            {
                return _getHeightForHeader(section);
            }

            public override float GetHeightForFooter(UITableView tableView, int section)
            {
                return _getHeightForFooter(section);
            }

            public override int RowsInSection(UITableView tableView, int section)
            {
                return _rowsInSection(section);
            }

            public override int NumberOfSections(UITableView tableView)
            {
                return _numberOfSections();
            }

            public override UIView GetViewForHeader(UITableView tableView, int section)
            {
                return _getViewForHeader(section);
            }

            public override UIView GetViewForFooter(UITableView tableView, int section)
            {
                return _getViewForFooter(section);
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                return _getCell(indexPath);
            }

            public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            {
                return _getHeightForRow(indexPath);
            }
        }
    }
}