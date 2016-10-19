using System.Collections.Generic;

namespace FireDogeWebBrowser
{
    /// <summary>
    /// This class derives from a simple List&lt;T> class and is
    /// an enhenced version of it. We added methods to move through the list
    /// and everything is hidden to the user
    /// </summary>
    /// <typeparam name="T">There is no restriction for the type used</typeparam>
    public class NavigationList<T> : List<T>
    {
        private int _currentIndex = 0;

        /// <summary>
        /// This method returns the current index where we stand in the list
        /// </summary>
        public int CurrnetIndex
        {
            get
            {
                if (_currentIndex > Count - 1) { _currentIndex = Count - 1; }
                if (_currentIndex < 0) { _currentIndex = 0; }
                return _currentIndex;
            }
            set { _currentIndex = value; }
        }

        /// <summary>
        /// This method gives the next item in the list
        /// </summary>
        public T MoveNext
        {
            get { _currentIndex++; return this[CurrnetIndex]; }
        }

        /// <summary>
        /// This method gives the previous item in the list
        /// </summary>
        public T MovePrevious
        {
            get { _currentIndex--; return this[CurrnetIndex]; }
        }

        /// <summary>
        /// This method gives the current item in the list
        /// </summary>
        public T Current
        {
            get { return this[CurrnetIndex]; }
        }
    }
}
