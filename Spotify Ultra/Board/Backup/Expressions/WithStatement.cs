using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Expressions {
    [Serializable]
    public class WithStatement : Statement, IWalkable {
        public Statement Statement { get; set; }
        public Expression Expression { get; set; }

        public WithStatement(Expression expression, Statement statement) {
            Statement = statement;
            Expression = expression;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public override void Accept(IStatementVisitor visitor) {
            visitor.Visit(this);
        }


        #region IWalkable Members

        public StatementWalkerPosition GetFirstStatement() {
            var walker = new CustomWalkerPosition(new Statement[] { Statement });
            walker.OnDelete += delegate(object sender, StatementEventArgs<Statement> args) {
                if (args.position != null && args.position == Statement)
                    Statement = new EmptyStatement() {
                        Label = args.position.Label,
                        Source = args.position.Source
                    };
            };
            return walker;
        }

        #endregion
    }
}
