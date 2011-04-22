using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Expressions {
    [Serializable]
    public class TryStatement : Statement, IWalkable {
        public Statement Statement { get; set; }
        public FinallyClause Finally { get; set; }
        public CatchClause Catch { get; set; }

        public TryStatement() {
        }

        [System.Diagnostics.DebuggerStepThrough]
        public override void Accept(IStatementVisitor visitor) {
            visitor.Visit(this);
        }

        #region IWalkable Members

        public StatementWalkerPosition GetFirstStatement() {
            LinkedList<Statement> list = new LinkedList<Statement>();
            list.AddLast(Statement);
            if (Finally != null)
                list.AddLast(Finally.Statement);
            if (Catch != null)
                list.AddLast(Catch.Statement);

            var walker = new CustomWalkerPosition(list);
            walker.OnDelete += delegate(object sender, StatementEventArgs<Statement> args) {
                if (args.position == null)
                    return;

                if( args.position == Statement)
                    Statement = new EmptyStatement() { Label = args.position.Label, Source = args.position.Source };

                if (args.position == Finally.Statement)
                    Finally.Statement = new EmptyStatement() { Label = args.position.Label, Source = args.position.Source };

                if (args.position == Catch.Statement)
                    Catch.Statement = new EmptyStatement() { Label = args.position.Label, Source = args.position.Source };
            };

            return walker;
        }

        #endregion
    }
}
