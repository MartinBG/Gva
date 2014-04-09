﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Common.Data;
using Regs.Api.Models;
using Common.Api.Models;
using Docs.Api.Models;
using Gva.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using Common.Api.UserContext;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Gva.MigrationTool.Sets;
using Gva.MigrationTool.Nomenclatures;
using Regs.Api.LotEvents;
using Newtonsoft.Json.Serialization;

namespace Gva.MigrationTool
{
    class Migration
    {
        static void Main(string[] args)
        {
            string connStr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.19)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=VENI.CAA)));User Id=system;Password=DBSYSTEMVENI;";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                try
                {
                    conn.Open();

                    JsonConvert.DefaultSettings = () =>
                    {
                        return new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                            NullValueHandling = NullValueHandling.Ignore,
                            DefaultValueHandling = DefaultValueHandling.Include,
                            DateFormatHandling = DateFormatHandling.IsoDateFormat,
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        };
                    };

                    Nomenclature.migrateNomenclatures(conn);
                    Person.migratePersons(conn, Nomenclature.noms);
                }
                catch (OracleException e)
                {
                    Console.WriteLine("Exception Message: " + e.Message);
                    Console.WriteLine("Exception Source: " + e.Source);
                }
                finally
                {
                    conn.Dispose();
                }

            }
            Console.WriteLine("Migration finished!");
        }
    }
}