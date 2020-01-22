using System;
using System.Collections;
using System.Windows.Forms;
using System.Linq;

namespace Faktury.Windows
{
    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        public bool SortByPrev = false;
        /// <summary>
        /// Specifies the column to be sorted
        /// </summary>
        private int _columnToSort;

        /// <summary>
        /// Case insensitive comparer object
        /// </summary>  private NumberCaseInsensitiveComparer ObjectCompare;
        private readonly ImageTextComparer _firstObjectCompare;
        private readonly NumberCaseInsensitiveComparer _objectCompare;
        /// <summary>
        /// Class constructor.  Initializes various elements
        /// </summary>
        /// 
        public ListViewColumnSorter()
        {
            // Initialize the column to '0'
            _columnToSort = 0;
            // Initialize the sort order to 'none'
            //OrderOfSort = SortOrder.None;
            Order = SortOrder.Ascending;
            // Initialize my implementationof CaseInsensitiveComparer object
            _objectCompare = new NumberCaseInsensitiveComparer();
            _firstObjectCompare = new ImageTextComparer();
        }  /// <summary>
        /// This method is inherited from the IComparer interface.
        /// It compares the two objects passed\
        /// using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal,
        /// negative if 'x' is less than 'y' and positive
        /// if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            // Cast the objects to be compared to ListViewItem objects
            var listViewX = (ListViewItem)x;
            var listViewY = (ListViewItem)y;
            if (_columnToSort == 0)
            {
                compareResult = _firstObjectCompare.Compare(x, y);
            }
            else
            {
                // Compare the two items
                compareResult =
                  _objectCompare.Compare(listViewX.SubItems[_columnToSort].Text, listViewY.SubItems[_columnToSort].Text);
            }

            if (compareResult == 0 && _columnToSort == 1 && SortByPrev) 
            {

                compareResult =_objectCompare.Compare(listViewX.SubItems[_columnToSort-1].Text, listViewY.SubItems[_columnToSort-1].Text);

            }

            // Calculate correct return value based on object comparison
            if (Order == SortOrder.Ascending)
            {
                // Ascending sort is selected,
                // return normal result of compare operation
                return compareResult;
            }

            if (Order == SortOrder.Descending)
            {
                // Descending sort is selected,
                // return negative result of compare operation
                return (-compareResult);
            }

            // Return '0' to indicate they are equal
            return 0;
        }

        /// <summary>
        /// Gets or sets the number of the column to which
        /// to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn
        {
            set => _columnToSort = value;
            get => _columnToSort;
        }
        /// <summary>
        /// Gets or sets the order of sorting to apply
        /// (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order { set; get; }

    }
    public class ImageTextComparer : IComparer
    {
        //private CaseInsensitiveComparer ObjectCompare;
        private readonly NumberCaseInsensitiveComparer _objectCompare;

        public ImageTextComparer()
        {
            // Initialize the CaseInsensitiveComparer object
            _objectCompare = new NumberCaseInsensitiveComparer();
        }
        public int Compare(object x, object y)
        {
            //int compareResult;
            // Cast the objects to be compared to ListViewItem objects
            var listViewX = (ListViewItem)x;
            var image1 = listViewX.ImageIndex;
            var listViewY = (ListViewItem)y;
            var image2 = listViewY.ImageIndex;
            if (image1 < image2)
            {
                return -1;
            }

            if (image1 == image2)
            {
                return _objectCompare.Compare(listViewX.Text, listViewY.Text);
            }

            return 1;
        }
    }
    public class NumberCaseInsensitiveComparer : CaseInsensitiveComparer
    {
        public new int Compare(object x, object y)
        {
            // in case x,y are strings and actually number,
            // convert them to int and use the base.Compare for comparison
            if (x is string s && y is string s1)
            {
                if (IsWholeNumber(s) && IsWholeNumber(s1))
                {
                    return base.Compare(Convert.ToInt32(x),
                        Convert.ToInt32(y));
                }
                else
                {
                    return string.Compare(
                        new string(s.Where(char.IsLetterOrDigit).ToArray()),
                        new string(s1.Where(char.IsLetterOrDigit).ToArray())
                        , StringComparison.OrdinalIgnoreCase);
                }
            }

            return base.Compare(x, y);
        }
        private bool IsWholeNumber(string strNumber)
        {
            // use a regular expression to find out if string is actually a number
            foreach (var character in strNumber)
            {
                if (!char.IsDigit(character))
                    return false;
            }

            return true;
        }
    }
}
