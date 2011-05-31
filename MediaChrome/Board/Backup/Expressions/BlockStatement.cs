using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Expressions {
    [Serializable]
    public class BlockStatement: Statement, IWalkable {

        class StatementPosition : StatementWalkerPosition {
            bool m_initialized = false;
            LinkedListNode<Statement> m_current;
            LinkedList<Statement> m_list;

            public StatementPosition(LinkedList<Statement> list) {
                if (list == null)
                    throw new ArgumentNullException("list");
                m_current = list.First;
                m_list = list;
            }

            public override bool DeleteStatement() {
                if (!m_initialized || m_current == null)
                    return false;

                var next = m_current.Next;
                m_list.Remove(m_current);

                return m_current != null;
            }

            public override void InsertBefore(Statement newStatement) {
                if (!m_initialized)
                    throw new InvalidOperationException("A valid position is required");
                if (m_current != null) {
                    m_list.AddBefore(m_current, newStatement);
                } else {
                    m_list.AddLast(newStatement);
                }
            }

            public override void InsertAfter(Statement newStatement) {
                if (!m_initialized)
                    throw new InvalidOperationException("A valid position is required");
                if (m_current != null) {
                    m_list.AddAfter(m_current, newStatement);
                } else {
                    m_list.AddLast(newStatement);
                }
            }

            public override void ReplaceStatement(Statement newStatement) {
                if (m_current == null)
                    throw new InvalidOperationException("A valid position is required");
                var newNode = m_list.AddAfter(m_current, newStatement);
                m_list.Remove(m_current);
                m_current = newNode;
            }

            public override Statement Current {
                get {
                    if (m_current == null)
                        throw new InvalidOperationException("A valid position is required");
                    return m_current.Value;
                }
            }

            public override bool MoveNext() {
                if (!m_initialized) {
                    m_initialized = true;
                } else {
                    m_current = m_current.Next;
                }

                return m_current != null;
            }
        }

        public LinkedList<Statement> Statements { get; set; }

        public BlockStatement() {
            Statements = new LinkedList<Statement>();
        }

        [System.Diagnostics.DebuggerStepThrough]
        public override void Accept(IStatementVisitor visitor) {
            visitor.Visit(this);
        }


        private bool reordered = false;

        public LinkedList<Statement> ReorderStatements() {
            if (!reordered) {
                var iter = Statements.First;

                var nodeVars = new LinkedListNode<Statement>(null);
                var nodeFuncs = new LinkedListNode<Statement>(null);

                Statements.AddFirst(nodeVars);
                Statements.AddFirst(nodeFuncs);

                StatementWalker.Walk(this, delegate(StatementWalkerPosition pos) {
                    var val = pos.Current;
                    if (val == null)
                        return;

                    if (val is VariableDeclarationStatement) {
                        //Statements.Remove(iter);
                        var varDecl = new VariableDeclarationStatement();
                        varDecl.Identifier = ((VariableDeclarationStatement)val).Identifier;
                        varDecl.Global = ((VariableDeclarationStatement)val).Global;
                        Statements.AddBefore(nodeVars, varDecl);
                    }
                    if (val is FunctionDeclarationStatement) {
                        pos.DeleteStatement();
                        Statements.AddBefore(nodeFuncs, val);
                    }

                    if (val is BlockStatement) {
                        ((BlockStatement)val).reordered = true;
                    }
                });

                Statements.Remove(nodeFuncs);
                Statements.Remove(nodeVars);

                reordered = true;
            }

            return Statements;
        }

        #region IWalkable Members

        public StatementWalkerPosition GetFirstStatement() {
            return new StatementPosition(Statements);
        }

        #endregion
    }
}
