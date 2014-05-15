using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Common.Rio.PortalBridge
{
    public static class MapperEx
    {
        private static Func<TSourceCollection, TDestinationCollection> CreateCollectionConversionFunction<TSourceCollection, TSource, TDestinationCollection, TDestination>()
            where TSourceCollection : List<TSource>, new()
            where TDestinationCollection : List<TDestination>, new()
        {
            return sc =>
            {
                if (sc == null)
                {
                    return null;
                }

                var dc = new TDestinationCollection();
                dc.AddRange(sc.Select(si => Mapper.Map<TSource, TDestination>(si)));

                return dc;
            };
        }

        public static void CreateCollectionMap<TSourceCollection, TSource, TDestinationCollection, TDestination>()
            where TSourceCollection : List<TSource>, new()
            where TDestinationCollection : List<TDestination>, new()
        {
            Mapper.CreateMap<TSourceCollection, TDestinationCollection>()
                .ConvertUsing(CreateCollectionConversionFunction<TSourceCollection, TSource, TDestinationCollection, TDestination>());

            //reverse
            Mapper.CreateMap<TDestinationCollection, TSourceCollection>()
                .ConvertUsing(CreateCollectionConversionFunction<TDestinationCollection, TDestination, TSourceCollection, TSource>());
        }
    }
}
