using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Common.Data;

namespace Common.Sequence
{
    public class Sequence
    {
        private const int rangeSize = 100;

        private Object syncRoot = new Object();

        private string sequenceName;
        private int lastValue;
        private int incrementBy;
        private int rangeFirstValue;
        private int rangeLastValue;

        public Sequence(string sequenceName)
        {
            this.sequenceName = sequenceName;
            this.lastValue = 0;
            this.incrementBy = 0;
            this.rangeFirstValue = 0;
            this.rangeLastValue = 0;
        }

        public int NextValue()
        {
            lock (this.syncRoot)
            {
                if (this.lastValue == this.rangeLastValue)
                {
                    this.GetNextRange();
                    this.lastValue = this.rangeFirstValue;
                }
                else
                {
                    this.lastValue += this.incrementBy;
                }

                return this.lastValue;
            }
        }

        private void GetNextRange(bool changeNextValue = true)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings[UnitOfWork.ContextName].ConnectionString))
            {
                connection.Open();

                SqlParameter rangeFirstVal;
                SqlParameter rangeLastVal;
                SqlParameter sequenceIncrement;

                using (SqlCommand getRangeCmd = this.GetRangeCmd(connection, out rangeFirstVal, out rangeLastVal, out sequenceIncrement))
                {
                    getRangeCmd.ExecuteNonQuery();

                    this.rangeFirstValue = (int)rangeFirstVal.Value;
                    this.rangeLastValue = (int)rangeLastVal.Value;
                    this.incrementBy = (int)sequenceIncrement.Value;
                }
            }
        }

        private SqlCommand GetRangeCmd(
            SqlConnection connection,
            out SqlParameter rangeFirstVal,
            out SqlParameter rangeLastVal,
            out SqlParameter sequenceIncrement)
        {
            var cmd = new SqlCommand("sp_sequence_get_range", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@sequence_name", this.sequenceName);
            cmd.Parameters.AddWithValue("@range_size", rangeSize);

            rangeFirstVal = new SqlParameter("@range_first_value", SqlDbType.Variant);
            rangeFirstVal.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(rangeFirstVal);

            rangeLastVal = new SqlParameter("@range_last_value", SqlDbType.Variant);
            rangeLastVal.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(rangeLastVal);

            sequenceIncrement = new SqlParameter("@sequence_increment", SqlDbType.Variant);
            sequenceIncrement.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(sequenceIncrement);

            return cmd;
        }
    }
}
