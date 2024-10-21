using CinemaBookingCore.Models;
using CinemaBookingCore.States;
using CinemaBookingCore.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CinemaBookingCore.Utilities.Constants;

namespace CinemaBookingCore.Validations.Rules
{
    public abstract class Rule
    {
        public abstract Result Evaluate(string input, CinemaContext cinemaContext = null);

        //Precedence order for running the rule. Most elementary/basic rules are given lower precendence and are run earlier
        public int RuleOrder { get; private set; }

        protected Rule(int ruleOrder)
        {
            RuleOrder = ruleOrder;
        }

    }
}
