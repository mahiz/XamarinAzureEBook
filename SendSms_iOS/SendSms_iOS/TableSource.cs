using System;
using UIKit;
using Foundation;

namespace SendSms_iOS
{
    public class TableSource : UITableViewSource
    {
        // An array of items as the data source
        protected string[] tableItems;

        // An identifier used to retrieve cells
        protected string cellIdentifier = "TableCell";

        // The owner of the bound Table View
        ViewController owner;

        public TableSource(string[] items, ViewController owner)
        {
            tableItems = items;
            this.owner = owner;
        }

        // Get the content of a specific cell
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // request a recycled cell to save memory
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);
            // if there are no cells to reuse, create a new one
            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier);
            cell.TextLabel.Text = tableItems[indexPath.Row];
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return tableItems.Length;
        }

        // Retrieve the selected row and sends an sms 
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            owner.SendSms(tableItems[indexPath.Row]);
        }
    }
}