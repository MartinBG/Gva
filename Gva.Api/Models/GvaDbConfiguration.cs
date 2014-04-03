﻿using System.Data.Entity;
using Common.Data;

namespace Gva.Api.Models
{
    public class GvaDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GvaCaseTypeMap());
            modelBuilder.Configurations.Add(new GvaCorrespondentMap());
            modelBuilder.Configurations.Add(new GvaLotCaseMap());
            modelBuilder.Configurations.Add(new GvaApplicationMap());
            modelBuilder.Configurations.Add(new GvaAppLotFileMap());
            modelBuilder.Configurations.Add(new GvaFileMap());
            modelBuilder.Configurations.Add(new GvaLotFileMap());
            modelBuilder.Configurations.Add(new GvaLotObjectMap());
            modelBuilder.Configurations.Add(new GvaViewPersonMap());
            modelBuilder.Configurations.Add(new GvaViewPersonRatingMap());
            modelBuilder.Configurations.Add(new GvaViewPersonLicenceMap());
            modelBuilder.Configurations.Add(new GvaViewInventoryItemMap());
            modelBuilder.Configurations.Add(new GvaViewApplicationMap());
        }
    }
}
