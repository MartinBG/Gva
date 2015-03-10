using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class OrganizationalFormNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.OrganizationalFormNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }
        // "1", "Сдружение"
        // "2", "Фондация"
        // "3", "Клон на чуждестранно юридическо лице"

        public static readonly OrganizationalFormNomenclature Association = new OrganizationalFormNomenclature { ResourceKey = "Association", Code = "1" };
        public static readonly OrganizationalFormNomenclature Fondation = new OrganizationalFormNomenclature { ResourceKey = "Fondation", Code = "2" };
        public static readonly OrganizationalFormNomenclature ForeignEntity = new OrganizationalFormNomenclature { ResourceKey = "ForeignEntity", Code = "3" };

        public List<BaseNomenclature> GetValuesWithoutForeignEntity()
        {
            return new List<BaseNomenclature>()
            {
                new BaseNomenclature{ Value = Association.Code, Text = Association.Name},
                new BaseNomenclature{ Value = Fondation.Code, Text = Fondation.Name}
            };
        }

        public List<BaseNomenclature> GetAllValues()
        {
            return new List<BaseNomenclature>()
            {
                new BaseNomenclature(){ Value = Association.Code, Text = Association.Name},
                new BaseNomenclature(){ Value = Fondation.Code, Text = Fondation.Name},
                new BaseNomenclature(){ Value = ForeignEntity.Code, Text = ForeignEntity.Name}
            };
        }
    }
}
