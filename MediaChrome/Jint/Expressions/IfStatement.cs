using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Expressions {
    [Serializable]
    public class IfStatement : Statement, IWalkable {
        public Expression Expression { get; set; }
        public Statement Then { get; set; }
        public Statement Else { get; set; }

        [System.Diagnostics.DebuggerStepThrough]
        public override void Accept(IStatementVisitor visitor) {
            visitor.Visit(this);
        }


        #region IWalkable Members

        public StatementWalkerPosition GetFirstStatement() {
            LinkedList<Statement> list = new LinkedList<Statement>();
            if (Then != null)
                list.AddLast(Then);
            if (Else != null)
                list.AddLast(Else);

            var walker = new CustomWalkerPosition( list );
            walker.OnDelete += delegate(object sender, StatementEventArgs<Statement> args) {
                if (args.position == null)
                    return;
                if (args.position == Then)
                    Then = new EmptyStatement() { Label = args.position.Label, Source = args.position.Source } ;

                if (args.position == Else)
                    Else = new EmptyStatement() { Label = args.position.Label, Source = args.position.Source };
            };

            return walker;
        }

        #endregion
    }
}
