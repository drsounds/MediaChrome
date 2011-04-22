using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Expressions {
    [Serializable]
    public class FunctionDeclarationStatement : Statement, IFunctionDeclaration, IWalkable {
        public string Name { get; set; }
        public List<string> Parameters { get; set; }
        public Statement Statement { get; set; }

        public FunctionDeclarationStatement() {
            Parameters = new List<string>();
        }

        [System.Diagnostics.DebuggerStepThrough]
        public override void Accept(IStatementVisitor visitor) {
            visitor.Visit(this);
        }


        #region IWalkable Members

        public StatementWalkerPosition GetFirstStatement() {
            var walker = new CustomWalkerPosition(new Statement[]{ Statement });
            walker.OnDelete += delegate(object sender, StatementEventArgs<Statement> args) {
                if (args.position == null)
                    return;
                var empty = new EmptyStatement() {
                    Label = args.position.Label,
                    Source = args.position.Source
                };
                if (args.position == Statement)
                    Statement = empty;
            };
            return walker;
        }

        #endregion
    }
}
