// Copyright (c) Pawel Kadluczka, Inc. All rights reserved. See License.txt in the project root for license information.

namespace CodeFirstStoreFunctions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Data.Entity.Core.Mapping;
    using System.Data.Entity.Core.Metadata.Edm;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Diagnostics;
    using System.Linq;

    public class FunctionsConvention : IStoreModelConvention<EntityContainer>
    {
        private readonly string _defaultSchema;
        private readonly Type _methodClassType;

        public FunctionsConvention(string defaultSchema, Type methodClassType)
        {
            _defaultSchema = defaultSchema;
            _methodClassType = methodClassType;
        }

        public void Apply(EntityContainer item, DbModel model)
        {
            var functionImports = new FunctionDiscovery(model, _methodClassType).FindFunctionImports();
            var storeFunctionBuilder = new StoreFunctionBuilder(model, _defaultSchema);

            foreach (var functionImport in functionImports)
            {
                var functionImportDefinition = CreateFunctionImport(model, functionImport);
                var storeFunctionDefinition = storeFunctionBuilder.Create(functionImport);
                model.ConceptualModel.Container.AddFunctionImport(functionImportDefinition);
                model.StoreModel.AddItem(storeFunctionDefinition);

                FunctionImportResultMapping entityTypeResultMapping = null;
                if (functionImport.ReturnType is EntityType)
                {
                    var entityType = (EntityType)functionImport.ReturnType;

                    var returnTypePropertyMappings = new Collection<FunctionImportReturnTypePropertyMapping>();
                    foreach (var propertyMapping in model.GetEntityTypePropertyMappings(entityType).OfType<ScalarPropertyMapping>())
                    {
                        returnTypePropertyMappings.Add(new FunctionImportReturnTypeScalarPropertyMapping(propertyMapping.Property.Name, propertyMapping.Column.Name));
                    }

                    var typeMapping = new FunctionImportEntityTypeMapping(
                        Enumerable.Empty<EntityType>(),
                        new[] { entityType },
                        returnTypePropertyMappings,
                        Enumerable.Empty<FunctionImportEntityTypeMappingCondition>());

                    entityTypeResultMapping = new FunctionImportResultMapping();
                    entityTypeResultMapping.AddTypeMapping(typeMapping);
                }

                if (functionImportDefinition.IsComposableAttribute)
                {
                    model.ConceptualToStoreMapping.AddFunctionImportMapping(
                        new FunctionImportMappingComposable(
                            functionImportDefinition,
                            storeFunctionDefinition,
                            entityTypeResultMapping ?? new FunctionImportResultMapping(),
                            model.ConceptualToStoreMapping));
                }
                else
                {
                    FunctionImportResultMapping[] functionImportResultMappings;
                    if (entityTypeResultMapping != null)
                    {
                        functionImportResultMappings = new FunctionImportResultMapping[] { entityTypeResultMapping };
                    }
                    else
                    {
                        functionImportResultMappings = new FunctionImportResultMapping[0];
                    }

                    model.ConceptualToStoreMapping.AddFunctionImportMapping(
                        new FunctionImportMappingNonComposable(
                            functionImportDefinition,
                            storeFunctionDefinition,
                            functionImportResultMappings,
                            model.ConceptualToStoreMapping));
                }
            }

            // TODO: scalar functions?, model defined functions?, multiple result sets?
        }

        private EdmFunction CreateFunctionImport(DbModel model, FunctionImport functionImport)
        {
            List<EntitySet> entitySets = null;
            if (functionImport.ReturnType.BuiltInTypeKind == BuiltInTypeKind.EntityType)
            {
                // TODO: derived types?
                entitySets =
                    model.ConceptualModel.Container.EntitySets.Where(s => s.ElementType == functionImport.ReturnType)
                        .ToList();

                // TODO: throw if no entity set found
                Debug.Assert(entitySets.Count == 1, "Invalid model (MEST)");
            }

            var functionPayload =
                new EdmFunctionPayload
                {
                    Parameters =
                        functionImport
                            .Parameters
                            .Select(p => FunctionParameter.Create(p.Key, p.Value, ParameterMode.In))
                            .ToList(),
                    ReturnParameters = new[]
                    {
                        FunctionParameter.Create(
                            "ReturnParam",
                            functionImport.ReturnType.GetCollectionType(),
                            ParameterMode.ReturnValue)
                    },
                    IsComposable = functionImport.IsComposable,
                    IsFunctionImport = true,
                    EntitySets = entitySets
                };

            return EdmFunction.Create(
                functionImport.Name,
                model.ConceptualModel.Container.Name,
                DataSpace.CSpace,
                functionPayload,
                null);
        }
    }
}