using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Expressions {
    interface IStatementInfo {
        Statement statement { get; }
    }

    class StatementEventArgs<T>: EventArgs {
        public T position { get; set; }
        public Statement statement { get; set; }

        public StatementEventArgs(T info) {
            this.position = info;
        }

        public StatementEventArgs(T info,Statement statement) {
            this.position = info;
            this.statement = statement;
        }
    }

    abstract class CustomWalkerPositionBase<T>: StatementWalkerPosition{
        protected IEnumerator<T> m_statements;

        public event EventHandler<StatementEventArgs<T>> OnDelete;
        public event EventHandler<StatementEventArgs<T>> OnReplace;
        public event EventHandler<StatementEventArgs<T>> OnInsertBefore;
        public event EventHandler<StatementEventArgs<T>> OnInsertAfter;

        public CustomWalkerPositionBase(IEnumerable<T> statements) {
            if (statements == null)
                throw new ArgumentNullException("statements");
            m_statements = statements.GetEnumerator();
        }

        public override bool DeleteStatement() {
            T info = m_statements.Current;
            bool result = m_statements.MoveNext();

            var handler = OnDelete;
            if (handler != null)
                handler(this, new StatementEventArgs<T>(info));

            return result;
        }

        public override void InsertBefore(Statement newStatement) {
            var handler = OnInsertBefore;
            if (handler != null)
                handler(this, new StatementEventArgs<T>(m_statements.Current, newStatement));
        }

        public override void InsertAfter(Statement newStatement) {
            var handler = OnInsertAfter;
            if (handler != null)
                handler(this, new StatementEventArgs<T>(m_statements.Current, newStatement));
        }

        public override void ReplaceStatement(Statement newStatement) {
            var handler = OnReplace;
            if (handler != null)
                handler(this, new StatementEventArgs<T>(m_statements.Current, newStatement));
        }


        
        public override bool MoveNext() {
            return m_statements.MoveNext();
        }

        public abstract override Statement Current {
            get;
        }
    }

    class CustomWalkerPosition: CustomWalkerPositionBase<Statement> {

        public CustomWalkerPosition(IEnumerable<Statement> statements)
            : base(statements) {
        }

        public override Statement Current {
            get {
                return m_statements.Current;
            }
        }
    }

}
