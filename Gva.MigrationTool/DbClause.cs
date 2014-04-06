using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gva.MigrationTool
{
    public class DbClause
    {
        public static DbClause Empty = new DbClause(String.Empty);

        private object[] _parameters;
        private string _text;
        private bool _isEnabled;

        public DbClause(string text)
        {
            _text = text;
            _parameters = new object[] { };
            _isEnabled = !String.IsNullOrEmpty(text);
        }

        public DbClause(string text, params object[] parameters)
        {
            _text = text;
            _parameters = parameters;
            _isEnabled = !String.IsNullOrWhiteSpace(text) && parameters.Length != 0 &&
                parameters.Where(o => o != null).Count() > 0;
        }

        public static DbClause CreateDbClauseIn(string text, IEnumerable parameters)
        {
            StringBuilder clauseText = new StringBuilder();

            List<object> parametersList = new List<object>();

            int i = 0;
            foreach (object param in parameters)
            {
                parametersList.Add(param);

                if (i == 0)
                {
                    clauseText.AppendFormat("{{{0}}}", i);
                }
                else
                {
                    clauseText.AppendFormat(",{{{0}}}", i);
                }

                i++;
            }

            return new DbClause(String.Format(text, clauseText.ToString()), parametersList.ToArray());
        }

        public object[] Parameters
        {
            get { return _parameters; }
            set { _parameters = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; }
        }
    }
}
