using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Expressions {
    /// <summary>
    /// Iterator over statements with editing ability
    /// </summary>
    public abstract class StatementWalkerPosition: IEnumerator<Statement> {
        
        /// <summary>
        /// Deletes current statement and moves to the next statement
        /// </summary>
        /// <returns><c>true</c> if statement is deleted and moved to the next element, <c>false</c>
        /// if there are no more elemets left in the iterator.</returns>
        public abstract bool DeleteStatement();
        
        /// <summary>
        /// Inserts a specified statement before current statement
        /// </summary>
        /// <param name="newStatement">A statement to insert</param>
        public abstract void InsertBefore(Statement newStatement);

        /// <summary>
        /// Inserts a specified statement after current statement
        /// </summary>
        /// <param name="newStatement">A statement to insert</param>
        public abstract void InsertAfter(Statement newStatement);

        /// <summary>
        /// Replaces current statement with a specified.
        /// </summary>
        /// <param name="newStatement">A statement to replace</param>
        public abstract void ReplaceStatement(Statement newStatement);

        #region IEnumerator<Statement> Members

        /// <summary>
        /// Current statement
        /// </summary>
        public abstract Statement Current {
            get;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Does nothing
        /// </summary>
        public void Dispose() {
        }

        #endregion

        #region IEnumerator Members

        object System.Collections.IEnumerator.Current {
            get { return Current; }
        }

        /// <summary>
        /// Moves to the next statement
        /// </summary>
        /// <returns><c>true</c> successful, <c>false</c> the end is reached</returns>
        public abstract bool MoveNext();

        /// <summary>
        /// Not supported
        /// </summary>
        public void Reset() {
            throw new NotImplementedException();
        }

        #endregion
    }
}
