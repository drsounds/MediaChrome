using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaChrome.Models
{
    public abstract class UserGeneratedModel : Model
    {
        public User owner { get; set; }
    }
    
}
