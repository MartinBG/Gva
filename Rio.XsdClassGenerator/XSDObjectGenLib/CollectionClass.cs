namespace Rio.XsdClassGenerator.XSDObjectGenLib
{
    using System;

    internal class CollectionClass
    {
        public string Datatype;
        public string FieldName;
        public bool IsAbstract;

        public CollectionClass(string fieldName, string datatype, bool isAbstract)
        {
            this.FieldName = fieldName;
            this.Datatype = datatype;
            this.IsAbstract = isAbstract;
        }
    }
}

