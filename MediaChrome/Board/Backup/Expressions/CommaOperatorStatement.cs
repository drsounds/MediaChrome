using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Expressions {
    [Serializable]
    public class CommaOperatorStatement : Expression, IWalkable {
        class StatementInfo {
            public int index { get; private set; }
            public Statement statement { get; private set; }

            public StatementInfo(int i, Statement s) {
                index = i;
                statement = s;
            }
        }

        class WalkerWrapper : IEnumerable<StatementInfo> {
            IList<Statement> m_list;

            public WalkerWrapper(IList<Statement> list) {
                m_list = list;
            }

            public IEnumerator<StatementInfo> GetEnumerator() {
                for (int i = 0;i < m_list.Count;i++)
                    yield return new StatementInfo(i, m_list[i]);
            }

            #region IEnumerable Members

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
                return GetEnumerator();
            }

            #endregion
        }

        class IndexedWalkerPosition : CustomWalkerPositionBase<StatementInfo> {
            public IndexedWalkerPosition(IList<Statement> list)
                : base(new WalkerWrapper(list)) {
            }

            public override Statement Current {
                get { return m_statements.Current.statement; }
            }
        }

        public List<Statement> Statements { get; set; }

        public CommaOperatorStatement() {
            Statements = new List<Statement>();
        }

        [System.Diagnostics.DebuggerStepThrough]
        public override void Accept(IStatementVisitor visitor) {
            visitor.Visit(this);
        }


        #region IWalkable Members

        public StatementWalkerPosition GetFirstStatement() {
            var walker = new IndexedWalkerPosition(Statements);
            walker.OnDelete += delegate(object sender, StatementEventArgs<StatementInfo> args) {
                if (args.position == null)
                    return;
                Statements[args.position.index] = new EmptyStatement() {
                    Label = args.position.statement.Label,
                    Source = args.position.statement.Source
                };
            };
            return walker;
        }

        #endregion
    }
}
