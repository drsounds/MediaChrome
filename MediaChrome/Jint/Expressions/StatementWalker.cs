using System;
using System.Collections.Generic;
using System.Text;

namespace Jint.Expressions {

    public class StatementWalker {
        public static void Walk(IWalkable target, Action<StatementWalkerPosition> handler) {
            if (target == null)
                throw new ArgumentNullException("target");
            if (handler == null)
                throw new ArgumentNullException("handler");

            for (StatementWalkerPosition pos = target.GetFirstStatement();pos.MoveNext();) {
                handler(pos);
                if (pos.Current is IWalkable)
                    Walk(pos.Current as IWalkable, handler);
            }
        }
    }
}
