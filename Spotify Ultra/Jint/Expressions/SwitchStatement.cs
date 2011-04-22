using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Expressions {
    [Serializable]
    public class SwitchStatement : Statement, IWalkable {
        class StatementInfo {
            public int index { get; private set; }
            public Statement statement { get; private set; }

            public StatementInfo(int i, Statement s) {
                index = i;
                statement = s;
            }
        }

        class WalkerWrapper : IEnumerable<StatementInfo> {
            IList<CaseClause> m_list;
            Statement m_additional;

            public WalkerWrapper(IList<CaseClause> list, Statement additional) {
                m_list = list;
                m_additional = additional;
            }

            public IEnumerator<StatementInfo> GetEnumerator() {
                for (int i = 0;i < m_list.Count;i++)
                    yield return new StatementInfo(i, m_list[i].Statements);
                if (m_additional != null)
                    yield return new StatementInfo(-1, m_additional);
            }

            #region IEnumerable Members

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
                return GetEnumerator();
            }

            #endregion
        }

        class IndexedWalkerPosition : CustomWalkerPositionBase<StatementInfo> {
            public IndexedWalkerPosition(IList<CaseClause> list, Statement additional)
                : base(new WalkerWrapper(list, additional)) {
            }

            public override Statement Current {
                get { return m_statements.Current.statement; }
            }
        }

        public Statement Expression { get; set; }
        public List<CaseClause> CaseClauses { get; private set; }
        public Statement DefaultStatements { get; set; }

        public SwitchStatement() {
            CaseClauses = new List<CaseClause>();
        }

        [System.Diagnostics.DebuggerStepThrough]
        public override void Accept(IStatementVisitor visitor) {
            visitor.Visit(this);
        }


        #region IWalkable Members

        public StatementWalkerPosition GetFirstStatement() {
            var walker = new IndexedWalkerPosition(CaseClauses,DefaultStatements);
            walker.OnDelete += delegate(object sender, StatementEventArgs<StatementInfo> args) {
                if (args.position.index < 0 && args.position.statement == DefaultStatements) {
                    DefaultStatements = new EmptyStatement() {
                        Label = args.position.statement.Label,
                        Source = args.position.statement.Source
                    };
                } else if (args.position.index >= 0) {
                    CaseClauses[args.position.index].Statements.Statements.Clear();
                    CaseClauses[args.position.index].Statements.Statements.AddLast(
                        new EmptyStatement() {
                            Label = args.position.statement.Label,
                            Source = args.position.statement.Source
                        }
                    );
                }

            };
            return walker;
        }

        #endregion
    }
}
