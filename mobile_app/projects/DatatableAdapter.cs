//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using System.Data;
//using Android.Graphics;

//namespace MobileCollector.projects
//{
//    public class DatatableAdapter : BaseAdapter<string>
//    {
//        private Activity _context;
//        private DataTable _datasource;

//        public DatatableAdapter(Activity ctx, DataTable datasource) : base()
//        {
//            this._context = ctx;
//            this._datasource = datasource;
//        }

//        private int GetCol(int pos)
//        {
//            int col = pos % (itemTable.Columns.Count);
//            return col;
//        }

//        private int GetRow(int pos)
//        {
//            int row = pos / (itemTable.Columns.Count);
//            return row;
//        }

//        private string PositionToString(int pos)
//        {
//            int row = GetRow(pos);
//            int col = GetCol(pos);

//            if (row == 0)
//            {
//                return itemTable.Columns[col].ColumnName;
//            }
//            if (itemTable.Rows[row - 1][col] != DBNull.Value)
//            {
//                return itemTable.Rows[row - 1][col].ToString();
//            }
//            return "";
//        }

//        public override string this[int position]
//        {
//            get { return PositionToString(position); }
//        }

//        public override long GetItemId(int position)
//        {
//            return position;
//        }

//        public override int Count
//        {
//            get { return (itemTable.Rows.Count + 1) * itemTable.Columns.Count; }
//        }

//        public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
//        {
//            // find the current row number, need to test we're in the header
//            int row = GetRow(position);

//            // Get our text for position
//            var item = PositionToString(position);

//            // find if we have a control for the cell, if not create it
//            var txtName = convertView as EditText ?? new EditText(_context);

//            // if the row is a header give it a different colour
//            if (row == 0)
//            {
//                txtName.SetBackgroundColor(new Color(0x0d, 0x12, 0xf5));
//                txtName.SetTextColor(new Color(0x0, 0x0, 0x0));
//            }

//            //Assign item's values to the various subviews
//            txtName.SetText(item, TextView.BufferType.Normal);

//            //Finally return the view
//            return txtName;
//        }
//    }
//}